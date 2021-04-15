using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IRoomService
    {
        IDataResult<List<Room>> GetList();
        IDataResult<Room> GetRoom(string roomId);
        IDataResult<List<User>> GetUsersExistInRoom(Room room);
        IDataResult<List<Room>> GetUserRooms(User user);
        IDataResult<Invitation> CreateInvitation(Room room);
        IResult DeleteInvitation(Invitation invitation);
        IResult LeaveRoom(User user, Room room);
        IResult JoinRoom(User user, string invitation);
        IResult Add(Room room);
        IResult Delete(Room room);
        IResult Update(Room room);
    }
}