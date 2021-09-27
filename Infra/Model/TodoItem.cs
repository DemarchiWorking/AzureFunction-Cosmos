using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infra.Model
{
    public class TodoItem
    {
        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }
        [Required]
        [JsonProperty(PropertyName = "assignedFor")]
        public String AssignedFor { get; set; }

        [JsonProperty(PropertyName = "status")]
        public State Status { get; set; }

        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; } = "todo";
    }

    public enum State
    {
        Backlog = 1,
        InProgress = 2,
        Done = 3
    }
}
