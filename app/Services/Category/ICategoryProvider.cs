using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.Services
{
    public interface ICategoryProvider
    {
        List<Category> GetAll();
        Category Get(int id);
        void Add(Category category);
        void Edit(int id, Category category);
        void Delete(int id);
    }
}
