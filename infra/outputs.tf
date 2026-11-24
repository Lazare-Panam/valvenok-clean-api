output "fqdn" {
  description = "Public hostname of the new Container App."
  value       = azurerm_container_app.api.ingress[0].fqdn
}

output "app_url" {
  description = "Base URL to point the frontend's catalog API_BASE at."
  value       = "https://${azurerm_container_app.api.ingress[0].fqdn}"
}
output "app_insights_connection_string" {
  description = "Application Insights connection string"
  value       = azurerm_application_insights.main.connection_string
  sensitive   = true
}

output "app_insights_instrumentation_key" {
  description = "Application Insights instrumentation key"
  value       = azurerm_application_insights.main.instrumentation_key
  sensitive   = true
}

output "app_insights_app_id" {
  description = "Application Insights App ID (for API queries)"
  value       = azurerm_application_insights.main.app_id
}