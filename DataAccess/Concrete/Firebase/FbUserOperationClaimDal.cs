using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.Firebase
{
    public class FbUserOperationClaimDal:FirebaseRepositoryBase<UserOperationClaim>,IUserOperationClaimDal
    {
        public FbUserOperationClaimDal() : base(FirebaseCollections.UserOperationClaims)
        {
        }
    }
}