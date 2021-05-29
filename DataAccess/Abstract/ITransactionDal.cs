using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface ITransactionDal:IEntityRepository<Transaction>
    {
        List<TransactionDetailDto> GetTransactionDetailDtos();
    }
}