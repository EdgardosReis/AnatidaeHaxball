using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatidaeHaxball.Repositories
{
    interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetSome(params object[] ids);
        T GetById(int id);
        void Add(T t);
        void Edit(T t);
        void Remove(object t);
        bool Exists(int id);
    }
}
