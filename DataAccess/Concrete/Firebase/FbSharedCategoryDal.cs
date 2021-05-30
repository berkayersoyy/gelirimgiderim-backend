using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Firebase
{
    public class FbSharedCategoryDal:FirebaseRepositoryBase<SharedCategory>,ISharedCategoryDal
    {
        public FbSharedCategoryDal() : base(FirebaseCollections.SharedCategories)
        {
        }
    }
}