using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAccountAPI.Repository;
using BankAccountAPI.Models;
using BankAccountAPI.Models.DTOs;
using BankAccountAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountServices;

        public AccountController(IAccountService accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<BankAccountDTO>>> SearchAllClients()
        {
            List<BankAccountModel> bankAccounts = await _accountServices.SearchAllAccounts();
            List<BankAccountDTO> bankAccountsDTO = bankAccounts.Select(x => BankAccountDTO.ToDTO(x)).ToList();
            return Ok(bankAccountsDTO);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccountDTO>> SearchAccountById(int id)
        {
            var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            BankAccountModel bankAccountById = await _accountServices.SearchAccountById(id);
            if (bankAccountById.CPF != clientCpf)
            {
                return Unauthorized("Ação não autorizada");
            }

            return Ok(BankAccountDTO.ToDTO(bankAccountById));
        }

        [HttpPost]
        public async Task<ActionResult<CreateAccountDTO>> AddAccount([FromBody] CreateAccountDTO newBankAccountDTO)
        {
            BankAccountModel newBankAccount = BankAccountModel.CreationDTOToModel(newBankAccountDTO);
            BankAccountModel bankAccount = await _accountServices.AddAccount(newBankAccount);
            return Ok(BankAccountDTO.ToDTO(bankAccount));
        }

        [HttpPatch("deposit/{id}")]
        public async Task<ActionResult<BankAccountDTO>> DepositBalance([FromForm] decimal amount, int id)
        {
            BankAccountModel bankAccount = await _accountServices.DepositBalance(amount, id);
            return Ok(BankAccountDTO.ToDTO(bankAccount));
        }

        [HttpPatch("withdraw/{id}")]
        public async Task<ActionResult<BankAccountDTO>> WithdrawBalance([FromForm] decimal amount, int id)
        {
            BankAccountModel bankAccount = await _accountServices.WithdrawBalance(amount, id);
            return Ok(BankAccountDTO.ToDTO(bankAccount));
        }

        [HttpPatch("transfer")]
        public async Task<ActionResult<BankAccountDTO>> TransferBalance([FromForm] decimal amount, int accountId, int recipientId)
        {
            BankAccountModel bankAccount = await _accountServices.TransferBalance(amount, accountId, recipientId);
            return Ok(BankAccountDTO.ToDTO(bankAccount));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BankAccountDTO>> DeleteAccount(int id)
        {
            bool accountDeleted = await _accountServices.DeleteAccount(id);
            return Ok(accountDeleted);
        }
    }
}