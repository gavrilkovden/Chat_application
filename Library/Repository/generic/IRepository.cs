using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IRepository<T>
    {
        T Create(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool IsValidEntity(T entity);
    }
}
