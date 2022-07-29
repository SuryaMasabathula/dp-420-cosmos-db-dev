using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Microsoft.Azure.Cosmos;

string endpoint = "https://cosmosacctlab3.documents.azure.com:443/";
string key = "ZrKn4NoAiuAk8ePhNac123qMjyBFiMXPXOcKNjOdNEcbAnAd3TPOKwFRrD63Tyfdqy6cqfJ7DdISSMpTgeBMAQ==";
CosmosClientOptions options = new () 
{ 
    AllowBulkExecution = true 
};

CosmosClient client = new (endpoint, key, options); 

Container container = client.GetContainer("cosmicworks", "products");

List<Product> productsToInsert = new Faker<Product>()
    .StrictMode(true)
    .RuleFor(o => o.id, f => Guid.NewGuid().ToString())
    .RuleFor(o => o.name, f => f.Commerce.ProductName())
    .RuleFor(o => o.price, f => Convert.ToDouble(f.Commerce.Price(max: 1000, min: 10, decimals: 2)))
    .RuleFor(o => o.categoryId, f => f.Commerce.Department(1))
    .Generate(25000);

    List<Task> concurrentTasks = new List<Task>();

    foreach(Product product in productsToInsert)
{    
    concurrentTasks.Add(
        container.CreateItemAsync(product, new PartitionKey(product.categoryId))
    );
}

await Task.WhenAll(concurrentTasks);   

Console.WriteLine("Bulk tasks complete");