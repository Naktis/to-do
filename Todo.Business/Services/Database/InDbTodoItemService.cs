using Todo.Data.Context;
using Todo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Todo.Business.Services.Database
{
    public class InDbTodoItemService : IDataServiceAsync<TodoItemVo>
    {
        private readonly Data.Context.AppContext context;
        private readonly IMapper mapper;
        public InDbTodoItemService(Data.Context.AppContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Add(TodoItemVo item)
        {
            context.Add(entity: mapper.Map<TodoItemDao>(item));
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var todoItem = await context.TodoItems.FindAsync(id);
            context.TodoItems.Remove(todoItem);
            await context.SaveChangesAsync();
        }

        public async Task Edit(TodoItemVo changes)
        {
            context.Update(entity: mapper.Map<TodoItemDao>(changes));
            await context.SaveChangesAsync();
        }

        public async Task<TodoItemVo> Get(int id)
        {
            var todoItem = await context.TodoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            return mapper.Map<TodoItemVo>(todoItem);
        }

        public async Task<List<TodoItemVo>> GetAll()
        {
            var contextWithCategories = context.TodoItems.Include(t => t.Category);
            var todoItems = await contextWithCategories.ToListAsync();
            return mapper.Map<List<TodoItemVo>>(todoItems);
        }
        public bool Exists(int id)
        {
            return context.TodoItems.Any(e => e.ID == id);
        }

        public IEnumerable<TodoItemVo> GetEnum()
        {
            return mapper.Map<IEnumerable<TodoItemVo>>(context.TodoItems);
        }
    }
}