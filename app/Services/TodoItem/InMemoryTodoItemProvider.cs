using app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class InMemoryTodoItemProvider : ITodoItemProvider
    {
        public static int maxID = 2; // ID={0,1,2} are reserved for samples below

        private List<TodoItem> data = new List<TodoItem>()
        {
            // Sample data
            new TodoItem(){ID=0, Name="Exercise", Description="Catch a pigeon"},
            new TodoItem(){ID=1, Name="Make dinner",Description="Try roasted rocks", Priority=2},
            new TodoItem(){ID=2, Name="Midnight routine", Description="Do a backflip", Priority=1}
        };
        public void Add(TodoItem todoItem)
        {
            maxID++;
            todoItem.ID = maxID;
            data.Add(todoItem);
        }

        public void Delete(int id)
        {
            data.Remove(GetItemByID(id));
        }

        public void Edit(int id, TodoItem todoItem)
        {
            GetItemByID(id).Copy(todoItem);
        }

        public TodoItem Get(int id)
        {
            return GetItemByID(id);
        }

        public List<TodoItem> GetAll()
        {
            return data;
        }

        private TodoItem GetItemByID(int id)
        {
            return data.Find(x => x.ID == id);
        }
    }
}
