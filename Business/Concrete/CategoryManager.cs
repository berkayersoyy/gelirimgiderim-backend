using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
            var result = _categoryDal.GetAll();
            return new SuccessDataResult<List<Category>>(result,Messages.CategoriesFetched);
        }

        public IDataResult<Category> Get(string id)
        {
            var result = _categoryDal.GetAll().SingleOrDefault(c => c.Id.Equals(id));
            return new SuccessDataResult<Category>(result,Messages.CategoryFetched);
        }
        [ValidationAspect(typeof(CategoryValidator))]
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
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }
    }
}
