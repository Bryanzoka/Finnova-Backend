using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;
using BankAccountAPI.Models.DTOs;
using BankAccountAPI.Services;
using BankAccountAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IClientService clientService, ITokenService tokenService)
        {
            _clientService = clientService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginClientDTO loginClientDTO)
        {
            var client = await _clientService.ValidateCredentials(loginClientDTO.CPF, loginClientDTO.Password);
            if (client == null) return Unauthorized();
            var token = _tokenService.GenerateToken(client);
            return Ok(token);
        }
    }
}