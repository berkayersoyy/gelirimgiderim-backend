using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Firebase
{
    public class FbUserRoomDal:FirebaseRepositoryBase<UserRoom>,IUserRoomDal
    {
        public FbUserRoomDal() : base(FirebaseCollections.UserRooms)
        {
        }
    }
}