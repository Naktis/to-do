using Todo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Business.Services
{
    public class InMemoryTodoItemProvider : InMemoryDataProvider<TodoItemDao>
{
    public InMemoryTodoItemProvider() : base()
    {
        Add(new TodoItemDao() { ID = 0, Name = "Exercise", Description = "Catch a pigeon" });
        Add(new TodoItemDao() { ID = 1, Name = "Make dinner", Description = "Try roasted rocks", Priority = 2 });
    }
}
}
