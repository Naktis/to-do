using app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Services
{
    public interface ITodoItemProvider
    {
        List<TodoItem> GetAll();
        TodoItem Get(int id);
        void Add(TodoItem todoItem);
        void Edit(int id, TodoItem todoItem);
        void Delete(int id);
    }
}
