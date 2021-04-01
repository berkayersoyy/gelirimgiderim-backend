using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Firebase
{
    public class FbCategoryDal:FirebaseRepositoryBase<Category>,ICategoryDal
    {
        public FbCategoryDal() : base(FirebaseCollections.Categories)
        {
        }
    }
}