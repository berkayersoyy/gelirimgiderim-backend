using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetList();
        IDataResult<List<Category>> GetListForRoom(string roomId);
        IDataResult<Category> Get(string categoryId);
        IDataResult<List<SharedCategory>> GetSharedList();
        IDataResult<SharedCategory> GetShared(string categoryId);
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);

    }
}
