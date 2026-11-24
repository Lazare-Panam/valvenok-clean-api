
import {
    to = azurerm_mssql_database.db
    id = "/subscriptions/a61d3219-20b5-47d5-a096-33b4666985cd/resourceGroups/rg-pv-dev/providers/Microsoft.Sql/servers/panamv/databases/ValvenokClean"
}
import {
    to = azurerm_mssql_firewall_rule.valvenok_db
        id = "/subscriptions/a61d3219-20b5-47d5-a096-33b4666985cd/resourceGroups/rg-pv-dev/providers/Microsoft.Sql/servers/panamv/firewallRules/FirewallRuleValvenokDB"
}
data "azurerm_mssql_server" "existing" {
  name                = var.sql_server_name
  resource_group_name = var.sql_server_resource_group
}

resource "azurerm_mssql_database" "db" {
  name                 = var.sql_database_name
  server_id            = data.azurerm_mssql_server.existing.id
  sku_name             = "Basic"
  max_size_gb          = 2
  collation            = "SQL_Latin1_General_CP1_CI_AS"
  storage_account_type = "Local"
}

# NOTE: this is the wide-open rule (0.0.0.0–255.255.255.255) that already exists.
# Kept as-is so the import is clean; tighten it in a separate, deliberate change.
resource "azurerm_mssql_firewall_rule" "valvenok_db" {
  name             = "FirewallRuleValvenokDB"
  server_id        = data.azurerm_mssql_server.existing.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "255.255.255.255"
}
