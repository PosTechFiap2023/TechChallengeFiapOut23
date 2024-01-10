using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class DurableFunctionsOrchestrationCSharp1
    {
        [FunctionName("OrchFunction")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            var jobStatus = await context.CallActivityAsync<string>(nameof(RandomStatus), 2);
            if (jobStatus == "0")
            {
                outputs.Add(await context.CallActivityAsync<string>(nameof(Approval), "Aprovado"));
            }
            else{
                outputs.Add(await context.CallActivityAsync<string>(nameof(NotApproval), "Rejeitado"));
            }

            return outputs;
        }

        [FunctionName(nameof(Approval))]
        public static string Approval([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation("Seu pedido foi", name);
            return $"Seu pedido foi {name}!";
        }

        [FunctionName(nameof(NotApproval))]
        public static string NotApproval([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation("Seu pedido foi", name);
            return $"Seu pedido foi {name}!";
        }

        [FunctionName(nameof(RandomStatus))]
        public static string RandomStatus([ActivityTrigger] int number, ILogger log)
        {
            int result = RandomNumberGenerator.GetInt32(number);
            log.LogInformation("RandomNumberGenerator: ", number);
            return result.ToString();
        }

        [FunctionName("HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            string instanceId = await starter.StartNewAsync("OrchFunction", null);

            log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}