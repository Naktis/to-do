using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Todo.Data.Models;

namespace Todo.Business.Services
{
    public class InFileDataProvider<TData> : InMemoryDataProvider<TData> where TData : IID
    {
        private readonly string fileName;

        public InFileDataProvider(string fileName) : base(ReadFile(fileName))
        {
            this.fileName = fileName;
        }

        #region IDataProvider

        public override void Add(TData item)
        {
            base.Add(item);
            SaveData(GetAll(), fileName);
        }

        public override void Edit(TData item)
        {
            base.Edit(item);
            SaveData(GetAll(), fileName);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
            SaveData(GetAll(), fileName);
        }

        #endregion

        #region DataReaderWriter

        private static List<TData> ReadFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string data = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<List<TData>>(data);
            }
            return new List<TData>();
        }

        private static void SaveData(List<TData> data, string fileName)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(fileName, jsonData);
        }

        #endregion
    }
}
