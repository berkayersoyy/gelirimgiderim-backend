
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Core.Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClaimsController : ControllerBase
    {
        private IClaimService _claimService;

        public ClaimsController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet("getall")]
        public ActionResult GetList(string room)
        {
            var result = _claimService.GetList(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get")]
        public ActionResult Get(string claim)
        {
            var result = _claimService.Get(claim);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public ActionResult Add(Claim claim)
        {
            var result = _claimService.Add(claim);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public ActionResult Delete(Claim claim)
        {
            var result = _claimService.Delete(claim);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public ActionResult Update(Claim claim)
        {
            var result = _claimService.Update(claim);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getuserclaims")]
        public ActionResult GetUserClaims(string room)
        {
            var result = _claimService.GetUserClaims(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addclaimtouser")]
        public ActionResult AddClaimToUser(UserClaim userClaim)
        {
            var result = _claimService.AddClaimToUser(userClaim);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deleteclaimfromuser")]
        public ActionResult DeleteClaimFromUser(UserClaim userClaim)
        {
            var result = _claimService.DeleteClaimFromUser(userClaim);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getsharedall")]
        public ActionResult GetSharedList()
        {
            var result = _claimService.GetSharedList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getshared")]
        public ActionResult GetShared(string claimId)
        {
            var result = _claimService.GetShared(claimId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
    }
}
