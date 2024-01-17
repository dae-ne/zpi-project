param name string
param serverFarmName string
param location string

resource appServicePlan 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: serverFarmName
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
}

resource appService 'Microsoft.Web/sites@2023-01-01' = {
  name: name
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {}
  }
}
