using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Firebase
{
    public class FbSharedCategoryDal:FirebaseRepositoryBase<Category>,ISharedCategoryDal
    {
        public FbSharedCategoryDal() : base(FirebaseCollections.SharedCategories)
        {
        }
    }
}