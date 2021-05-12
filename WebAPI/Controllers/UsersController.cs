using Microsoft.AspNetCore.Mvc;
using Business.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("currentuser")]
        public ActionResult GetCurrentUser()
        {
            var result = _userService.GetCurrentUser();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public ActionResult GetUserById(string id)
        {
            var result = _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyemail")]
        public ActionResult GetUserByEmail(string email)
        {
            var result = _userService.GetByMail(email);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
