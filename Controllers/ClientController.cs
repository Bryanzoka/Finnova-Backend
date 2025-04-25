using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAccountAPI.Repository;
using BankAccountAPI.Models;
using BankAccountAPI.Services.Interface;
<<<<<<< HEAD
=======
using BankAccountAPI.Models.DTOs;
>>>>>>> 4617cd4 (Adicionando novos DTOs para proteger a saída e entrada de dados e corrigindo erros)

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _clientServices;

        public ClientController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<BankClientModel>>> SearchAllClients()
        {
            List<BankClientModel> bankClients = await _clientServices.SearchAllClients();
            return Ok(bankClients);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<BankClientModel>> SearchClientByCPF(string cpf)
        {
            BankClientModel bankClientByCpf = await _clientServices.SearchClientByCPF(cpf);
            return Ok(bankClientByCpf);
        }

        [HttpPost]
        public async Task<ActionResult<BankClientModel>> AddClient([FromBody] BankClientModel bankClientModel)
        {
            BankClientModel bankClient = await _clientServices.AddClient(bankClientModel);
            return Ok(bankClient);
        }

        [HttpPut("{cpf}")]
<<<<<<< HEAD
        public async Task<ActionResult<BankClientModel>> UpdateClient([FromBody] BankClientModel bankClientModel, string cpf)
        {
            BankClientModel bankClient = await _clientServices.UpdateClient(bankClientModel, cpf);
=======
        public async Task<ActionResult<BankClientModel>> UpdateClient([FromBody] UpdateClientDTO bankClientDTO, string cpf)
        {
            BankClientModel bankClient = await _clientServices.UpdateClient(BankClientModel.ToModelUpdate(bankClientDTO), cpf);
>>>>>>> 4617cd4 (Adicionando novos DTOs para proteger a saída e entrada de dados e corrigindo erros)
            return Ok(bankClient);
        }

        [HttpDelete("{cpf}")]
        public async Task<ActionResult<BankClientModel>> DeleteClient(string cpf)
        {
            bool clientDeleted = await _clientServices.DeleteClient(cpf);
            return Ok(clientDeleted);
        }
    }
}