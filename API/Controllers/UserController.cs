using Domain.Commands.v1.Users.CreateUser;
using Domain.Commands.v1.Users.LoginUser;
using Domain.Queries.v1.Users.GetUserById;
using Domain.Queries.v1.Users.GetUsersByFilter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        public IMediator Mediator { get; set; }
        public ILogger Logger { get; set; }

        public UserController(
            IMediator mediator,
            ILoggerFactory logger)
        {
            Mediator = mediator;
            Logger = logger.CreateLogger<UserController>();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand request)
        {
            try 
            {
                var result = await Mediator.Send(request);
                return Created(result.Id , result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserBByFilter([FromHeader] string? username, string? email)
        {
            try
            {
                var result = await Mediator.Send(new GetUserByFilterQuery() { Email = email, Username = username});
                return result.Users.Count() > 0 ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            try
            {
                var result = await Mediator.Send(new GetUserByIdQuery() { Id = id});
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromHeader] string username, string password)
        {
            try
            {
                var result = await Mediator.Send(new LoginUserCommand() { Username = username, Password = password });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
