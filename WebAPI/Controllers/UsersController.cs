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

        [HttpGet("currentUser")]
        public ActionResult GetCurrentUser()
        {
            var user = _userService.GetCurrentUser();
            if (user.Success)
            {
                return Ok(user);
            }

            return BadRequest(user);
        }
    }
}
