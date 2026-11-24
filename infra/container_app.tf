data "azurerm_container_app_environment" "existing" {
  name                = var.container_app_environment_name
  resource_group_name = var.container_app_environment_rg
}


resource "azurerm_container_app" "api" {
  name                         = var.app_name
  resource_group_name          = azurerm_resource_group.rg.name
  container_app_environment_id = data.azurerm_container_app_environment.existing.id
  revision_mode                = "Single"
  workload_profile_name        = "Consumption"

  # Pull from ACR with admin credentials (simplest; matches the existing app).
  # Hardening step later: swap to a user-assigned identity + AcrPull role.
  registry {
    server               = var.acr_login_server
    username             = var.acr_username
    password_secret_name = "acr-pwd"
  }

  # Secrets (referenced by env vars below via secret_name).
  secret {
    name  = "acr-pwd"
    value = var.acr_password
  }
  secret {
    name  = "mongo"
    value = var.mongo_conn
  }
  secret {
    name  = "sql"
    value = var.sql_conn
  }
  secret {
    name  = "storage"
    value = var.storage_conn
  }
  secret {
    name  = "queue"
    value = var.queue_conn
  }

  ingress {
    external_enabled = true
    target_port      = 8080
    transport        = "auto"

    traffic_weight {
      percentage      = 100
      latest_revision = true
    }
  }

  template {
    min_replicas = 1
    max_replicas = 2

    container {
      name   = "valvenok-api"
      image  = var.image
      cpu    = 0.5
      memory = "1Gi"

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = "Production"
      }
      env {
        name        = "MongoDB__ConnectionString"
        secret_name = "mongo"
      }
      env {
        name  = "MongoDB__DatabaseName"
        value = "ValvenokCleanDB"
      }
      env {
        name        = "ConnectionStrings__DefaultConnection"
        secret_name = "sql"
      }
      env {
        name        = "AzureStorage__ConnectionString"
        secret_name = "storage"
      }
      env {
        name        = "Queue__ConnectionString"
        secret_name = "queue"
      }
      env {
        name  = "Queue__QueueName"
        value = var.queue_name
      }
    }

    http_scale_rule {
      name                = "http-scale"
      concurrent_requests = "100"
    }
  }
}