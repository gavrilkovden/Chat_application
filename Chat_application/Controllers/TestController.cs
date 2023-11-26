using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Chat_application.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IUserService _userService;

        public TestController(IUserService userService)
        {
            _userService = userService;
        }

        [CustomAuthorization("123")]
        [HttpPost("create")]
        public ActionResult<UserDTO> CreateUser([FromBody] string name)
        {
            // Если код в запросе прошел аутентификацию в атрибуте, продолжаем выполнение

            if (!string.IsNullOrEmpty(name))
            {
                // Аутентификация успешна, создайте пользователя
                var createdUser = _userService.CreateUser(name);

                if (createdUser != null)
                {
                    return Ok(createdUser);
                }
            }

            return BadRequest("Users creation failed.");
        }
    }

}
