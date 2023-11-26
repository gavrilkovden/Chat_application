using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic.Interfaces;
using ExceptionHandling.Exceptions;

namespace DataAccessLayer.Repository.generic
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ChatDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
