using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic;
using ExceptionHandling.Exceptions;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccessLayer.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ChatDbContext _context;

        public BaseRepository(ChatDbContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }

            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T GetById(int id, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            if (id <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }

            var query = _context.Set<T>().AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            var entity = query.FirstOrDefault(m => m.Id == id);

            if (entity == null)
            {
                throw new ChatNotFoundException("Not found");
            }

            return entity;
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var listEntity = _context.Set<T>().AsQueryable();

            if (include != null)
            {
                listEntity = include(listEntity);
            }
            if (listEntity == null)
            {
                throw new ChatNotFoundException("Not found");
            }

            return listEntity;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var listEntity = _context.Set<T>().Where(filter).AsQueryable();

            if (include != null)
            {
                listEntity = include(listEntity);
            }

            if (listEntity == null)
            {
                throw new ChatNotFoundException("Not found");
            }

            return listEntity;
        }


        public virtual bool Delete(int id)
        {
            if (id <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }

            var entity = _context.Set<T>().Find(id);

            if (entity == null)
            {
                return false;
            }

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

            return true;
        }
    }
}
