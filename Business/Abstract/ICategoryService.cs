using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetList();
        IDataResult<Category> Get(Category category);
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);

    }
}