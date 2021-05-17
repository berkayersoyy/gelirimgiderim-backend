using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IRoomService
    {
        IDataResult<List<Room>> GetList();
        IDataResult<Room> Get(string roomId);
        IDataResult<List<User>> GetUsersExistInRoom(string roomId);
        IDataResult<List<Room>> GetUserRooms();
        IDataResult<List<Invitation>> GetListInvitations();
        IDataResult<Invitation> CreateInvitation(Room room);
        IDataResult<Invitation> GetInvitation(string roomId);
        IResult DeleteInvitation(Invitation invitation);
        IResult LeaveRoom(Room room);
        IResult JoinRoom(string invitation);
        IResult Add(Room room);
        IResult Delete(Room room);
        IResult Update(Room room);

    }
}
