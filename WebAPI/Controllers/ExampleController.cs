
using Microsoft.AspNetCore.Mvc;
using Core.Utilities.Results;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
      [HttpGet("get")]
      public IActionResult Example()
      {
        return Ok(new SuccessDataResult<string>("YAVRUSSS"));
      }
    }
}
