using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Delete(User user);
        IDataResult<User> GetByMail(string email);
        IDataResult<User> GetById(string id);
        IDataResult<List<OperationClaim>> GetClaims(User user);

        IDataResult<User> GetCurrentUser();
    }
}
