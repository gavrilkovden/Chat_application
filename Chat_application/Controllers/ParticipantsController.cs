using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat_application.Controllers
{
    [ApiController]
    [Route("api/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantsService _participantsService;

        public ParticipantsController(IParticipantsService participantsService)
        {
            _participantsService = participantsService;
        }
        // GET: getting all participants for the selected chat
        [HttpGet("chat/{chatId}")]
        public IActionResult GetChatParticipants(int chatId)
        {
            try
            {
                var participants = _participantsService.GetChatParticipants(chatId);
                return Ok(participants);
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
