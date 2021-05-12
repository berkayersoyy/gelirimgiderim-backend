using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Claim = Core.Entities.Concrete.Claim;

namespace Business.Concrete
{
  public class ClaimManager:IClaimService
  {
    private IClaimDal _claimDal;
    private IUserClaimDal _userClaimDal;
    private IUserService _userService;

    public ClaimManager(IClaimDal claimDal, IUserClaimDal userClaimDal, IUserService userService)
    {
      _claimDal = claimDal;
      _userClaimDal = userClaimDal;
      _userService = userService;
    }

    public IDataResult<List<Claim>> GetList(string room)
    {
      var claims = _claimDal.GetAll().Where(c => c.RoomId == room).ToList();
      return new SuccessDataResult<List<Claim>>(claims); //TODO Message will be added
    }

    public IDataResult<Claim> Get(string claim)
    {
      var claimCheck = _claimDal.GetAll().SingleOrDefault(c => c.Id == claim);
      return new SuccessDataResult<Claim>(claimCheck); //TODO Message will be added
    }

    public IResult Add(Claim claim)
    {
      _claimDal.Add(claim);
      return new SuccessResult(); //TODO Message will be added
    }

    public IResult Delete(Claim claim)
    {
      _claimDal.Delete(claim);
      return new SuccessResult(); //TODO Message will be added
    }

    public IResult Update(Claim claim)
    {
      _claimDal.Update(claim);
      return new SuccessResult(); //TODO Message will be added
    }

    public IDataResult<List<Claim>> GetUsersClaims(string room)
    {
      var user = _userService.GetCurrentUser();
      var usersClaims = _claimDal.GetUserClaims(room, user.Data.Id);
      return new SuccessDataResult<List<Claim>>(usersClaims); //TODO Message will be added
    }

    public IResult AddClaimToUser(UserClaim userClaim)
    {
      //TODO 1 claim limit refactor or something else idk its too four at the morning im sleepy.
      _userClaimDal.Add(userClaim);
      return new SuccessResult(); //TODO Message will be added
    }

    public IResult DeleteClaimFromUser(UserClaim userClaim)
    {
      _userClaimDal.Add(userClaim);
      return new SuccessResult(); //TODO Message will be added
    }
  }
}
