using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IRoomService
    {
        IDataResult<List<Room>> GetList();
        IDataResult<Room> Get(string id);
        IDataResult<List<UserToList>> GetUsersExistInRoom(Room room);
        IDataResult<List<Room>> GetUserRooms();
        IResult CreateInvitation(Room room);
        IDataResult<Invitation> GetInvitation(Room room);
        IResult DeleteInvitation(Invitation invitation);
        IResult SetCurrentRoom(string roomId);
        IResult LeaveRoom(Room room);
        IResult JoinRoom(string invitation);
        IResult Add(Room room);
        IResult Delete(Room room);
        IResult Update(Room room);
    }
}
