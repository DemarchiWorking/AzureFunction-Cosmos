using Infra.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class CosmosDb
    {
        private string ConnectionString = "AccountEndpoint=https://cosmos-db-infnet.documents.azure.com:443/;AccountKey=aJfYGYHj2M82OzRQHsv5JRSsrO8SqcRd9JSDKLFXYisXESaDheQIqdN2k7dHolDGacRIbYqSCx94SK7lcftIDg==;";
        private string Container = "todo-container";
        private string Database = "infnet-db";

        private CosmosClient CosmosClient { get; set; }

        public CosmosDb()
        {
            this.CosmosClient = new CosmosClient(this.ConnectionString);
        }

        public List<TodoItem> GetAll()
        {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");

            var result = new List<TodoItem>();

            var queryResult = container.GetItemQueryIterator<TodoItem>(queryDefinition);

            while (queryResult.HasMoreResults)
            {
                FeedResponse<TodoItem> currentResultSet = queryResult.ReadNextAsync().Result;
                result.AddRange(currentResultSet.Resource);
            }

            return result;

        }

        public TodoItem GetById(string id)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c where c.id = '{id}'");

            var queryResult = container.GetItemQueryIterator<TodoItem>(queryDefinition);

            TodoItem item = null;

            while (queryResult.HasMoreResults)
            {
                FeedResponse<TodoItem> currentResultSet = queryResult.ReadNextAsync().Result;
                item = currentResultSet.Resource.FirstOrDefault();
            }

            return item;
        }

        public async Task Save(TodoItem item)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.CreateItemAsync<TodoItem>(item, new PartitionKey(item.PartitionKey));
        }
        public async Task Delete(TodoItem item)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.DeleteItemAsync<TodoItem>(item.Id.ToString(), new PartitionKey(item.PartitionKey));
        }
        public async Task Update(TodoItem item)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.ReplaceItemAsync<TodoItem>(item, item.Id.ToString(), new PartitionKey(item.PartitionKey));
        }
    }
}
