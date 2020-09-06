using Todo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Todo.Business.Services
{
    public class InDbCategoryService : IDataServiceAsync<CategoryVo>
    {
        private readonly Data.Context.AppContext context;
        private readonly IMapper mapper;
        public InDbCategoryService(Data.Context.AppContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Add(CategoryVo item)
        {
            context.Add(entity: mapper.Map<CategoryDao>(item));
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await context.Categories.FindAsync(id);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        public async Task Edit(CategoryVo changes)
        {
            context.Update(entity: mapper.Map<CategoryDao>(changes));
            await context.SaveChangesAsync();
        }

        public async Task<CategoryVo> Get(int id)
        {
            var category = await context.Categories.FindAsync(id);
            return mapper.Map<CategoryVo>(category);
        }

        public async Task<List<CategoryVo>> GetAll()
        {
            var categories = await context.Categories.ToListAsync();
            return mapper.Map<List<CategoryVo>>(categories);
        }

        public bool Exists(int id)
        {
            return context.Categories.Any(e => e.ID == id);
        }
        public IEnumerable<CategoryVo> GetEnum()
        {
            return mapper.Map<IEnumerable<CategoryVo>>(context.Categories);
        }
    }
}