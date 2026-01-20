using Finnova.Application.Commands.Users;
using Finnova.Application.DTOs.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var command = new LoginUserCommand
            {
                Email = dto.Email,
                Password = dto.Password
            };

            var tokens = await _mediator.Send(command);

            return Ok(tokens);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
        {
            var command = new RefreshTokenCommand { RefreshToken = dto.RefreshToken };

            var tokens = _mediator.Send(command);

            return Ok(tokens);
        }

        [HttpPost("request-verification-code")]
        public async Task<IActionResult> RequestVerificationCode([FromBody] RequestVerificationCodeDto dto)
        {
            var command = new RequestVerificationCodeCommand { Email = dto.Email };

            await _mediator.Send(command);
            
            return NoContent();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshTokenDto dto)
        {
            var command = new LogoutUserCommand { RefreshToken = dto.RefreshToken };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}