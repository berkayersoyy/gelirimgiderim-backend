using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ISharedClaimDal:IEntityRepository<SharedClaim>
    {
        List<SharedClaim> GetUserSharedClaims(string userId,string roomId);
    }
}