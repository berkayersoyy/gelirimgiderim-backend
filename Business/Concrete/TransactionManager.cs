using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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

        public IDataResult<Transaction> Get(string transactionId)
        {
            return new SuccessDataResult<Transaction>(_transactionDal.GetAll().SingleOrDefault(t=>t.Id.Equals(transactionId)),Messages.TransactionFetched);
        }
        [ValidationAspect(typeof(TransactionValidator))]
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
            _transactionDal.Delete(transaction);
            return new SuccessResult(Messages.TransactionDeleted);
        }
    }
}