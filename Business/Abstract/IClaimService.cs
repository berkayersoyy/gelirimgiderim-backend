using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
  public interface IClaimService
  {
    IDataResult<List<Claim>> GetList(Room room);
    IDataResult<Claim> Get(Claim claim);
    IResult Add(Claim claim);
    IResult Delete(Claim claim);
    IResult Update(Claim claim);
    IDataResult<List<Claim>> GetUsersClaims(Room room);
    IResult AddClaimToUser(UserClaim userClaim);
    IResult DeleteClaimFromUser(UserClaim userClaim);

  }
}
