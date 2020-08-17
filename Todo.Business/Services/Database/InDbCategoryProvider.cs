using Todo.Business.Data;
using Todo.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Business.Services
{
    public class InDbCategoryProvider : IDataProviderAsync<CategoryDao>
    {
        private readonly Data.AppContext context;
        public InDbCategoryProvider(Data.AppContext context)
        {
            this.context = context;
        }

        public async void Add(CategoryDao item)
        {
            context.Add(item);
            await context.SaveChangesAsync();
        }

        public async void Delete(int id)
        {
            var category = await context.Categories.FindAsync(id);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        public async void Edit(int id, CategoryDao changes)
        {
            context.Update(changes);
            await context.SaveChangesAsync();
        }

        public async Task<CategoryDao> Get(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<List<CategoryDao>> GetAll()
        {
            return await context.Categories.ToListAsync();
        }

        public bool Exists(int id)
        {
            return context.Categories.Any(e => e.ID == id);
        }
    }
}