using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ITransactionService
    {
        IDataResult<List<Transaction>> GetList();
        IDataResult<Transaction> Get(string id);
        IDataResult<List<Transaction>> GetTransactionsForRoom(string roomId);
        IDataResult<List<TransactionDetailDto>> GetTransactionDetailDtos(string roomId);
        IDataResult<TransactionDetailDto> GetTransactionDetailDto(string id);
        IResult Add(Transaction transaction);
        IResult Update(Transaction transaction);
        IResult Delete(Transaction transaction);
        IDataResult<List<Transaction>> GetTransactionsByCategory(string categoryId);
    }
}
