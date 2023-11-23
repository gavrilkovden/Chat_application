using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Chat_application.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_application.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;

        public UserController(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        //[AllowAnonymous]
        //[HttpPost("register")]
        //public ActionResult<string> Register([FromBody] string name)
        //{
        //    // Создайте пользователя и добавьте его в базу данных
        //    var newUser = _userService.CreateUser(name);

        //    // Здесь можно создать и вернуть JWT-токен для нового пользователя
        //    var token = _jwtTokenService.GenerateToken(newUser.UserName);

        //    return Ok(token);
        //}
     //   [Authorize]
        [HttpPost]
        public ActionResult<UserDTO> CreateUser(string name)
        {
            // Получите JWT-токен из заголовка запроса
   //         var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Проверьте аутентификацию с использованием JWT-токена
       //              bool isAuthenticated = _jwtTokenService.ValidateToken(jwtToken);

       //     if (isAuthenticated)
      //      {
                // Аутентификация успешна, создайте пользователя
                var createdUser = _userService.CreateUser(name);

                if (createdUser != null)
                {
                    return Ok(createdUser);
                }
                else
                {
                    return BadRequest("Users creation failed.");
                }
        //    }
       //     else
       //     {
                // Ошибка аутентификации
       //         return Unauthorized("Authentication failed.");
       //     }
        }

     //   [Authorize]
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            if (_userService.DeleteUser(userId))
            {
                return Ok("Users deleted successfully.");
            }
            else
            {
                return NotFound("Users not found or you don't have permission to delete.");
            }
        }
     //   [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

  //      [Authorize]
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
                return NotFound("Users not found.");
            }
        }
    }
}