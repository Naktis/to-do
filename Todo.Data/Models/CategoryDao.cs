using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Business.Services;

namespace Todo.Business.Models
{
    public class CategoryDao : IID
    { 
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
