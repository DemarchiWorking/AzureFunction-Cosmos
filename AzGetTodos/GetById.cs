using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Infra.Repository;

namespace AzGetTodos
{
    public static class GetById
    {
        [FunctionName("GetByIdFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Guid id = new Guid(req.Query["id"]);
            var repository = new CosmosDb();
            var todo = repository.GetById(id.ToString());

            if (todo == null)
                return new NotFoundObjectResult(new { message = "Tarefa não encontrada" });

            return new OkObjectResult(todo);
        }
    }
}
