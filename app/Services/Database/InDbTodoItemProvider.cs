using app.Data;
using app.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Services.Database
{
    public class InDbTodoItemProvider : IDataProviderAsync<TodoItem>
    {
        public static int maxID = -1;
        private readonly Data.AppContext context;
        public InDbTodoItemProvider(Data.AppContext context)
        {
            this.context = context;
        }

        public async void Add(TodoItem item)
        {
            maxID++;
            item.ID = maxID;
            context.TodoItems.Add(item);
            await context.SaveChangesAsync();
        }

        public async void Delete(int id)
        {
            var todoItem = await context.TodoItems.FindAsync(id);
            context.TodoItems.Remove(todoItem);
            await context.SaveChangesAsync();
        }

        public async void Edit(int id, TodoItem changes)
        {
            context.Update(changes);
            await context.SaveChangesAsync();
        }

        public async Task<TodoItem> Get(int id)
        {
            return await context.TodoItems.FindAsync(id);
        }

        public async Task<List<TodoItem>> GetAll()
        {
            return await context.TodoItems.ToListAsync();
        }

        public bool Exists(int id)
        {
            return context.TodoItems.Any(e => e.ID == id);
        }
    }
}