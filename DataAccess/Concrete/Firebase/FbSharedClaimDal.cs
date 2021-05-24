using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.Firebase
{
    public class FbSharedClaimDal:FirebaseRepositoryBase<Claim>,ISharedClaimDal
    {
        public FbSharedClaimDal() : base(FirebaseCollections.SharedClaims)
        {
        }
    }
}