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
using System.ComponentModel;

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
        public async Task<ActionResult<List<BankAccountModel>>> SearchAllClients()
        {
            List<BankAccountModel> bankAccounts = await _accountServices.SearchAllAccounts();
            List<BankAccountDTO> bankAccountsDTO = bankAccounts.Select(x => BankAccountDTO.ToDTO(x)).ToList();
            return Ok(bankAccounts);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccountDTO>> SearchAccountById(int id)
        {
            var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                var bankAccountById = await _accountServices.SearchAccountById(id);
                if (bankAccountById.CPF != clientCpf)
                {
                    return Unauthorized("Invalid operation");
                }

                return Ok(bankAccountById);
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

        [HttpPost]
        public async Task<ActionResult<CreateAccountDTO>> AddAccount([FromBody] CreateAccountDTO newBankAccountDTO)
        {
            try
            {
                var bankAccount = await _accountServices.AddAccount(newBankAccountDTO);
                return Ok(BankAccountDTO.ToDTO(bankAccount));
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
        [HttpPatch("deposit/{id}")]
        public async Task<ActionResult<BankAccountDTO>> DepositBalance([FromForm] decimal amount, int id)
        {
            var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var account = await _accountServices.SearchAccountById(id);

            if (clientCpf != account.CPF)
            {
                return Unauthorized("Invalid operation");
            }

            try
            {
                account = await _accountServices.DepositBalance(amount, id);
                return account;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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