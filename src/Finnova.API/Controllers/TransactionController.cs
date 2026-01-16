using Finnova.Application.Commands.Transactions;
using Finnova.Application.DTOs.Transactions;
using Finnova.Application.Queries.Transactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finnova.API.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/accounts/{id}/transactions")]
        public async Task<IActionResult> GetByAccount(int id, [FromQuery] TransactionFilter filter)
        {
            var query = new GetAllTransactionsByAccountIdQuery
            {
                Id = id,
                Type = filter.Type,
                MinAmount = filter.MinAmount,
                MaxAmount = filter.MaxAmount,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate
            };

            var transactions = await _mediator.Send(query);
            
            return Ok(transactions);
        }

        [HttpGet("/api/users/{id}/transactions")]
        public async Task<IActionResult> GetByUser(int id, [FromQuery] TransactionFilter filter)
        {
            var query = new GetAllTransactionsByUserIdQuery
            {
                Id = id,
                Type = filter.Type,
                MinAmount = filter.MinAmount,
                MaxAmount = filter.MaxAmount,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate
            };

            var transactions = await _mediator.Send(query);
            
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetTransactionByIdQuery{ Id = id };

            var transaction = await _mediator.Send(query);

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)
        {
            var command = new CreateTransactionCommand
            {
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Type = dto.Type,
                Description = dto.Description,
                Date = dto.Date
            };

            int newTransactionId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id =  newTransactionId }, null);
        }
    }
}