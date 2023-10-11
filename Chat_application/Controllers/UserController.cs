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

        // POST: creating user
        [HttpPost("create")]
        public ActionResult<UserDTO> CreateUser(string name)
        {
            try
            {
                var createdUser = _userService.CreateUser(name);

                if (createdUser != null)
                {
                    return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
                }
                else
                {
                    return BadRequest("User creation failed."); 
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: deleting user
        [HttpDelete("delete/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            try
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: getting a list of all users
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: getting user by id
        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            try
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}