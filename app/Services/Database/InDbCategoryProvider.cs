using app.Data;
using app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Services
{
    public class InDbCategoryProvider : IDataProvider<Category>
    {
        public static int maxID = -1;
        private readonly appContext context;
        public InDbCategoryProvider(appContext context) : base()
        {
            this.context = context;
        }

        public void Add(Category item)
        {
            maxID++;
            item.ID = maxID;
            context.Categories.Add(item);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Categories.Remove(GetItemByID(id));
            context.SaveChanges();
        }

        public void Edit(int id, Category changes)
        {
            context.Update(changes);
            context.SaveChanges();
        }

        public Category Get(int id)
        {
            return GetItemByID(id);
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        private Category GetItemByID(int id)
        {
            Category item = context.Categories.FirstOrDefault(x => x.ID == id);
            if (item == null)
            {
                throw new IDNotFoundException(item.ID);
            }
            return item;
        }
    }
}