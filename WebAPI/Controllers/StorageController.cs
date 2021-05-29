
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }
        [HttpPost("upload")]
        public IActionResult Upload(StorageForPathDto path)
        {
            var result = _storageService.Upload(path.Path);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(string fileName)
        {
            var result = _storageService.Delete(fileName);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get")]
        public IActionResult Get(string fileName)
        {
            var result = _storageService.Get(fileName);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
