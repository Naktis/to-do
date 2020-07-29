using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Services
{
    public interface IDataProviderAsync<TData>
    {
        Task<List<TData>> GetAll();
        Task<TData> Get(int id);
        void Add(TData item);
        void Edit(int id, TData item);
        void Delete(int id);
    }
}
