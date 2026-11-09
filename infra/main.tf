# Root Terraform config for Valvenok infrastructure.

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
resource "azurerm_resource_group" "rg" {
  name     = "rg-valvenok-clean"
  location = var.location
}

data "azurerm_mssql_server" "existing" {
  name                = var.sql_server_name
  resource_group_name = var.sql_server_resource_group
}

resource "azurerm_mssql_database" "db" {
  name        = var.sql_database_name
  server_id   = data.azurerm_mssql_server.existing.id
  sku_name    = "Basic"
  max_size_gb = 2
  collation   = "SQL_Latin1_General_CP1_CI_AS"
  storage_account_type = "Local"
}
resource "azurerm_mssql_firewall_rule" "example" {
  name             = "FirewallRuleValvenokDB"
  server_id        = data.azurerm_mssql_server.existing.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "255.255.255.255"
}