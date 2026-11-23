# Terraform for the "valvenok-clean" Container App — the strangler-fig front door.
# It REUSES the existing Container Apps environment and the existing ACR; Mongo,
# SQL and Storage already exist, so this file only stands up the new app.
#
#   terraform init
#   terraform plan
#   terraform apply
#
# Secrets are fed from terraform.tfvars (git-ignored). NOTE: secret values land in
# terraform state — use a secure remote backend before this is more than a POC.

terraform {
  required_version = ">= 1.1.0"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0"
    }
  }
}

provider "azurerm" {
  features {}
  subscription_id = var.subscription_id
}

# Resource group for the clean stack's app.
resource "azurerm_resource_group" "rg" {
  name     = "rg-valvenok-clean"
  location = var.location
}

# Reuse the EXISTING Container Apps environment (do not create a new one).
data "azurerm_container_app_environment" "existing" {
  name                = var.container_app_environment_name
  resource_group_name = var.container_app_environment_rg
}

# The new front-door API container app.
resource "azurerm_container_app" "api" {
  name                         = var.app_name
  resource_group_name          = azurerm_resource_group.rg.name
  container_app_environment_id = data.azurerm_container_app_environment.existing.id
  revision_mode                = "Single"

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
