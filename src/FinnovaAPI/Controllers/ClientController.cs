using Microsoft.AspNetCore.Mvc;
using FinnovaAPI.Models;
using FinnovaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FinnovaAPI.Models.DTOs.Client;
using FinnovaAPI.Models.DTOs.Account;

namespace FinnovaAPI.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BankClientModel>>> SearchAllClients()
        {
            List<BankClientModel> bankClients = await _clientService.SearchAllClients();
            return Ok(bankClients);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<BankClientDTO>> SearchClientById()
        {
            try
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                BankClientDTO bankClientByCpf = await _clientService.SearchClientById(int.Parse(id));
                return Ok(bankClientByCpf);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("search")]
        public async Task<ActionResult<BankClientDTO>> SearchClientByCPF([FromBody] CpfRequest recipient)
        {
            try
            {
                BankClientDTO bankClientByCpf = await _clientService.SearchClientByCPF(recipient.Cpf);
                return Ok(bankClientByCpf);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("verify")]
        public async Task<ActionResult> ValidateClientInfo([FromBody] ClientValidationRequestDTO client)
        {
            try
            {
                var verifiedClient = await _clientService.ValidateClientInfo(client);
                return Ok(verifiedClient);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<BankClientDTO>> AddClient([FromBody] RegisterClientDTO client)
        {
            try
            {
                var registeredClient = await _clientService.AddClient(client);
                return Ok(registeredClient);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<ActionResult<BankClientDTO>> UpdateClient([FromBody] UpdateClientDTO updatedClient)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                BankClientDTO bankClient = await _clientService.UpdateClient(updatedClient, int.Parse(id));
                return Ok(bankClient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<BankClientModel>> DeleteClient()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                bool clientDeleted = await _clientService.DeleteClient(int.Parse(id));
                return Ok(clientDeleted);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}