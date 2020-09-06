using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Api.Models
{
    public class Project
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int ClientID { get; set; }
        public Client Client { get; set; }
    }
}
