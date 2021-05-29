using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;
        private ISharedCategoryDal _sharedCategoryDal;

        public CategoryManager(ICategoryDal fbCategoryDal, ISharedCategoryDal sharedCategoryDal)
        {
            _categoryDal = fbCategoryDal;
            _sharedCategoryDal = sharedCategoryDal;
        }
        [CacheAspect(duration:60)]
        public IDataResult<List<Category>> GetList()
        {
            var result = _categoryDal.GetAll();
            return new SuccessDataResult<List<Category>>(result,Messages.CategoriesFetched);
        }

        public IDataResult<List<Category>> GetListForRoom(string roomId)
        {
            var result = _categoryDal.GetAll().Where(c => c.RoomId.Equals(roomId)).ToList();
            return new SuccessDataResult<List<Category>>(result);
        }
        public IDataResult<Category> Get(string categoryId)
        {
            var result = _categoryDal.GetAll().SingleOrDefault(c => c.Id.Equals(categoryId));
            return new SuccessDataResult<Category>(result,Messages.CategoryFetched);
        }

        public IDataResult<List<Category>> GetSharedList()
        {
            var result = _sharedCategoryDal.GetAll();
            return new SuccessDataResult<List<Category>>(result);
        }

        public IDataResult<Category> GetShared(string categoryId)
        {
            var result = _sharedCategoryDal.GetAll().SingleOrDefault(c => c.Id.Equals(categoryId));
            return new SuccessDataResult<Category>(result);
        }

        [ValidationAspect(typeof(CategoryValidator),Priority = 1)]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }
        [ValidationAspect(typeof(CategoryValidator), Priority = 1)]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }
    }
}
