using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.Firebase
{
    public class FbOperationClaimDal:FirebaseRepositoryBase<OperationClaim>,IOperationClaimDal
    {
        public FbOperationClaimDal() : base(FirebaseCollections.OperationClaims)
        {
        }
    }
}