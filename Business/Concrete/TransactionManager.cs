
using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DateTime = System.DateTime;
using Transaction = Entities.Concrete.Transaction;

namespace Business.Concrete
{
    public class TransactionManager:ITransactionService
    {
        private ITransactionDal _transactionDal;
        private IUserService _userService;

        public TransactionManager(ITransactionDal fbTransactionDal, IUserService userService)
        {
            _transactionDal = fbTransactionDal;
            _userService = userService;
        }

        public IDataResult<List<Transaction>> GetList()
        {
            var result = _transactionDal.GetAll();
            return new SuccessDataResult<List<Transaction>>(result,Messages.TransactionsFetched);
        }

        public IDataResult<Transaction> Get(string transactionId)
        {
            var result = _transactionDal.GetAll().SingleOrDefault(t => t.Id.Equals(transactionId));
            return new SuccessDataResult<Transaction>(result,Messages.TransactionFetched);
        }
        [ValidationAspect(typeof(TransactionValidator))]
        public IResult Add(Transaction transaction)
        {
            transaction.Date = DateTime.UtcNow
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;
            transaction.UserId = _userService.GetCurrentUser().Data.Id;
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

        public IDataResult<List<Transaction>> GetTransactionsForRoom(string roomId)
        {
            var result = _transactionDal.GetAll().Where(t => t.RoomId == roomId).ToList();
            return new SuccessDataResult<List<Transaction>>(result, Messages.TransactionsFetched);
        }
    }
}
