using Finnova.Application.Commands.Accounts;
using Finnova.Application.DTOs.Accounts;
using Finnova.Application.Queries.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finnova.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/users/{id}/accounts")]
        public async Task<IActionResult> GetAll(int id, [FromQuery] AccountFilter filter)
        {
            var query = new GetAllAccountsQuery
            {
                Id = id,
                IsActive = filter.IsActive,
                Type = filter.Type,
                MinBalance = filter.MinBalance,
                MaxBalance = filter.MaxBalance
            };

            var accounts = await _mediator.Send(query);

            if (accounts == null)
            {
                return NotFound("accounts not found");
            }

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetAccountByIdQuery { Id = id };
            var account = await _mediator.Send(query);

            if (account == null)
            {
                return NotFound("account not found");
            }

            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountDto dto)
        {
            var command = new CreateAccountCommand
            {
                UserId = dto.UserId,
                Name = dto.Name,
                Type = dto.Type
            };

            int newAccountId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = newAccountId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountDto dto)
        {
            var command = new UpdateAccountCommand
            {
                Id = id,
                Name = dto.Name,
                Type = dto.Type,
                IsActive = dto.IsActive
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAccountCommand { Id = id };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}