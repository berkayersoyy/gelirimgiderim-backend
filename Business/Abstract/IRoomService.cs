using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IRoomService
    {
        IDataResult<List<Room>> GetList();
        IDataResult<Room> GetRoom(Room room);
        IDataResult<List<User>> GetUsersExistInRoom(Room room);
        IDataResult<List<Room>> GetUserRooms(User user);
        IResult CreateInvitation(Room room);
        IDataResult<Invitation> GetInvitation(Room room);
        IResult DeleteInvitation(Invitation invitation);
        IResult LeaveRoom(UserForLeaveRoom userForLeaveRoom);
        IResult JoinRoom(UserForJoinRoom userForJoinRoom);
        IResult Add(UserForCreateRoom userForCreateRoom);
        IResult Delete(Room room);
        IResult Update(Room room);
    }
}