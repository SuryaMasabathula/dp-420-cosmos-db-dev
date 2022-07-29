using System;
using Microsoft.Azure.Cosmos;

string endpoint = "https://cosmosacctlab3.documents.azure.com:443/";
string key = "ZrKn4NoAiuAk8ePhNac123qMjyBFiMXPXOcKNjOdNEcbAnAd3TPOKwFRrD63Tyfdqy6cqfJ7DdISSMpTgeBMAQ==";

CosmosClient client = new CosmosClient(endpoint, key);
Database database= await client.CreateDatabaseIfNotExistsAsync("cosmicworks");
Container container= await database.CreateContainerIfNotExistsAsync("products","/categoryId", 400);
Console.WriteLine("..............Creating Data using Id and categoryId.....................");
// Adding a New Cosmos Item
Product saddle=new() {
    id = "706cd7c6-db8b-41f9-aea2-0e0c7e8eb009",
    categoryId = "9603ca6c-9e28-4a02-9194-51cdb7fea816",
    name = "Road Saddle",
    price = 45.99d,
    tags = new string[]
    {
        "tan",
        "new",
        "crisp"
    }
};

await container.CreateItemAsync<Product>(saddle);

Console.WriteLine("..............Reading Data using Id and categoryId.....................");
string id = "706cd7c6-db8b-41f9-aea2-0e0c7e8eb009";

string categoryId = "9603ca6c-9e28-4a02-9194-51cdb7fea816";
PartitionKey partitionKey = new (categoryId);

saddle = await container.ReadItemAsync<Product>(id, partitionKey);

Console.WriteLine($"[{saddle.id}]\t{saddle.name} ({saddle.price:C})");

Console.WriteLine("..............Reading Data using Id and categoryId Completed.....................");

Console.WriteLine("..............Update Data .....................");

saddle.price=1000.0d;
saddle.name="this is the update saddle name";
await container.UpsertItemAsync<Product>(saddle);

Console.WriteLine("..............Update Data completed....................");

Console.WriteLine("..............Delete Data using Id and categoryId.completed....................");

await container.DeleteItemAsync<Product>(id,partitionKey);

saddle=await container.ReadItemAsync<Product>(id,partitionKey);
if(saddle is null)
{
Console.WriteLine("..............Delete Data using Id and categoryId.completed....................");
}
else
Console.WriteLine("..............Delete Data using Id and categoryId.Failed....................");
