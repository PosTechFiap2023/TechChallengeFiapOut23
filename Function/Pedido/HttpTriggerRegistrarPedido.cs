
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace Company.Function
{
    public class Pedidos : TableEntity{
        // public string PertitionKey { get; set; }
        // public string RowKey { get; set; }

        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string UserEmail { get; set; }
        public string Price { get; set; }

        public bool IsAproved { get; set; }
    }
    public static class HttpTriggerRegistrarPedido
    {
        [FunctionName("HttpTriggerRegistrarPedido")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "novopedido")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Pedidos>(requestBody);
            name = name ?? data?.name;

            // Get Storage Information
            var accountName = "rgfunctions9fe2";
            var accountKey = Environment.GetEnvironmentVariable("AzureStorageKey");

            // Set Auth
            var creds = new StorageCredentials(accountName, accountKey);
            var account = new CloudStorageAccount(creds, useHttps: true);

            // Connect to Storage
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("pedidos");

            Pedidos obj = new Pedidos()
            {
                PartitionKey = Guid.NewGuid().ToString(), // Must be unique
                RowKey = Guid.NewGuid().ToString(), // Must be unique
                OrderId = data?.OrderId,
                ProductId = data?.ProductId,
                UserEmail = data?.UserEmail,
                Price = data?.Price,
                IsAproved = data?.IsAproved,
            };

            var insertOperation = TableOperation.Insert(obj);
            await table.ExecuteAsync(insertOperation);

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response." + data
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);

        }
    }
}
