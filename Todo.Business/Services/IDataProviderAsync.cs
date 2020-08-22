using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Business.Services
{
    public interface IDataProviderAsync<TData>
    {
        Task<List<TData>> GetAll();
        Task<TData> Get(int id);
        Task Add(TData item);
        Task Edit(TData item);
        Task Delete(int id);
        bool Exists(int id);
        IEnumerable<TData> GetEnum();
    }
}
