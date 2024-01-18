param storageAccountName string
param appServicePlanName string
param appServiceName string
param keyVaultName string
param emailHost string
param emailPort string
param emailUsername string
param emailAddress string
param logoUrl string
param avatarContainerName string
param imageContainerName string

param location string = resourceGroup().location

var defaultDbConnectionStringSecretName = 'ConnectionStrings-DefaultDb'
var emailPasswordSecretName = 'Email-Password'

// kv references don't work (probably because of the free tier)
resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyVaultName
}

module storageAccount 'modules/storageAccount.bicep' = {
  name: 'storageAccountDeployment'
  params: {
    name: storageAccountName
    location: location
  }
}

module appService 'modules/appService.bicep' = {
  name: 'appServiceDeployment'
  params: {
    name: appServiceName
    serverFarmName: appServicePlanName
    storageAccountName: storageAccount.outputs.storageAccountName
    emailHost: emailHost
    emailPort: emailPort
    emailUsername: emailUsername
    emailAddress: emailAddress
    logoUrl: logoUrl
    avatarContainerName: avatarContainerName
    imageContainerName: imageContainerName
    emailPassword: keyVault.getSecret(emailPasswordSecretName)
    defaultDbConnectionString: keyVault.getSecret(defaultDbConnectionStringSecretName)
    location: location
  }
}

output appServiceName string = appServiceName
