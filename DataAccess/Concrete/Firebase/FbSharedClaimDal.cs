using System.Collections.Generic;
using System.Linq;
using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.Firebase
{
    public class FbSharedClaimDal:FirebaseRepositoryBase<SharedClaim>,ISharedClaimDal
    {
        private IUserClaimDal _userClaimDal;
        public FbSharedClaimDal(IUserClaimDal userClaimDal) : base(FirebaseCollections.SharedClaims)
        {
            _userClaimDal = userClaimDal;
        }
        public List<SharedClaim> GetUserSharedClaims(string userId,string roomId)
        {
            var userClaimsList = from userClaims in _userClaimDal.GetAll()
                join claim in this.GetAll() on userClaims.RoomId equals roomId
                where userClaims.UserId == userId && userClaims.ClaimId==claim.Id
                select new SharedClaim
                {
                    Id = claim.Id,
                    Name = claim.Name,
                    ClaimProperties = claim.ClaimProperties,
                };
            return userClaimsList.ToList();
        }
    }
}