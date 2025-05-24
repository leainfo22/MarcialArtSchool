using Microsoft.AspNetCore.Mvc;
using Account.DTO.LoginDTO;
using MarcialArtSchool.Core.RepositoryContracts;

namespace MarcialArtSchool.Controllers
{
    
    [Route("[controller]")] //api/Auth
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        private readonly IAccountRepository _userService;
        public AuthorizationController(IAccountRepository userService)
        {
            _userService = userService;
        }

        [HttpPost("login")] //POST authorization/register
        public async Task<IActionResult>Login(LoginDTO loginRequest)
        {
            //check if the request is valid
            if (loginRequest == null)
                return BadRequest("Invalid request");

             var response = await _userService.Login(loginRequest);

            if (response == null)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}
