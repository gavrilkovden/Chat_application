using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IRepository<T>
    {
        T Create(T entity);
        T GetById(int id, Func<IQueryable<T>, IQueryable<T>> include = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>> include = null);
        IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> include = null);
        bool Delete(int userId);


    }
}
