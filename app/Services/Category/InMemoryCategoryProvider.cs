using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.Services
{
    public class InMemoryCategoryProvider : ICategoryProvider
    {
        public static int maxID = 2; // ID={0,1,2} are reserved for samples below

        private List<Category> data = new List<Category>()
        {
            // Sample data
            new Category(){ID=0, Name="Work"},
            new Category(){ID=1, Name="Personal"},
            new Category(){ID=2, Name="Learning"}
        };

        public void Add(Category category)
        {
            maxID++;
            category.ID = maxID;
            data.Add(category);
        }

        public void Delete(int id)
        {
            data.Remove(GetCategoryByID(id));
        }

        public void Edit(int id, Category category)
        {
            GetCategoryByID(id).Name = category.Name;
        }

        public Category Get(int id)
        {
            return GetCategoryByID(id);
        }

        public List<Category> GetAll()
        {
            return data;
        }

        private Category GetCategoryByID(int id)
        {
            return data.Find(x => x.ID == id);
        }
    }
}
