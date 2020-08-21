using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Models;

namespace Todo.Business.Services
{
    public interface ITodoItemTagProviderAsync
    {
        Task<List<TodoItemTagVo>> GetAll();
        Task<TodoItemTagVo> Get(int tagID, int todoItemID);
        Task Add(TodoItemTagVo item);
        Task Edit(TodoItemTagVo oldTodoItemTag, TodoItemTagVo newTodoItemTag);
        Task Delete(TodoItemTagVo item);
        bool Exists(int tagID, int todoItemID);
    }
}
