using BLL.security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace apiMateriels.Controllers
{
    public class SecurityController : ControllerBase
    {
        readonly IsecurityService _securityService;
        private IsecurityService securityService;
        public SecurityController(IsecurityService securityservice)
        {
            _securityService = securityservice;

        }
        public class SwaggerLogin { public string Username { get; set; } public string Password { get; set; } }
        [HttpPost("loginSwagger")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginDocSwagger([FromForm] SwaggerLogin loginRequest)
        {
            var result = await _securityService.signIn(loginRequest.Username, loginRequest.Password);

            if (result is null) return BadRequest();

            else return Ok(new { access_token = result });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginDocSwagger([FromBody] LoginRequest loginRequest)
        {
            var result = await _securityService.signIn(loginRequest.Email, loginRequest.Password);

            if (result is null) return BadRequest();

            else return Ok(new { access_token = result });
        }
    }
    //[HttpPost("login")]
    //public Task...(){}
}
