using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services;

namespace Todo.Models
{
    public class Category : IID
    { 
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
