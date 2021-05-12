using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
  public interface IClaimDal:IEntityRepository<Claim>
  {
    List<Claim> GetUserClaims(string room, string userId);

  }
}
