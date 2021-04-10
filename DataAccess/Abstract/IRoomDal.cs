using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IRoomDal:IEntityRepository<Room>
    {
        public List<User> GetUsersExistInRoom(Room room);
        public List<Room> GetUserRooms(User user);

    }
}