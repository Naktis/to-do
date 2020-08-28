using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Data.Models
{
    public class TagVo : IID
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IList<TodoItemTagDao> TodoItemTags { get; set; }
    }
}
