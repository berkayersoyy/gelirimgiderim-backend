using System.Collections.Generic;
using System.Linq;
using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.Firebase
{
    public class FbRoomDal:FirebaseRepositoryBase<Room>,IRoomDal
    {
        private IUserDal _userDal;
        private IUserRoomDal _userRoomDal;
        public FbRoomDal(IUserDal userDal, IUserRoomDal userRoomDal) : base(FirebaseCollections.Rooms)
        {
            _userDal = userDal;
            _userRoomDal = userRoomDal;
        }
        public List<User> GetUsersExistInRoom(Room room)
        {
            var users = from userRoom in _userRoomDal.GetAll()
                join user in _userDal.GetAllUsersWithFirebase() on userRoom.RoomId equals room.Id
                where user.Id == userRoom.UserId
                select new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PasswordHash = user.PasswordHash,
                    PasswordSalt = user.PasswordSalt,
                    Status = true,
                };
            return users.ToList();
        }
        public List<Room> GetUserRooms(User user)
        {
            var userRooms = from userRoom in _userRoomDal.GetAll()
                join room in GetAll() on userRoom.UserId equals user.Id
                where userRoom.RoomId == room.Id
                select new Room
                {
                    Id = room.Id,
                    Name = room.Name,
                };
            return userRooms.ToList();
        }
    }
}