using Todo.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Business.Models
{
    public class TagDao : IID
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IList<TodoItemTagDao> TodoItemTags { get; set; }
    }
}
