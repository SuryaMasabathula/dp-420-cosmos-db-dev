using System;
using System.Linq;
using Microsoft.Azure.Cosmos;


string endpoint = "https://cosmosacctlab3.documents.azure.com:443/";
string key = "ZrKn4NoAiuAk8ePhNac123qMjyBFiMXPXOcKNjOdNEcbAnAd3TPOKwFRrD63Tyfdqy6cqfJ7DdISSMpTgeBMAQ==";


CosmosClient client= new (endpoint,key);

AccountProperties account= await client.ReadAccountAsync();
Console.WriteLine($"Account Name:\t{account.Id}");
Console.WriteLine($"Primary Region:\t{account.WritableRegions.FirstOrDefault()?.Name}");