using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        var userClaimsList = from userClaim in _userClaimDal.GetAll()
        join claim in GetAll() on userClaim.RoomId equals room
        where userClaim.UserId == userId && userClaim.ClaimId == claim.Id
        select new Claim
        {
          Id = claim.Id,
          Name = claim.Name,
          ClaimProperties = claim.ClaimProperties,
          RoomId = claim.RoomId,
        };
        return userClaimsList.ToList();
    }

  }
}
