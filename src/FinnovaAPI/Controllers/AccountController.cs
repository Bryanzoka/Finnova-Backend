using Microsoft.AspNetCore.Mvc;
using FinnovaAPI.Models;
using FinnovaAPI.Models.DTOs.Account;
using FinnovaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinnovaAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BankAccountModel>>> SearchAllAccounts()
        {
            List<BankAccountModel> bankAccounts = await _accountService.SearchAllAccounts();
            List<BankAccountDTO> bankAccountsDTO = bankAccounts.Select(BankAccountDTO.ToDTO).ToList();
            return Ok(bankAccounts);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccountDTO>> SearchAccountById(int id)
        {
            try
            {
                var clientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var bankAccountById = await _accountService.GetAuthenticatedClientAccount(id, clientId);
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
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("client/{id}")]
        public async Task<ActionResult<List<AccountPreviewDTO>>> SearchAllAccountsByClientId(int id)
        {
            try
            {
                var accounts = await _accountService.SearchAllAccountsByClientId(id);
                return Ok(accounts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CreateAccountDTO>> AddAccount([FromBody] CreateAccountDTO account)
        {
            try
            {
                int clientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var bankAccount = await _accountService.AddAccount(account, clientId);
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
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //TODO: create a http put or patch to update the account password

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult<BankAccountDTO>> UpdatePassword([FromBody] UpdateAccountDTO account)
        {
            try
            {
                int clientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var bankAccount = await _accountService.UpdateAccount(account, clientId);
                return Ok(bankAccount);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [Authorize]
        [HttpPatch("deposit")]
        public async Task<ActionResult<BankAccountDTO>> DepositBalance([FromBody] DepositDTO deposit)
        {
            try
            {
                //Client id comes as a string of the token
                var clientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var account = await _accountService.DepositBalance(deposit, clientId);
                return Ok(account);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("withdraw")]
        public async Task<ActionResult<BankAccountDTO>> WithdrawBalance([FromBody] WithdrawDTO withdraw)
        {
            try
            {
                var clientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                BankAccountDTO bankAccount = await _accountService.WithdrawBalance(withdraw, clientId);
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
                return Unauthorized(ex.Message);
            } 
        }

        [Authorize]
        [HttpPatch("transfer")]
        public async Task<ActionResult<BankAccountDTO>> TransferBalance([FromBody] TransferDTO transfer)
        {
            try
            {
                var clientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                BankAccountDTO bankAccount = await _accountService.TransferBalance(transfer, clientId);
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
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankAccountDTO>> DeleteAccount(int id)
        {
            try
            {
                var clientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                bool accountDeleted = await _accountService.DeleteAccount(id, clientId);
                return Ok(accountDeleted);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}