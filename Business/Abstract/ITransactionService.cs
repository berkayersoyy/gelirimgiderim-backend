using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITransactionService
    {
        IDataResult<List<Transaction>> GetList();
        IDataResult<Transaction> Get(string id);
        IDataResult<List<Transaction>> GetTransactionsForRoom(string roomId);
        IResult Add(Transaction transaction);
        IResult Update(Transaction transaction);
        IResult Delete(Transaction transaction);
    }
}
