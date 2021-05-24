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
        private IStorageDal _storageDal;
        private IRoomService _roomService;
        private ISharedCategoryDal _sharedCategoryDal;

        public CategoryManager(ICategoryDal fbCategoryDal, IStorageDal storageDal, IRoomService roomService, ISharedCategoryDal sharedCategoryDal)
        {
            _categoryDal = fbCategoryDal;
            _storageDal = storageDal;
            _roomService = roomService;
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
            var currentRoom = _roomService.GetCurrentRoom().Data;
            var upload = _storageDal.Upload(category.ImagePath, currentRoom.Id);
            var list = upload.GetAwaiter().GetResult();
            category.ImagePath =  list[0];
            category.ImageFileName = list[1];
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }
        [ValidationAspect(typeof(CategoryValidator), Priority = 1)]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Category category)
        {
            var currentRoom = _roomService.GetCurrentRoom().Data;
            _categoryDal.Update(category);
            _storageDal.Delete(currentRoom.Id,category.ImageFileName);
            return new SuccessResult(Messages.CategoryUpdated);
        }
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Category category)
        {
            var currentRoom = _roomService.GetCurrentRoom().Data;
            _categoryDal.Delete(category);
            _storageDal.Delete(currentRoom.Id,category.ImageFileName);
            return new SuccessResult(Messages.CategoryDeleted);
        }
    }
}
