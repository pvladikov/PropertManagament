using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagamentDatabase.Interface
{
    public interface  IDatabase<T> where T : class, new()
    {
        bool Delete(T item);
        void DeleteAll();
        IQueryable<T> Query { get; set; }
        void Add(T item);
    }
}
