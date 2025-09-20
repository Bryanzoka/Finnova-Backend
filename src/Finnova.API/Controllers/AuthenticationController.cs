/* using Microsoft.AspNetCore.Mvc;

namespace FinnovaAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {/* 
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
            try
            {
                var client = await _clientService.ValidateCredentials(loginClientDTO.Cpf, loginClientDTO.Password);
                var token = _tokenService.GenerateToken(client);
                return Ok(new { token });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
} */