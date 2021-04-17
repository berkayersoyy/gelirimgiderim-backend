using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Castle.Core.Internal;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.RoomInvitation;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class RoomManager : IRoomService
    {
        private IRoomDal _roomDal;
        private IUserRoomDal _userRoomDal;
        private IInvitationDal _invitationDal;
        private IInvitationHelper _invitationHelper;

        public RoomManager(IRoomDal roomDal, IInvitationDal invitationDal, IInvitationHelper invitationHelper, IUserRoomDal userRoomDal)
        {
            _roomDal = roomDal;
            _invitationDal = invitationDal;
            _invitationHelper = invitationHelper;
            _userRoomDal = userRoomDal;
        }
        public IDataResult<List<Room>> GetList()
        {
            var result = _roomDal.GetAll();
            return new SuccessDataResult<List<Room>>(result);//TODO message will be added
        }

        public IDataResult<Room> GetRoom(Room room)
        {
            var result = _roomDal.GetAll().SingleOrDefault(r => r.Id == room.Id);
            return new SuccessDataResult<Room>(result);//TODO message will be added
        }

        public IDataResult<List<User>> GetUsersExistInRoom(Room room)
        {
            var result = _roomDal.GetUsersExistInRoom(room);
            return new SuccessDataResult<List<User>>(result);//TODO message will be added
        }

        public IResult CreateInvitation(Room room)
        {
            var invitationCheck = _invitationDal.GetAll().SingleOrDefault(r => r.RoomId == room.Id);
            if (invitationCheck==null)
            {
                var invitation = _invitationHelper.CreateInvitation(room);
                _invitationDal.Add(invitation);
                return new SuccessResult();
                //TODO Invitation check needed for creating the same one in the list!
            }
            return new ErrorResult();
            //TODO message will be added
        }

        public IDataResult<Invitation> GetInvitation(Room room)
        {
            var invitation = _invitationDal.GetAll().SingleOrDefault(r => r.RoomId == room.Id);
            return new SuccessDataResult<Invitation>(invitation); //TODO message will be added
        }

        public IResult DeleteInvitation(Invitation invitation)
        {
            _invitationDal.Delete(invitation);
            return new SuccessResult();
        }

        public IResult JoinRoom(UserForJoinRoom userForJoinRoom)
        {
            var invitationCheck = _invitationDal.GetAll().SingleOrDefault(r => r.Code == userForJoinRoom.Invitation);
            if (invitationCheck==null)
            {
                return new ErrorResult();
                //TODO message will be added
            }
            _userRoomDal.Add(new UserRoom
            {
                RoomId = invitationCheck.RoomId,
                UserId = userForJoinRoom.User.Id
            });
            return new SuccessResult();//TODO message will be added
        }

        public IResult LeaveRoom(UserForLeaveRoom userForLeaveRoom)
        {
            var roomToBeLeaved = _userRoomDal.GetAll().SingleOrDefault(u => u.RoomId == userForLeaveRoom.Room.Id && u.UserId == userForLeaveRoom.User.Id);
            _userRoomDal.Delete(roomToBeLeaved);
            return new SuccessResult();//TODO message will be added
        }

        public IResult Add(UserForCreateRoom userForCreateRoom)
        {
            _roomDal.Add(userForCreateRoom.Room);
            _userRoomDal.Add(new UserRoom
            {
                UserId = userForCreateRoom.User.Id,
                RoomId = userForCreateRoom.Room.Id
            });
            return new SuccessResult();//TODO message will be added

        }

        public IResult Delete(Room room)
        {
            _roomDal.Delete(room);
            return new SuccessResult();//TODO message will be added
        }

        public IResult Update(Room room)
        {
            _roomDal.Update(room);
            return new SuccessResult();//TODO message will be added
        }
        public IDataResult<List<Room>> GetUserRooms(User user)
        {
            var result = _roomDal.GetUserRooms(user);
            return new SuccessDataResult<List<Room>>(result, Messages.UserRoomsFetched);
        }
    }
}