using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IRoomDal:IEntityRepository<Room>
    {
        public List<UserToList> GetUsersExistInRoom(Room room);
        public List<Room> GetUserRooms(User user);

    }
}
