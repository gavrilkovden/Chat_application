using DataAccessLayer.EntityDB;
using ExceptionHandling.Exceptions;

namespace DataAccessLayer.Repository.generic
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public bool DeleteUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }
            // search for a user by his ID
            var userEntity = _context.User.FirstOrDefault(u => u.Id == userId);

            if (userEntity == null)
            {
                return false;
            }

            _context.User.Remove(userEntity);
            _context.SaveChanges();

            return true;
        }

        public override bool IsValidEntity(UserEntity user)
        {
            return !string.IsNullOrEmpty(user.UserName);
        }
    }
}
