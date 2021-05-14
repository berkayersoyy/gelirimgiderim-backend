using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
  public interface IClaimService
  {
    IDataResult<List<Claim>> GetList(string room);
    IDataResult<Claim> Get(string claim);
    IResult Add(Claim claim);
    IResult Delete(Claim claim);
    IResult Update(Claim claim);
    IDataResult<List<Claim>> GetUserClaims(string room);
    IResult AddClaimToUser(UserClaim userClaim);
    IResult DeleteClaimFromUser(UserClaim userClaim);

  }
}
