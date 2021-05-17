using System;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.RoomInvitation;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class RoomManager : IRoomService
    {
        private IRoomDal _roomDal;
        private IUserRoomDal _userRoomDal;
        private IInvitationDal _invitationDal;
        private IInvitationHelper _invitationHelper;

        private ITransactionService _transactionService;
        private IUserService _userService;

        private int _invitationExpireTime = 15;


        public RoomManager(IRoomDal roomDal, IInvitationDal invitationDal, IInvitationHelper invitationHelper, IUserRoomDal userRoomDal, IUserService userService, ITransactionService transactionService)
        {
            _roomDal = roomDal;
            _invitationDal = invitationDal;
            _invitationHelper = invitationHelper;
            _userRoomDal = userRoomDal;
            _userService = userService;
            _transactionService = transactionService;
        }
        public IDataResult<List<Room>> GetList()
        {
            var result = _roomDal.GetAll();
            return new SuccessDataResult<List<Room>>(result, Messages.RoomsFetched);
        }

        public IDataResult<Room> Get(string roomId)
        {
            var result = _roomDal.GetAll().SingleOrDefault(r => r.Id.Equals(roomId));
            return new SuccessDataResult<Room>(result, Messages.RoomFetched);
        }

        public IDataResult<List<User>> GetUsersExistInRoom(string room)
        {
            var result = _roomDal.GetUsersExistInRoom(room);
            return new SuccessDataResult<List<User>>(result, Messages.UsersExistInRoomFetched);
        }

        public IDataResult<List<Invitation>> GetListInvitations()
        {
            var result = _invitationDal.GetAll().ToList();
            return new SuccessDataResult<List<Invitation>>(result,Messages.InvitationsFetched); 
            //TODO github readmes need to be added.
        }
        public IDataResult<Invitation> CreateInvitation(Room room)
        {
            var invitationCheck = _invitationDal.GetAll().SingleOrDefault(r => r.RoomId == room.Id);
            if (invitationCheck == null)
            {
                var invitation = _invitationHelper.CreateInvitation(room.Id);
                while (true)
                {
                    var invitationForCheckInvitationCode = GetListInvitations().Data.Where(i => i.InvitationCode == invitation.InvitationCode);
                    if (invitationForCheckInvitationCode.IsNullOrEmpty())
                    {
                        break;
                    }

                    invitation = _invitationHelper.CreateInvitation(room.Id);
                }
                invitation.Expiration = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_invitationExpireTime))
                    .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                    .TotalMilliseconds;
                _invitationDal.Add(invitation);
                return new SuccessDataResult<Invitation>(invitation, Messages.InvitationCreated);
            }
            return new ErrorDataResult<Invitation>(Messages.InvitationExists);
        }

        public IDataResult<Invitation> GetInvitation(string roomId)
        {
            var invitation = _invitationDal.GetAll().SingleOrDefault(r => r.RoomId == roomId);
            if (invitation == null)
            {
                return new ErrorDataResult<Invitation>(Messages.InvitationNotFound);
            }
            var timeNow = DateTime.UtcNow
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;
            if (timeNow > invitation.Expiration)
            {
                DeleteInvitation(invitation);
                return new ErrorDataResult<Invitation>(Messages.InvitationExpired);
            }
            return new SuccessDataResult<Invitation>(invitation, Messages.InvitationFetched);
        }

        public IResult DeleteInvitation(Invitation invitation)
        {
            _invitationDal.Delete(invitation);
            return new SuccessResult(Messages.InvitationDeleted);
        }
        public IResult JoinRoom(string invitation)
        {
            var userCheck = _userService.GetCurrentUser();
            if (userCheck.Data == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var invitationCheck = _invitationDal.GetAll().SingleOrDefault(r => r.InvitationCode == invitation);
            if (invitationCheck == null)
            {
                return new ErrorResult(Messages.InvitationNotFound);
            }
            BusinessRules.Run(CheckIfRoomCapasityLessThan100(invitationCheck.RoomId));
            var result = _userRoomDal.GetAll()
                .SingleOrDefault(r => r.RoomId == invitationCheck.RoomId && r.UserId == userCheck.Data.Id);
            if (result != null)
            {
                return new ErrorResult(Messages.UserAlreadyExistsInRoom);
            }
            _userRoomDal.Add(new UserRoom
            {
                RoomId = invitationCheck.RoomId,
                UserId = userCheck.Data.Id
            });
            return new SuccessResult(Messages.JoinRoomSuccessful);
        }

        public IResult LeaveRoom(Room room)
        {
            var userCheck = _userService.GetCurrentUser();
            if (userCheck.Data == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            //TODO Claims will be added.
            var roomToBeLeaved = _userRoomDal.GetAll().SingleOrDefault(u => u.RoomId == room.Id && u.UserId == userCheck.Data.Id);
            if (roomToBeLeaved == null)
            {
                return new ErrorResult(Messages.UserNotFoundInRoom);
            }
            _userRoomDal.Delete(roomToBeLeaved);
            var usersLeftInRoom = _userRoomDal.GetAll().Where(r => r.RoomId == room.Id);
            if (usersLeftInRoom.IsNullOrEmpty())
            {
                Delete(room);
                var invitation = GetInvitation(room.Id);
                DeleteInvitation(invitation.Data);
            }
            return new SuccessResult(Messages.LeaveRoomSuccessful);
        }

        public IResult Add(Room room)
        {
            var userCheck = _userService.GetCurrentUser();
            if (userCheck.Data != null)
            {
                _roomDal.Add(room);
                _userRoomDal.Add(new UserRoom
                {
                    UserId = userCheck.Data.Id,
                    RoomId = room.Id
                });
                return new SuccessResult(Messages.RoomCreated);
            }
            return new ErrorResult(Messages.UserNotFound);
        }

        public IResult Delete(Room room)
        {
            _roomDal.Delete(room);
            var users = _userRoomDal.GetAll().Where(u => u.RoomId == room.Id).ToList();
            users.ForEach(u => _userRoomDal.Delete(u));
            _transactionService.GetTransactionsForRoom(room.Id).Data
                .ForEach(x => _transactionService.Delete(x));
            var invitation = GetInvitation(room.Id);
            DeleteInvitation(invitation.Data);
            return new SuccessResult(Messages.RoomDeleted);
        }

        public IResult Update(Room room)
        {
            _roomDal.Update(room);
            return new SuccessResult(Messages.RoomUpdated);
        }
        public IDataResult<List<Room>> GetUserRooms()
        {
            var userCheck = _userService.GetCurrentUser();
            if (userCheck.Data == null)
            {
                return new ErrorDataResult<List<Room>>(Messages.UserNotFound);
            }
            var result = _roomDal.GetUserRooms(userCheck.Data.Id);
            return new SuccessDataResult<List<Room>>(result, Messages.UserRoomsFetched);

        }

        private IResult CheckIfRoomCapasityLessThan100(string roomId)
        {
            var result = _userRoomDal.GetAll().Where(r => r.RoomId.Equals(roomId));
            if (result.Count() >= 99)
            {
                return new ErrorResult(Messages.RoomLimitExceed);
            }

            return new SuccessResult();
        }

    }
}
