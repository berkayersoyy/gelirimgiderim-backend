
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
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
        public ActionResult Get(RoomForGetByIdDto room)
        {
            var result = _roomService.Get(room.Id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getall")]
        public ActionResult GetUserRooms()
        {
            var result = _roomService.GetUserRooms();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getusersexist")]
        public ActionResult GetUsersExistInRoom(Room room)
        {
            var result = _roomService.GetUsersExistInRoom(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public ActionResult Add(Room room)
        {
            var result = _roomService.Add(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public ActionResult Update(Room room)
        {
            var result = _roomService.Update(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public ActionResult Delete(Room room)
        {
            var result = _roomService.Delete(room);
            if (result.Success)
            {
                Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("createinvitation")]
        public ActionResult CreateInvitation(Room room)
        {
            var result = _roomService.CreateInvitation(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deleteinvitation")]
        public ActionResult DeleteInvitation(Invitation invitation)
        {
            var result = _roomService.DeleteInvitation(invitation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getinvitation")]
        public ActionResult GetInvitation(Room room)
        {
            var result = _roomService.GetInvitation(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("joinroom")]
        public ActionResult JoinRoom(InvitationForJoinRoomDto invitation)
        {
            var result = _roomService.JoinRoom(invitation.InvitationCode);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("leaveroom")]
        public ActionResult LeaveRoom(Room room)
        {
            var result = _roomService.LeaveRoom(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
