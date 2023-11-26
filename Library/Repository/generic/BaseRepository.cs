using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic;
using DataAccessLayer.Repository.generic.Interfaces;
using ExceptionHandling.Exceptions;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccessLayer.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ChatDbContext _context;
        protected readonly IUnitOfWork _unitOfWork;


        public BaseRepository(ChatDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public virtual T Create(T entity)
        {
            if (entity == null)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }

            _context.Set<T>().Add(entity);
            return entity;
        }

        public T GetById(int id, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            if (id <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }

            var query = _context.Set<T>().AsQueryable().ApplyInclude(include);

            var entity = query.FirstOrDefault(m => m.Id == id);

            return entity;
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var listEntity = _context.Set<T>().AsQueryable().ApplyInclude(include);

            return listEntity;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var listEntity = _context.Set<T>().Where(filter).AsQueryable().ApplyInclude(include);

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

            return true;
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }
    }

    public static class RepositoryExtensions
    {
        public static IQueryable<T> ApplyInclude<T>(this IQueryable<T> query, Func<IQueryable<T>, IQueryable<T>> include)
        {
            return include != null ? include(query) : query;
        }
    }
}
