using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Web.Models
{
    public enum TodoItemStatus
    {
        Backlog, //default value since it is the first enumerator
        Wip,
        Done,
        Archived
    }
}
