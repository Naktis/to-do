using app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class InMemoryTodoItemProvider : ITodoItemProvider
    {
        private List<TodoItem> data = new List<TodoItem>()
        {
            // Sample data
            new TodoItem(){Name="Exercise", Description="Catch a pigeon"},
            new TodoItem(){Name="Make dinner",Description="Try roasted rocks", Priority=2},
            new TodoItem(){Name="Midnight routine", Description="Do a backflip", Priority=1}
        };
        public void Add(TodoItem todoItem)
        {
            data.Add(todoItem);
        }

        public void Delete(int id)
        {
            // This action calls the constructor and increases MaxID value
            data.RemoveAt(getIndexByID(id));
            TodoItem.decreaseMaxID(); // So we need to get it back
        }

        public void Edit(int id, TodoItem todoItem)
        {
            data[getIndexByID(id)].Copy(todoItem);
            TodoItem.decreaseMaxID(); // Stabilize numbering again
        }

        public TodoItem Get(int id)
        {
            return data[getIndexByID(id)];
        }

        public List<TodoItem> GetAll()
        {
            return data;
        }

        private int getIndexByID(int id)
        {
            return data.FindIndex(x => x.ID == id);
        }
    }
}
