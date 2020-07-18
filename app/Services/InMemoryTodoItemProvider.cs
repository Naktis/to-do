using app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Services
{
    public class InMemoryTodoItemProvider : ITodoItemProvider
    {
        private List<TodoItem> data = new List<TodoItem>()
        {
            // Sample data
            new TodoItem(){Name="Rename Elon Musk's child", Description="Do not use numbers"},
            new TodoItem(){Name="Who lives in a pineapple under the sea",Description="Spongebob Squarepants", Priority=2},
            new TodoItem(){Name="Eat lunch", Description="Try microwaving ants", Priority=1}
        };

        public List<TodoItem> GetAll()
        {
            return data;
        }

        public TodoItem Get(int id)
        {
            return data[id];
        }

        public void Add(TodoItem todoItem)
        {
            data.Add(todoItem);
        }

        public void Edit(int id, TodoItem todoItem)
        {
            // Get an index where the item with a specific ID is stored
            int index = data.FindIndex(item => item.ID == id);
            // Replace its contents
            data[index] = data[index].Copy(todoItem);
        }

        public void Delete(int id)
        {
            // Search for an item with specific ID and remove it
            data.RemoveAt(data.FindIndex(item => item.ID == id));
        }
    }
}
