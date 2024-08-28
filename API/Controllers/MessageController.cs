using Domain.Commands.v1.Messages.SendMessage;
using Domain.Queries.v1.Messages.GetChat;
using Domain.Queries.v1.Users.GetUsersByFilter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/messages")]
    public class MessageController : ControllerBase
    {
        public IMediator Mediator { get; set; }
        public ILogger Logger { get; set; }

        public MessageController(
            IMediator mediator,
            ILoggerFactory logger)
        {
            Mediator = mediator;
            Logger = logger.CreateLogger<MessageController>();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageCommand request)
        {
            try
            {
                var result = await Mediator.Send(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("chat")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChat([FromHeader] string? sender, string? reciver)
        {
            try
            {
                var result = await Mediator.Send(new GetChatQuery() { Sender = sender, Reciver = reciver });
                result.Messages = result.Messages.OrderBy(x => x.SendDate);
                return result.Messages.Any() ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
