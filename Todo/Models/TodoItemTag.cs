﻿using Todo.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Web.Models
{
    public class TodoItemTag
    {
        public int TodoItemID { get; set; }
        public TodoItem TodoItem { get; set; }

        public int TagID { get; set; }
        public Tag Tag { get; set; }
    }
}