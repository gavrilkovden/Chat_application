using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataAccessLayer.EntityDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly Context _context; 

        public UserService(Context context)
        {
            _context = context;
        }

        public UserDTO CreateUser(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid input parameters.");
            }
            // checking whether a user with the same name exists
            var existingUser = _context.DALUser.FirstOrDefault(u => u.UserName == name);

            if (existingUser != null)
            {
                throw new Exception("User with the same name already exists.");
            }

            // Creating a new user
            var newUser = new UserEntity
            {
                UserName = name
            };

            // Adding a user to the database context
            _context.DALUser.Add(newUser);
            _context.SaveChanges();

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
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid input parameters.");
            }
            // search for a user by his ID
            var userEntity = _context.DALUser.FirstOrDefault(u => u.Id == userId);

            if (userEntity == null)
            {
                return false;
            }

            _context.DALUser.Remove(userEntity);
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            // Query all users from the database
            var users = _context.DALUser.ToList();

            if (users == null)
            {
                throw new NotFoundException("Users not found");
            }

            var userDTOs = users.Select(userEntity => new UserDTO
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
            });

            return userDTOs;
        }

        public UserDTO GetUserById(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            //  query to the database to search for a user by his ID
            var userEntity = _context.DALUser.FirstOrDefault(u => u.Id == userId);

            if (userEntity == null)
            {
                throw new NotFoundException("Users not found");
            }

            var userDTO = new UserDTO
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
            };

            return userDTO;
        }
    }
}
