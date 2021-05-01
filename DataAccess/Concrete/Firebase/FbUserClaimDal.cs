using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.Firebase
{
  public class FbUserClaimDal:FirebaseRepositoryBase<UserClaim>,IUserClaimDal
  {
    public FbUserClaimDal() : base(FirebaseCollections.UserClaims)
    {
    }
  }
}
