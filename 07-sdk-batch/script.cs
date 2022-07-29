using System;
using Microsoft.Azure.Cosmos;

string endpoint = "https://cosmosacctlab3.documents.azure.com:443/";
string key = "ZrKn4NoAiuAk8ePhNac123qMjyBFiMXPXOcKNjOdNEcbAnAd3TPOKwFRrD63Tyfdqy6cqfJ7DdISSMpTgeBMAQ==";

CosmosClient client = new CosmosClient(endpoint, key);
    
Database database = await client.CreateDatabaseIfNotExistsAsync("cosmicworks");
Container container = await database.CreateContainerIfNotExistsAsync("products", "/categoryId", 400);
// Product saddle = new("0120", "Worn Saddle", "9603ca6c-9e28-4a02-9194-51cdb7fea816");
// Product handlebar = new("012A", "Rusty Handlebar", "9603ca6c-9e28-4a02-9194-51cdb7fea816");

// PartitionKey partitionKey = new ("9603ca6c-9e28-4a02-9194-51cdb7fea816");
// TransactionalBatch batch = container.CreateTransactionalBatch(partitionKey)
//     .CreateItem<Product>(saddle)
//     .CreateItem<Product>(handlebar);


Product light = new("012B", "Flickering Strobe Light", "9603ca6c-9e28-4a02-9194-51cdb7fea816");
Product helmet = new("012C", "New Helmet", "0feee2e4-687a-4d69-b64e-be36afc33e74");

PartitionKey partitionKey = new ("9603ca6c-9e28-4a02-9194-51cdb7fea816");

TransactionalBatch batch = container.CreateTransactionalBatch(partitionKey)
    .CreateItem<Product>(light)
    .CreateItem<Product>(helmet);

    using TransactionalBatchResponse response = await batch.ExecuteAsync();

    Console.WriteLine($"Status:\t{response.StatusCode}");