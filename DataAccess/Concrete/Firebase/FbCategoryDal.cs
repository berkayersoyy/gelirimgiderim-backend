﻿using Core.DataAccess.Firebase;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Firebase
{
    public class FbCategoryDal:FirebaseRepositoryBase<Category>,ICategoryDal
    {
        public FbCategoryDal(string collectionName="categories") : base(collectionName)
        {
        }
    }
}