using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat_application.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser(string name)
        {
            var createdUser = _userService.CreateUser(name);

            if (createdUser != null)
            {
                return Ok(createdUser);
            }
            else
            {
                return BadRequest("User creation failed.");
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            if (_userService.DeleteUser(userId))
            {
                return Ok("User deleted successfully.");
            }
            else
            {
                return NotFound("User not found or you don't have permission to delete.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("User not found.");
            }
        }
    }
}