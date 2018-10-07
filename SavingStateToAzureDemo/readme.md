# SavingStateDemoToAzure

This Visual Studio 2017 solution is a bot that saves state to ConversationData, PrivateConversationData, and UserData.

All state data is saved in to Azure storage, which is the default.

## Prerequisites

1. Create an Azure Storage Account
2. Copy a Connection String from the "keys" tab of the Storage Account
3. Add a Web.config file to this project
4. Add a connections.config file, as below:
```
    <configuration>
        <connectionStrings>
        <add name="BotAzureStorage"
          connectionString="xxxxxxxxxxx"
         />
        </connectionStrings>


