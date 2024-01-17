param name string
param serverFarmName string
param storageAccountName string
param keyVaultName string
param emailHost string
param emailPort string
param emailUsername string
param emailAddress string
param logoUrl string
param avatarContainerName string
param imageContainerName string
param location string

var defaultDbConnectionStringSecretName = 'ConnectionStrings-DefaultDb'

resource storageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' existing = {
  name: storageAccountName
}

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
    siteConfig: {
      appSettings: [
        {
          name: 'Email:Host'
          value: emailHost
        }
        {
          name: 'Email:Port'
          value: emailPort
        }
        {
          name: 'Email:Username'
          value: emailUsername
        }
        {
          name: 'Email:Address'
          value: emailAddress
        }
        {
          name: 'Email:LogoUrl'
          value: logoUrl
        }
        {
          name: 'ImageStorage:AvatarContainerName'
          value: avatarContainerName
        }
        {
          name: 'ImageStorage:ImageContainerName'
          value: imageContainerName
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultDB'
          connectionString: '@Microsoft.KeyVault(SecretUri=https://${keyVaultName}.vault.azure.net/secrets/${defaultDbConnectionStringSecretName})'
          type: 'Custom'
        }
        {
          name: 'ImageStorage'
          connectionString: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};AccountKey=${storageAccount.listKeys().keys[0].value};EndpointSuffix=core.windows.net'
          type: 'Custom'
        }
      ]
    }
  }
}
