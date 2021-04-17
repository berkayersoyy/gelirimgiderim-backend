
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Core.Entities.Concrete;
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

        [HttpGet("getall")]
        public ActionResult GetUserRooms(User user)
        {
            var result = _roomService.GetUserRooms(user);
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
        public ActionResult Add(UserForCreateRoom userForCreateRoom)
        {
            var result = _roomService.Add(userForCreateRoom);
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

        [HttpPost("invitation/create")]
        public ActionResult CreateInvitation(Room room)
        {
            var result = _roomService.CreateInvitation(room);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("invitation/delete")]
        public ActionResult DeleteInvitation(Invitation invitation)
        {
            var result = _roomService.DeleteInvitation(invitation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("invitation/get")]
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
        public ActionResult JoinRoom(UserForJoinRoom userForJoinRoom)
        {
            var result = _roomService.JoinRoom(userForJoinRoom);
            if (result.Success)
            {
                return Ok(result);
            }
            //TODO Guess New DTOs on the way...

            return BadRequest(result);
        }

        [HttpPost("leaveroom")]
        public ActionResult LeaveRoom(UserForLeaveRoom userForLeaveRoom)
        {
            var result = _roomService.LeaveRoom(userForLeaveRoom);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
