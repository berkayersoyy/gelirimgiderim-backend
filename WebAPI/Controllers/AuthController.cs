
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(Messages.UserAlreadyRegistered);
            }

            var result = _authService.Register(userForRegisterDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            var tokenResult = _authService.CreateAccessToken(result.Data);
            if (tokenResult.Success)
            {
                return Ok(tokenResult);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
