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
    }
}