using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RoomManager : IRoomService
    {
        private IRoomDal _roomDal;

        public RoomManager(IRoomDal roomDal)
        {
            _roomDal = roomDal;
        }

        public IDataResult<List<Room>> GetList()
        {
            var result = _roomDal.GetAll();
            return new SuccessDataResult<List<Room>>(result);
        }

        public IDataResult<Room> GetRoom(string roomId)
        {
            var result = _roomDal.GetAll().SingleOrDefault(r => r.Id == roomId);
            return new SuccessDataResult<Room>(result);
        }

        public IDataResult<List<User>> GetUsersExistInRoom(Room room)
        {
            var result = _roomDal.GetUsersExistInRoom(room);
            return new SuccessDataResult<List<User>>(result);
        }

        public IResult Add(Room room)
        {
            _roomDal.Add(room);
            return new SuccessResult();

        }

        public IResult Delete(Room room)
        {
            _roomDal.Delete(room);
            return new SuccessResult();
        }

        public IResult Update(Room room)
        {
            _roomDal.Update(room);
            return new SuccessResult();
        }
        public IDataResult<List<Room>> GetUserRooms(User user)
        {
            var result = _roomDal.GetUserRooms(user);
            return new SuccessDataResult<List<Room>>(result, Messages.UserRoomsFetched);
        }
    }
}