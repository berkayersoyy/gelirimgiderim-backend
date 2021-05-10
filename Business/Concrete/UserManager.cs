
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Business.Concrete
{
    public class UserManager:IUserService
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

        public IDataResult<User> GetByMail(string email)
        {
            var userCheck = _userDal.GetAllUsersWithFirebase().SingleOrDefault(u => u.Email == email);
            return new SuccessDataResult<User>(userCheck,Messages.UserFetchedByMail);
        }

        public IDataResult<User> GetById(string id)
        {
          var userCheck = _userDal.GetAllUsersWithFirebase().SingleOrDefault(u => u.Id.Equals(id));
          return new SuccessDataResult<User>(userCheck,Messages.UserFetchedById); 

        }
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var result = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(result,Messages.UserClaimsFetched);
        }

        public IDataResult<User> GetCurrentUser()
        {
            //TODO delete jwt when expires
          string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
          var userCheck = GetById(userId);
          return new SuccessDataResult<User>(userCheck.Data,Messages.CurrentUserFetched); 
        }

    }
}
