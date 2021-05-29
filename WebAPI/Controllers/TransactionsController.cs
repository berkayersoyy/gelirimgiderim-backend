
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _transactionService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("get")]
        public IActionResult GetTransactionById(string transaction)
        {
            var result = _transactionService.Get(transaction);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Transaction transaction)
        {
            var result = _transactionService.Add(transaction);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Transaction transaction)
        {
            var result = _transactionService.Delete(transaction);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Transaction transaction)
        {
            var result = _transactionService.Update(transaction);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
          
        }
        [HttpGet("getallforroom")]
        public IActionResult GetListForRoom(string room)
        {
            var result = _transactionService.GetTransactionsForRoom(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("gettransactionsbycategory")]
        public IActionResult GetTransactionsByCategory(string categoryId)
        {
            var result = _transactionService.GetTransactionsByCategory(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("gettransactiondetaildtos")]
        public IActionResult GetTransactionDetailDtos(string roomId)
        {
            var result = _transactionService.GetTransactionDetailDtos(roomId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("gettransactiondetaildto")]
        public IActionResult GetTransactionDetailDto(string transactionId)
        {
            var result = _transactionService.GetTransactionDetailDto(transactionId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
