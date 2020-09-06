using Todo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Todo.Business.Services.Database
{
    public class InDbTodoItemTagService : ITodoItemTagServiceAsync
    {
        private readonly Data.Context.AppContext context;
        private readonly IMapper mapper;
        public InDbTodoItemTagService(Data.Context.AppContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Add(TodoItemTagVo item)
        {
            context.Add(entity: mapper.Map<TodoItemTagDao>(item));
            await context.SaveChangesAsync();
        }

        public async Task Delete(TodoItemTagVo item)
        {
            context.TodoItemTag.Remove(entity: mapper.Map<TodoItemTagDao>(item));
            await context.SaveChangesAsync();
        }

        public async Task Edit(TodoItemTagVo oldTodoItemTag, TodoItemTagVo newTodoItemTag)
        {
            //context.Update(entity: mapper.Map<TodoItemTagDao>(changes));
            context.Remove(entity: mapper.Map<TodoItemTagDao>(oldTodoItemTag));
            context.Add(entity: mapper.Map<TodoItemTagDao>(newTodoItemTag));
            await context.SaveChangesAsync();
        }

        public async Task<TodoItemTagVo> Get(int tagID, int todoItemID)
        {
            var todoItemTag = await context.TodoItemTag
                .Include(s => s.TodoItem)
                .Include(s => s.Tag)
                .FirstOrDefaultAsync(m => m.TodoItemID == todoItemID && m.TagID == tagID);
            return mapper.Map<TodoItemTagVo>(todoItemTag);
        }

        public async Task<List<TodoItemTagVo>> GetAll()
        {
            var contextWithTags = context.TodoItemTag.Include(t => t.Tag).Include(t => t.TodoItem);
            var tags = await contextWithTags.ToListAsync();
            return mapper.Map<List<TodoItemTagVo>>(tags);
        }

        public bool Exists(int tagID, int todoItemID)
        {
            return context.TodoItemTag.Any(e => (e.TagID == tagID && e.TodoItemID == todoItemID));
        }
    }
}