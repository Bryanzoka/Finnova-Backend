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

        [HttpPost]
        public async Task<ActionResult<BankClientModel>> AddClient([FromBody] BankClientModel bankClientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BankClientModel bankClient = await _clientService.AddClient(bankClientModel);
            return Ok(bankClient);
        }
        
        [Authorize]
        [HttpPut("{cpf}")]
        public async Task<ActionResult<BankClientDTO>> UpdateClient([FromBody] UpdateClientDTO updatedClient, string cpf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (cpf != clientCpf)
            {
                return Unauthorized("Invalid operation");
            }

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
        [HttpDelete("{cpf}")]
        public async Task<ActionResult<BankClientModel>> DeleteClient(string cpf)
        {
            var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (cpf != clientCpf)
            {
                return Unauthorized("Unauthorized acess");
            }

            bool clientDeleted = await _clientService.DeleteClient(cpf);
            return Ok(clientDeleted);
        }
    }
}