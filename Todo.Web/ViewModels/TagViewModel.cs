using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Models;

namespace Todo.Web.ViewModels
{
    public class TagViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IList<TodoItemTagDao> TodoItemTags { get; set; }
    }
}
