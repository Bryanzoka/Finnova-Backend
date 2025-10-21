using Finnova.Application.Commands.Accounts;
using Finnova.Application.DTOs.Accounts;
using Finnova.Application.Queries.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finnova.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllAccountsQuery();
            var accounts = await _mediator.Send(query);

            if (accounts == null)
            {
                return NotFound("Accounts not found");
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
                return NotFound("Account not found");
            }

            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountDto dto)
        {
            var command = new CreateAccountCommand
            {
                ClientId = dto.ClientId,
                Type = dto.Type,
                Password = dto.Password,
                Password_confirmation = dto.Password_confirmation
            };

            int newAccountId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = newAccountId }, null);
        }
    }
}