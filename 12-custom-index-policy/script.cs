using System;
using Microsoft.Azure.Cosmos;

string endpoint = "https://cosmosacctlab3.documents.azure.com:443/";

string key = "ZrKn4NoAiuAk8ePhNac123qMjyBFiMXPXOcKNjOdNEcbAnAd3TPOKwFRrD63Tyfdqy6cqfJ7DdISSMpTgeBMAQ==";

CosmosClient client = new CosmosClient(endpoint, key);

Database database = await client.CreateDatabaseIfNotExistsAsync("newcosmicworks");


IndexingPolicy policy = new ();
policy.IndexingMode = IndexingMode.Consistent;
policy.ExcludedPaths.Add(
    new ExcludedPath{ Path = "/*" }
);
policy.IncludedPaths.Add(
    new IncludedPath{ Path = "/name/?" }
);

ContainerProperties options = new ("products", "/categoryId");
options.IndexingPolicy = policy;

Container container = await database.CreateContainerIfNotExistsAsync(options);
Console.WriteLine($"Container Created [{container.Id}]");