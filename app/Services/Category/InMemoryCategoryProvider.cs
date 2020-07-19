using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.Services
{
    public class InMemoryCategoryProvider : ICategoryProvider
    {
        private List<Category> data = new List<Category>()
        {
            // Sample data
            new Category(){Name="Work"},
            new Category(){Name="Personal"},
            new Category(){Name="Learning"}
        };
        public void Add(Category category)
        {
            data.Add(category);
        }

        public void Delete(int id)
        {
            // This action calls the constructor and increases MaxID value
            data.RemoveAt(getIndexByID(id));
            Category.decreaseMaxID(); // So we need to get it back
        }

        public void Edit(int id, Category category)
        {
            data[getIndexByID(id)].Name = category.Name;
            Category.decreaseMaxID(); // Stabilize numbering again
        }

        public Category Get(int id)
        {
            return data[getIndexByID(id)];
        }

        public List<Category> GetAll()
        {
            return data;
        }

        private int getIndexByID(int id)
        {
            return data.FindIndex(x => x.ID == id);
        }
    }
}
