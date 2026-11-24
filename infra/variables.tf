# Input variables. Real values live in terraform.tfvars (git-ignored);
# terraform.tfvars.example shows what to fill in.

variable "subscription_id" {
  type        = string
  description = "Azure subscription id (required by azurerm v4)."
}

variable "location" {
  type        = string
  description = "Region for the app's resource group. (The app itself runs in the environment's region, ukwest, regardless of this.)"
  default     = "uksouth"
}

variable "app_name" {
  type        = string
  description = "Name of the new Container App."
  default     = "valvenok-api-clean"
}

# ── Existing Container Apps environment to reuse ──────────────────────────
variable "container_app_environment_name" {
  type        = string
  description = "Name of the existing managed environment."
  default     = "managedEnvironment-rgpanamdev-87ae"
}

variable "container_app_environment_rg" {
  type        = string
  description = "Resource group that holds the existing managed environment."
  default     = "rg-panam-dev"
}

# ── Azure Container Registry ──────────────────────────────────────────────
variable "acr_login_server" {
  type        = string
  description = "ACR login server."
  default     = "dimages.azurecr.io"
}

variable "acr_username" {
  type        = string
  description = "ACR admin username."
  default     = "dimages"
}

variable "acr_password" {
  type        = string
  description = "ACR admin password (az acr credential show -n dimages)."
  sensitive   = true
}

variable "image" {
  type        = string
  description = "Full image reference to deploy."
  default     = "dimages.azurecr.io/valvenok-api-clean:v1"
}

# ── App config ────────────────────────────────────────────────────────────
variable "queue_name" {
  type        = string
  description = "Azure Storage queue name."
  default     = "notifications"
}

# ── Secrets (fill in terraform.tfvars) ────────────────────────────────────
variable "mongo_conn" {
  type        = string
  description = "MongoDB__ConnectionString"
  sensitive   = true
}

variable "sql_conn" {
  type        = string
  description = "ConnectionStrings__DefaultConnection"
  sensitive   = true
}

variable "storage_conn" {
  type        = string
  description = "AzureStorage__ConnectionString"
  sensitive   = true
}

variable "queue_conn" {
  type        = string
  description = "Queue__ConnectionString"
  sensitive   = true
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