﻿using Todo.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Web.Models
{
    public class Tag : IID
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IList<TodoItemTag> TodoItemTags { get; set; }
    }
}