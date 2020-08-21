using Todo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Business.Services
{
    public class InMemoryCategoryProvider : InMemoryDataProvider<CategoryVo>
    {
        public InMemoryCategoryProvider() : base()
        {
            Add(new CategoryVo() { Name = "Work" });
            Add(new CategoryVo() { Name = "Personal" });
        }
    }
}
