using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.generic;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        //     private readonly IMessageRepository _messageRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDTO CreateUser(string name)
        {
            var newUser = _userRepository.Create(new UserEntity { UserName = name });

            //Creating a UserDTO based on a new user
            var userDTO = new UserDTO
            {
                Id = newUser.Id,
                UserName = newUser.UserName
            };

            return userDTO;
        }

        public bool DeleteUser(int userId)
        {
            return _userRepository.Delete(userId);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            var userDTOs = users.Select(userEntity => new UserDTO
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
            });
            //           var users1 = _messageRepository.GetMessageChatById(1);
            return userDTOs;
        }

        public UserDTO GetUserById(int userId)
        {
            var userEntity = _userRepository.GetById(userId);

            var userDTO = new UserDTO
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
            };

            return userDTO;
        }
    }
}
