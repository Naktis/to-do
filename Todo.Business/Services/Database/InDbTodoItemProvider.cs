using Todo.Business.Data;
using Todo.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Business.Services.Database
{
    public class InDbTodoItemProvider : IDataProviderAsync<TodoItemDao>
    {
        private readonly Data.AppContext context;
        public InDbTodoItemProvider(Data.AppContext context)
        {
            this.context = context;
        }

        public async void Add(TodoItemDao item)
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

        public async void Edit(int id, TodoItemDao changes)
        {
            context.Update(changes);
            await context.SaveChangesAsync();
        }

        public async Task<TodoItemDao> Get(int id)
        {
            return await context.TodoItems.FindAsync(id);
        }

        public async Task<List<TodoItemDao>> GetAll()
        {
            return await context.TodoItems.ToListAsync();
        }
    }
}