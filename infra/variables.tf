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
