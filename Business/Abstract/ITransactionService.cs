using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITransactionService
    {
        IDataResult<List<Transaction>> GetList();
        IDataResult<Transaction> Get(string transactionId);
        IDataResult<List<Transaction>> GetTransactionsForRoom(Room room);
        IResult Add(Transaction transaction);
        IResult Update(Transaction transaction);
        IResult Delete(Transaction transaction);
    }
}