using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Data.Models
{
    public class TodoItemTagVo
    {
        public int TodoItemID { get; set; }
        public TodoItemDao TodoItem { get; set; }

        public int TagID { get; set; }
        public TagDao Tag { get; set; }
    }
}
