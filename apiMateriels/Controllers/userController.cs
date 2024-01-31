using BLL.interfaces;
using BLL.security;
using domain.DTO.DTOrequests;
using domain.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
namespace apiMateriels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IsecurityService _securityService;
        private readonly IusersService _userService;
        public userController(IusersService userService, IsecurityService securityService)
        {
            _securityService = securityService;
            _userService = userService;
        }

        //// GET user role:
        #region get user role by id
        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserRoleById([FromRoute] int id)
        {
            string role = await _userService.GetUserRoleById(id);
            if (role is not null)
            {
                return Ok(role);
            }
            else
            {
                return NotFound($"Le utilisateur {id} n'a ps de rôle attribué.");
            }
        }
        #endregion
        //Gestion de la connection utilisateur:
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserFromConnection(DTOconnectionRequest rquestConnect)
        {//On retourneOk avec le token généré dans la variable access_token:
            return Ok(new DTOLoginResponse() { access_token = await _securityService.signIn(rquestConnect.name, rquestConnect.password) });
        }
    }

}
