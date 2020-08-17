using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Web.Services
{
    public interface IDataProvider<TData>
    {
        List<TData> GetAll();
        TData Get(int id);
        void Add(TData item);
        void Edit(TData item);
        void Delete(int id);
    }
}
