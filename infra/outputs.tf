output "fqdn" {
  description = "Public hostname of the new Container App."
  value       = azurerm_container_app.api.ingress[0].fqdn
}

output "app_url" {
  description = "Base URL to point the frontend's catalog API_BASE at."
  value       = "https://${azurerm_container_app.api.ingress[0].fqdn}"
}
