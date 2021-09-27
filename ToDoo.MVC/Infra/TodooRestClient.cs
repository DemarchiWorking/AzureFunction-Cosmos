using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace ToDoo.MVC.Infra
{
    public class TodooRestClient
    {
        private string URL_Todoo_REST = "https://azgettodos.azurewebsites.net/api";
        public IList<TodooModel> GetAll()
        {

            var client = new RestClient(URL_Todoo_REST);

            var request = new RestRequest("GetTodos", DataFormat.Json);

            var response = client.Get<IList<TodooModel>>(request);

            return response.Data;
        }
        public TodooModel GetById(Guid id)
        {
            var client = new RestClient(URL_Todoo_REST);

            var request = new RestRequest($"GetByIdFunction?id={id}", DataFormat.Json);
            //request.AddParameter("id", id.ToString());

            var response = client.Get<TodooModel>(request);

            return response.Data;

        }
        public void Save(TodooModel model)
        {
            var client = new RestClient(URL_Todoo_REST);
            var request = new RestRequest($"SaveTodos", DataFormat.Json);

            request.AddJsonBody(model);
            var response = client.Post<TodooModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception("Não foi possivel criar a tarefa. ");


        }

        public void Delete(Guid id)
        {
            var client = new RestClient(URL_Todoo_REST);

            var request = new RestRequest($"Delete?id={id}", DataFormat.Json);
            //request.AddParameter("id", id.ToString());

            var response = client.Delete(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Não foi possivel deletar a tarefa. ");

        }

        public void Update(TodooModel model)
        {
            var client = new RestClient(URL_Todoo_REST);
            var request = new RestRequest($"Update", DataFormat.Json);

            request.AddJsonBody(model);
            var response = client.Put<TodooModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Não foi possivel criar a tarefa. ");

        }
    }
}
