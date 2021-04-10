using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IRoomService
    {
        IDataResult<List<Room>> GetList();
        IDataResult<Room> GetRoom(string roomId);
        IDataResult<List<User>> GetUsersExistInRoom(Room room);
        IDataResult<List<Room>> GetUserRooms(User user);
        IResult Add(Room room);
        IResult Delete(Room room);
        IResult Update(Room room);
    }
}