using Finnova.Application.Commands.Clients;
using Finnova.Application.DTOs.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finnova.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginClientDto dto)
        {
            var command = new LoginClientCommand
            {
                Email = dto.Email,
                Password = dto.Password
            };

            var token = await _mediator.Send(command);

            return Ok(new { token });
        }

        [HttpPost("request-verification-code")]
        public async Task<IActionResult> RequestVerificationCode([FromBody] RequestVerificationCodeDto dto)
        {
            var command = new RequestVerificationCodeCommand { Email = dto.Email };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}