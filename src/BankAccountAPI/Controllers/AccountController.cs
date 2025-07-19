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
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BankAccountModel>>> SearchAllClients()
        {
            List<BankAccountModel> bankAccounts = await _accountService.SearchAllAccounts();
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
                var bankAccountById = await _accountService.SearchAccountById(id);
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CreateAccountDTO>> AddAccount([FromBody] CreateAccountDTO newBankAccountDTO)
        {
            try
            {
                var bankAccount = await _accountService.AddAccount(newBankAccountDTO);
                return Ok(bankAccount);
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
            try
            {
                var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var account = await _accountService.DepositBalance(amount, id, clientCpf);
                return Ok(account);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("withdraw/{id}")]
        public async Task<ActionResult<BankAccountDTO>> WithdrawBalance([FromForm] decimal amount, int id)
        {
            try
            {
                var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                BankAccountDTO bankAccount = await _accountService.WithdrawBalance(amount, id, clientCpf);
                return Ok(bankAccount);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [Authorize]
        [HttpPatch("transfer")]
        public async Task<ActionResult<BankAccountDTO>> TransferBalance([FromForm] decimal amount, int accountId, int recipientId)
        {
            try
            {
                var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                BankAccountDTO bankAccount = await _accountService.TransferBalance(amount, accountId, recipientId);
                return Ok(bankAccount);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankAccountDTO>> DeleteAccount(int id)
        {
            try
            {
                var clientCpf = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                bool accountDeleted = await _accountService.DeleteAccount(id, clientCpf);
                return Ok(accountDeleted);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}