
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Core.Entities.Concrete;
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
        public ActionResult GetList()
        {
            var result = _transactionService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public ActionResult GetTransactionById(string transactionId)
        {
            var result = _transactionService.Get(transactionId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public ActionResult Add(Transaction transaction)
        {
            var result = _transactionService.Add(transaction);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("delete")]
        public ActionResult Delete(Transaction transaction)
        {
            var result = _transactionService.Delete(transaction);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("update")]
        public ActionResult Update(Transaction transaction)
        {
            var result = _transactionService.Update(transaction);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
          
        }
        [HttpGet("getallforroom")]
        public ActionResult GetListForRoom(Room room)
        {
            var result = _transactionService.GetTransactionsForRoom(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
