using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Business.Models;

namespace Todo.Web.ViewModels
{
    public class TodoItemTagViewModel
    {
        public int TodoItemID { get; set; }
        public TodoItemDao TodoItem { get; set; }

        public int TagID { get; set; }
        public TagDao Tag { get; set; }
    }
}
