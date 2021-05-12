using System.Collections.Generic;
using System.Linq;
using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.Firebase
{
  public class FbClaimDal:FirebaseRepositoryBase<Claim>,IClaimDal
  {
    private IUserClaimDal _userClaimDal;
    public FbClaimDal(IUserClaimDal userClaimDal) : base(FirebaseCollections.Claims)
    {
      _userClaimDal = userClaimDal;
    }

    public List<Claim> GetUserClaims(string room, string userId)
    {
      var userClaimsList = from userClaims in _userClaimDal.GetAll()
        join claim in this.GetAll() on userClaims.RoomId equals room
        where userClaims.UserId == userId
        select new Claim
        {
          Id = claim.Id,
          Name = claim.Name,
          ClaimProperties = claim.ClaimProperties,
          RoomId = claim.RoomId,
          Priority = claim.Priority
        };
      return userClaimsList.ToList();
    }

  }
}
