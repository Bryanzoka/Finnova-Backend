using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAccountAPI.Repository;
using BankAccountAPI.Models;
using BankAccountAPI.Services.Interface;
using BankAccountAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/client")]
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
        [HttpGet("{cpf}")]
        public async Task<ActionResult<BankClientDTO>> SearchClientByCPF(string cpf)
        {
            var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (cpf != clientCpf)
            {
                return Unauthorized("Unauthorized access");
            }

            BankClientDTO bankClientByCpf = await _clientService.SearchClientByCPF(cpf);
            
            return Ok(bankClientByCpf);
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
        
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<BankClientDTO>> UpdateClient([FromBody] UpdateClientDTO updatedClient)
        {
            var cpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                BankClientDTO bankClient = await _clientService.UpdateClient(updatedClient, cpf);
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
            var cpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                bool clientDeleted = await _clientService.DeleteClient(cpf);
                return Ok(clientDeleted);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}