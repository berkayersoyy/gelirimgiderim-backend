using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.RoomInvitation;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
  public class RoomManager : IRoomService
  {
    private IRoomDal _roomDal;
    private IUserRoomDal _userRoomDal;
    private IInvitationDal _invitationDal;
    private IInvitationHelper _invitationHelper;

    private IUserService _userService;

    private IHttpContextAccessor _httpContextAccessor;

    public RoomManager(IRoomDal roomDal, IInvitationDal invitationDal, IInvitationHelper invitationHelper, IUserRoomDal userRoomDal, IUserService userService)
    {
      _roomDal = roomDal;
      _invitationDal = invitationDal;
      _invitationHelper = invitationHelper;
      _userRoomDal = userRoomDal;
      _userService = userService;
      _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }
    public IDataResult<List<Room>> GetList()
    {
      var result = _roomDal.GetAll();
      return new SuccessDataResult<List<Room>>(result, Messages.RoomsFetched);
    }

    public IDataResult<Room> Get(string id)
    {
      var result = _roomDal.GetAll().SingleOrDefault(r => r.Id.Equals(id));
      return new SuccessDataResult<Room>(result, Messages.RoomFetched);
    }

    public IDataResult<List<UserToList>> GetUsersExistInRoom(Room room)
    {
      var result = _roomDal.GetUsersExistInRoom(room);
      return new SuccessDataResult<List<UserToList>>(result, Messages.UsersExistInRoomFetched);
    }

    public IResult CreateInvitation(Room room)
    {
      var invitationCheck = _invitationDal.GetAll().SingleOrDefault(r => r.RoomId == room.Id);
      if (invitationCheck == null)
      {
        var invitation = _invitationHelper.CreateInvitation(room);
        _invitationDal.Add(invitation);
        return new SuccessResult(Messages.InvitationCreated);
        //TODO Invitation check needed for creating the same one in the list!
      }
      return new ErrorResult(Messages.InvitationExists);
    }

    public IDataResult<Invitation> GetInvitation(Room room)
    {
      var invitation = _invitationDal.GetAll().SingleOrDefault(r => r.RoomId == room.Id);
      return new SuccessDataResult<Invitation>(invitation, Messages.InvitationFetched); 
    }

    public IResult DeleteInvitation(Invitation invitation)
    {
      _invitationDal.Delete(invitation);
      return new SuccessResult(Messages.InvitationDeleted);
    }

    public IResult SetCurrentRoom(string roomId)
    {
      _httpContextAccessor.HttpContext.Items.Add("currentRoom",roomId);
      return new SuccessResult(); //TODO Message will be added.
    }

    public IResult JoinRoom(string invitation)
    {
      //TODO room capasity arrangements.
      var userCheck = _userService.GetCurrentUser();
      if (userCheck.Data != null)
      {
        var invitationCheck = _invitationDal.GetAll().SingleOrDefault(r => r.Code == invitation);
        if (invitationCheck == null)
        {
          return new ErrorResult(Messages.InvitationNotFound);
        }
        _userRoomDal.Add(new UserRoom
        {
          RoomId = invitationCheck.RoomId,
          UserId = userCheck.Data.Id
        });
        return new SuccessResult(Messages.JoinRoomSuccessful);
      }
      return new ErrorResult(Messages.UserNotFound);
    }

    public IResult LeaveRoom(Room room)
    {
      var userCheck = _userService.GetCurrentUser();
      if (userCheck.Data != null)
      {
        var roomToBeLeaved = _userRoomDal.GetAll().SingleOrDefault(u => u.RoomId == room.Id && u.UserId == userCheck.Data.Id);
        _userRoomDal.Delete(roomToBeLeaved);
        return new SuccessResult(Messages.LeaveRoomSuccessful);
      }
      //TODO Room user count check
      //TODO Claims will be added.
      return new ErrorResult(Messages.UserNotFound);
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
      var users = _userRoomDal.GetAll().Where(u=>u.RoomId==room.Id).ToList();
      users.ForEach(u=>_userRoomDal.Delete(u));
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
      if (userCheck.Data != null)
      {
        var result = _roomDal.GetUserRooms(userCheck.Data);
        return new SuccessDataResult<List<Room>>(result, Messages.UserRoomsFetched);
      }
      return new ErrorDataResult<List<Room>>(Messages.UserNotFound);

    }
  }
}
