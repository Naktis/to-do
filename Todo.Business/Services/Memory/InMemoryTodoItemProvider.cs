using Todo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Business.Services
{
    public class InMemoryTodoItemProvider : InMemoryDataProvider<TodoItemVo>
{
    public InMemoryTodoItemProvider() : base()
    {
        Add(new TodoItemVo() { ID = 0, Name = "Exercise", Description = "Catch a pigeon" });
        Add(new TodoItemVo() { ID = 1, Name = "Make dinner", Description = "Try roasted rocks", Priority = 2 });
    }
}
}
