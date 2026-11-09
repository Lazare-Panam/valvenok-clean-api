# Input variables. Real values live in terraform.tfvars (gitignored);
# terraform.tfvars.example shows what to fill in.

variable "subscription_id" {
  type        = string
  description = "Azure subscription id (required by azurerm v4)."
}

variable "location" {
  type        = string
  description = "Region for the resource group."
  default     = "uksouth"
}

variable "sql_server_name" {
  type        = string
  description = "SQL Server name"
  default     = "panamv"
}
variable "sql_server_resource_group" {
  type        = string
  description = "SQL Server Resource Group Name"
  default     = "rg-pv-dev"
}
variable "sql_database_name" {
  type        = string
  description = "Azure SQL Database Name"
  default     = "ValvenokClean"
}

