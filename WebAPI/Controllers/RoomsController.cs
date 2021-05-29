
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private IRoomService _roomService;


        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("get")]
        public IActionResult Get(string room)
        {
            var result = _roomService.Get(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getuserrooms")]
        public IActionResult GetUserRooms()
        {
            var result = _roomService.GetUserRooms();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getusersexist")]
        public IActionResult GetUsersExistInRoom(string room)
        {
            var result = _roomService.GetUsersExistInRoom(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Room room)
        {
            var result = _roomService.Add(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Room room)
        {
            var result = _roomService.Update(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Room room)
        {
            var result = _roomService.Delete(room);
            if (result.Success)
            {
               return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("createinvitation")]
        public IActionResult CreateInvitation(Room room)
        {
            var result = _roomService.CreateInvitation(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deleteinvitation")]
        public IActionResult DeleteInvitation(Invitation invitation)
        {
            var result = _roomService.DeleteInvitation(invitation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getinvitation")]
        public IActionResult GetInvitation(string room)
        {
            var result = _roomService.GetInvitation(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("joinroom")]
        public IActionResult JoinRoom(InvitationForJoinRoomDto invitation)
        {
            var result = _roomService.JoinRoom(invitation.InvitationCode);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("leaveroom")]
        public IActionResult LeaveRoom(Room room)
        {
            var result = _roomService.LeaveRoom(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("setcurrentroom")]
        public IActionResult SetCurrentRoom(Room room)
        {
            var result = _roomService.SetCurrentRoom(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcurrentroom")]
        public IActionResult GetCurrentRoom()
        {
            var result = _roomService.GetCurrentRoom();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
