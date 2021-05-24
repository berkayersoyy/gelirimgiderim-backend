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
        public List<User> GetUsersExistInRoom(string roomId)
        {
            var users = from userRoom in _userRoomDal.GetAll()
                join user in _userDal.GetAllUsersWithFirebase() on userRoom.RoomId equals roomId
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
        public List<Room> GetUserRooms(string userId)
        {
            var userRooms = from userRoom in _userRoomDal.GetAll()
                join room in GetAll() on userRoom.UserId equals userId
                where userRoom.RoomId == room.Id
                select new Room
                {
                    Id = room.Id,
                    Name = room.Name,
                    Description = room.Description
                };
            return userRooms.ToList();
        }
    }
}
