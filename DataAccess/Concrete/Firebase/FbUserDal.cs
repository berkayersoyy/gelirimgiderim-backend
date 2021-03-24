using Core.Constants;
using Core.DataAccess.Firebase;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.Firebase
{
    public class FbUserDal:FirebaseRepositoryBase<User>
    {
        public FbUserDal(string users = "users") : base(users)
        {

        }
    }
}