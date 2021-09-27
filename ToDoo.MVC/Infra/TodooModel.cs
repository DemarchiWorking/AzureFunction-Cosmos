using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoo.MVC.Infra
{
    public class TodooModel
    {
        public Guid Id { get; set; }

        public string AssignedFor { get; set; }

        public State Status { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
    public enum State
    {
        Backlog = 1,
        InProgress = 2,
        Done = 3
    }
}

