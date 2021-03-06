using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;


namespace DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(string userId);
        List<User> GetAllUsersWithFirebase();
    }
}