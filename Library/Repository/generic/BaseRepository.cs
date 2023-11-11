using DataAccessLayer.EntityDB;
using ExceptionHandling.Exceptions;

namespace DataAccessLayer.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            if (!IsValidEntity(entity))
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }

            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T GetById(int id)
        {
            if (id <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }
            //  receive a message with the specified identifier
            var entity = _context.Set<T>().Find(id);

            if (entity == null)
            {
                throw new ChatNotFoundException("Not found");
            }

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            var listEntity = _context.Set<T>().ToList();
            if (listEntity.Count == 0)
            {
                throw new ChatNotFoundException("Not found");
            }
            return listEntity;

        }

        public virtual bool IsValidEntity(T entity)
        {
            return true;
        }
    }
}
