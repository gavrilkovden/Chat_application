using DataAccessLayer.EntityDB;
using ExceptionHandling.Exceptions;

namespace DataAccessLayer.Repository.generic
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ChatDbContext context) : base(context)
        {
        }
    }
}
