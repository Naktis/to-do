using Todo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Services
{
    public class InMemoryTodoItemProvider : InMemoryDataProvider<TodoItem>
{
    public InMemoryTodoItemProvider() : base()
    {
        Add(new TodoItem() { ID = 0, Name = "Exercise", Description = "Catch a pigeon" });
        Add(new TodoItem() { ID = 1, Name = "Make dinner", Description = "Try roasted rocks", Priority = 2 });
    }
}
}
