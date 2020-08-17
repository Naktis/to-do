using Todo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Business.Services
{
    public class InMemoryCategoryProvider : InMemoryDataProvider<CategoryDao>
    {
        public InMemoryCategoryProvider() : base()
        {
            Add(new CategoryDao() { Name = "Work" });
            Add(new CategoryDao() { Name = "Personal" });
        }
    }
}
