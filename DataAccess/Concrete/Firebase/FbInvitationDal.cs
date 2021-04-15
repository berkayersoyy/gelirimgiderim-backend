using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.Firebase
{
    public class FbInvitationDal:FirebaseRepositoryBase<Invitation>,IInvitationDal
    {
        public FbInvitationDal() : base(FirebaseCollections.Invitations)
        {
        }
    }
}