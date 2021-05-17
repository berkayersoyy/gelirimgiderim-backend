
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var userCheck = _userDal.GetAllUsersWithFirebase().SingleOrDefault(u => u.Email == email);
            return new SuccessDataResult<User>(userCheck, Messages.UserFetchedByMail);
        }

        public IDataResult<User> GetById(string id)
        {
            var userCheck = _userDal.GetAllUsersWithFirebase().SingleOrDefault(u => u.Id.Equals(id));
            return new SuccessDataResult<User>(userCheck, Messages.UserFetchedById);

        }
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var result = _userDal.GetClaims(user.Id);
            return new SuccessDataResult<List<OperationClaim>>(result, Messages.UserClaimsFetched);
        }

        public IDataResult<User> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new SecurityTokenExpiredException();
            }
            var userCheck = GetById(userId.Value);
            return new SuccessDataResult<User>(userCheck.Data, Messages.CurrentUserFetched);
        }

    }
}
