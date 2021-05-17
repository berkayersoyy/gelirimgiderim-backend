using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IRoomDal:IEntityRepository<Room>
    {
        public List<User> GetUsersExistInRoom(string roomId);
        public List<Room> GetUserRooms(string userId);

    }
}
