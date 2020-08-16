using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Services
{
    public class InMemoryDataProvider<TData> : IDataProvider<TData> where TData : IID
    {
        public static int maxID = -1;

        private readonly List<TData> data;

        public InMemoryDataProvider(List<TData> newData)
        {
            data = newData;
        }

        public InMemoryDataProvider() : this(new List<TData>()) { }

        public virtual void Add(TData item)
        {
            maxID++;
            item.ID = maxID;
            data.Add(item);
        }

        public virtual void Delete(int id)
        {
            data.Remove(GetItemByID(id));
        }

        public virtual void Edit(TData item)
        {
            data.Remove(GetItemByID(item.ID));
            data.Add(item);
        }

        public TData Get(int id)
        {
            return GetItemByID(id);
        }

        public List<TData> GetAll()
        {
            return data;
        }

        private TData GetItemByID(int id)
        {
            TData item = data.Find(x => x.ID == id);
            if (item == null)
            {
                throw new IDNotFoundException(item.ID);
            }
            return item;
        }
    }
}
