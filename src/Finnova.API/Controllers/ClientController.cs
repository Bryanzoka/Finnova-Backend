using Microsoft.AspNetCore.Mvc;
using MediatR;
using Finnova.Application.Commands.Clients;
using Finnova.Application.DTOs.Clients;
using Finnova.Application.Queries.Clients;

namespace Finnova.API.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllClientsQuery();
            var clients = await _mediator.Send(query);

            if (clients == null)
            {
                return NotFound("Clients not found");
            }

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetClientByIdQuery { Id = id };
            var clientDto = await _mediator.Send(query);

            if (clientDto == null)
                return NotFound("Client not found");

            return Ok(clientDto);
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestVerificationCode([FromBody] RequestVerificationCodeDto dto)
        {
            var command = new RequestVerificationCodeCommand { Email = dto.Email };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateClientDto dto)
        {
            var command = new CreateClientCommand
            {
                Name = dto.Name,
                Cpf = dto.Cpf,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = dto.Password,
                Password_confirmation = dto.Password_confirmation,
                Code = dto.Code
            };

            var newClientId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = newClientId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClientDto dto)
        {
            var command = new UpdateClientCommand
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = dto.Password
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}