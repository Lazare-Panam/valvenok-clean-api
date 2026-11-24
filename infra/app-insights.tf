# Log Analytics workspace (required for workspace-based App Insights)
resource "azurerm_log_analytics_workspace" "main" {
  name                = "log-valvenok-clean"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  sku                 = "PerGB2018"
  retention_in_days   = 30

  tags = {
    Environment = "Production"
  }
}

# Create workspace-based Application Insights
resource "azurerm_application_insights" "main" {
  name                = "appi-webapp-prod-001"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  workspace_id        = azurerm_log_analytics_workspace.main.id

  # Application type is required by the provider and is case-sensitive
  # Options: web, ios, java, MobileCenter, Node.JS, other, phone, store
  application_type = "web"
  # Daily data cap in GB (0 means no cap)
  daily_data_cap_in_gb = 5
  # Sampling percentage (100 = no sampling, collect everything)
  sampling_percentage = 100

  # Data retention for workspace-based resources is controlled by
  # the linked Log Analytics workspace above

  tags = {
    Environment = "Production"
    Application = "WebApp"
  }
}