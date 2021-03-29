using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Firebase;
using Entities.Concrete;

namespace Business.Concrete
{
    public class TransactionManager:ITransactionService
    {
        private ITransactionDal _transactionDal;

        public TransactionManager(ITransactionDal fbTransactionDal)
        {
            _transactionDal = fbTransactionDal;
        }

        public IDataResult<List<Transaction>> GetList()
        {
            return new SuccessDataResult<List<Transaction>>(_transactionDal.GetAll(),Messages.TransactionsFetched);
        }

        public IDataResult<Transaction> Get(Transaction transaction)
        {
            return new SuccessDataResult<Transaction>(_transactionDal.Get(transaction),Messages.TransactionFetched);
        }

        public IResult Add(Transaction transaction)
        {
            _transactionDal.Add(transaction);
            return new SuccessResult(Messages.TransactionAdded);
        }

        public IResult Update(Transaction transaction)
        {
            _transactionDal.Update(transaction);
            return new SuccessResult(Messages.TransactionUpdated);
        }

        public IResult Delete(Transaction transaction)
        {
            _fbTransactionDal.Delete(transaction);
            return new SuccessResult(Messages.TransactionDeleted);
        }
    }
}