using Core.Constants;
using Core.DataAccess.Firebase;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.Firebase
{
    public class FbUserDal:FirebaseRepositoryBase<User>,IUserDal
    {
        public FbUserDal() : base(FirebaseCollections.Users)
        {

        }
    }
}