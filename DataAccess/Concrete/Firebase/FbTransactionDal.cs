using System.Collections.Generic;
using System.Linq;
using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Concrete.Firebase
{
    public class FbTransactionDal:FirebaseRepositoryBase<Transaction>,ITransactionDal
    {
        private ICategoryDal _categoryDal;
        public FbTransactionDal(ICategoryDal categoryDal) : base(FirebaseCollections.Transactions)
        {
            _categoryDal = categoryDal;
        }

        public List<TransactionDetailDto> GetTransactionDetailDtos()
        {
            var result = from transaction in this.GetAll()
                join category in _categoryDal.GetAll() on transaction.CategoryId equals category.Id
                select new TransactionDetailDto
                {
                    TransactionId = transaction.Id,
                    RoomId = transaction.RoomId,
                    Description = transaction.Description,
                    UserId = transaction.UserId,
                    Date = transaction.Date,
                    Title = transaction.Title,
                    Amount = transaction.Amount,
                    CategoryFileName = category.ImageFileName,
                    CategoryImagePath = category.ImagePath,
                    CategoryId = category.Id
                };
            return result.ToList();
        }
    }
}