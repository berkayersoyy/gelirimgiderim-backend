using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Aspects.Autofac;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Claim = Core.Entities.Concrete.Claim;

namespace Business.Concrete
{
    public class ClaimManager : IClaimService
    {
        private IClaimDal _claimDal;
        private IUserClaimDal _userClaimDal;
        private IUserService _userService;
        private ISharedClaimDal _sharedClaimDal;

        public ClaimManager(IClaimDal claimDal, IUserClaimDal userClaimDal, IUserService userService, ISharedClaimDal sharedClaimDal)
        {
            _claimDal = claimDal;
            _userClaimDal = userClaimDal;
            _userService = userService;
            _sharedClaimDal = sharedClaimDal;
        }
        public IDataResult<List<Claim>> GetList(string room)
        {
            var claims = _claimDal.GetAll().Where(c => c.RoomId == room).ToList();
            return new SuccessDataResult<List<Claim>>(claims, Messages.ClaimsFetched);
        }

        public IDataResult<Claim> Get(string claim)
        {
            var claimCheck = _claimDal.GetAll().SingleOrDefault(c => c.Id == claim);
            return new SuccessDataResult<Claim>(claimCheck, Messages.ClaimFetched);
        }
        [SecuredOperation("claim")]
        public IResult Add(Claim claim)
        {
            _claimDal.Add(claim);
            return new SuccessResult(Messages.ClaimAdded);
        }
        [SecuredOperation("claim")]
        public IResult Delete(Claim claim)
        {
            _claimDal.Delete(claim);
            return new SuccessResult(Messages.ClaimDeleted);
        }
        [SecuredOperation("claim")]
        public IResult Update(Claim claim)
        {
            _claimDal.Update(claim);
            return new SuccessResult(Messages.ClaimUpdated);
        }

        public IDataResult<List<Claim>> GetUserClaims(string room)
        {
            var user = _userService.GetCurrentUser();
            var usersClaims = _claimDal.GetUserClaims(room, user.Data.Id);
            return new SuccessDataResult<List<Claim>>(usersClaims);
        }

        public IDataResult<List<SharedClaim>> GetUserSharedClaims(string room)
        {
            var user = _userService.GetCurrentUser();
            var userSharedClaims = _sharedClaimDal.GetUserSharedClaims(user.Data.Id,room);
            return new SuccessDataResult<List<SharedClaim>>(userSharedClaims);
        }
        [SecuredOperation("claim")]
        public IResult AddClaimToUser(UserClaim userClaim)
        {
            _userClaimDal.Add(userClaim);
            return new SuccessResult(Messages.ClaimAddedToUser);
        }
        [SecuredOperation("claim")]
        public IResult DeleteClaimFromUser(UserClaim userClaim)
        {
            var userClaimToBeDeleted = _userClaimDal.GetAll().SingleOrDefault(c =>
                c.ClaimId.Equals(userClaim.ClaimId) && c.RoomId.Equals(userClaim.RoomId) &&
                c.UserId.Equals(userClaim.UserId));
            _userClaimDal.Delete(userClaimToBeDeleted);
            return new SuccessResult(Messages.ClaimDeletedFromUser);
        }

        [SecuredOperation("claim")]
        public IResult UpdateClaimToUser(UserClaim userClaim)
        {
            _userClaimDal.Update(userClaim);
            return new SuccessResult(Messages.ClaimUpdatedToUser);
        }
        public IDataResult<List<SharedClaim>> GetSharedList()
        {
            var result = _sharedClaimDal.GetAll();
            return new SuccessDataResult<List<SharedClaim>>(result);
        }

        public IDataResult<SharedClaim> GetShared(string claimId)
        {
            var result = _sharedClaimDal.GetAll().SingleOrDefault(c => c.Id.Equals(claimId));
            return new SuccessDataResult<SharedClaim>(result);
        }
    }
}
