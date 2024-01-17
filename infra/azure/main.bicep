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
    keyVaultName: keyVaultName
    emailHost: emailHost
    emailPort: emailPort
    emailUsername: emailUsername
    emailAddress: emailAddress
    logoUrl: logoUrl
    avatarContainerName: avatarContainerName
    imageContainerName: imageContainerName
    location: location
  }
}

output appServiceName string = appServiceName
