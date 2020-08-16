using Todo.Web.Data;
using Todo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Web.Services
{
    public class InDbCategoryProvider : IDataProviderAsync<Category>
    {
        private readonly Data.AppContext context;
        public InDbCategoryProvider(Data.AppContext context)
        {
            this.context = context;
        }

        public async void Add(Category item)
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

        public async void Edit(int id, Category changes)
        {
            context.Update(changes);
            await context.SaveChangesAsync();
        }

        public async Task<Category> Get(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetAll()
        {
            return await context.Categories.ToListAsync();
        }

        public bool Exists(int id)
        {
            return context.Categories.Any(e => e.ID == id);
        }
    }
}