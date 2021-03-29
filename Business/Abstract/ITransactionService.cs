using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITransactionService
    {
        IDataResult<List<Transaction>> GetList();
        IDataResult<Transaction> Get(Transaction transaction);
        IResult Add(Transaction transaction);
        IResult Update(Transaction transaction);
        IResult Delete(Transaction transaction);
    }
}