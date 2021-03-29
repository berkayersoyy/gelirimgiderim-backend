using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal fbCategoryDal)
        {
            _categoryDal = fbCategoryDal;
        }

        public IDataResult<List<Category>> GetList()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(),Messages.CategoriesFetched);
        }

        public IDataResult<Category> Get(Category category)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(category),Messages.CategoryFetched);
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }

        public IResult Delete(Category category)
        {
            _fbCategoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }
    }
}