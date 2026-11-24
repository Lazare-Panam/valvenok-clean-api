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

# Resource group for the clean stack's app.
resource "azurerm_resource_group" "rg" {
  name     = "rg-valvenok-clean"
  location = var.location
}

# Reuse the EXISTING Container Apps environment (do not create a new one).

# The new front-door API container app.

