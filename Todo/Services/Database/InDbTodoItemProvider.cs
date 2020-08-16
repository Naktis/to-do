using Todo.Data;
using Todo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Services.Database
{
    public class InDbTodoItemProvider : IDataProviderAsync<TodoItem>
    {
        private readonly Data.AppContext context;
        public InDbTodoItemProvider(Data.AppContext context)
        {
            this.context = context;
        }

        public async void Add(TodoItem item)
        {
            context.Add(item);
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
    }
}