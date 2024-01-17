param storageAccountName string
param appServicePlanName string
param appServiceName string

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
    location: location
  }
}

output appServiceName string = appServiceName
