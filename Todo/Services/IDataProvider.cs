using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Services
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
