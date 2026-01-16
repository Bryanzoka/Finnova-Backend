using Microsoft.AspNetCore.Mvc;
using MediatR;
using Finnova.Application.Commands.Users;
using Finnova.Application.DTOs.Users;
using Finnova.Application.Queries.Users;

namespace Finnova.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var users = await _mediator.Send(query);

            if (users == null)
            {
                return NotFound("users not found");
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var userDto = await _mediator.Send(query);

            if (userDto == null)
            {
                return NotFound("user not found");
            }
            
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var command = new CreateUserCommand
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                PasswordConfirmation = dto.PasswordConfirmation,
                Code = dto.Code
            };

            var newUserId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = newUserId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var command = new UpdateUserCommand
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUserCommand { Id = id };
            
            await _mediator.Send(command);

            return NoContent();
        }
    }
}