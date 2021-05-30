
using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Aspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using DateTime = System.DateTime;
using Transaction = Entities.Concrete.Transaction;

namespace Business.Concrete
{
    public class TransactionManager:ITransactionService
    {
        private ITransactionDal _transactionDal;
        private IUserService _userService;
        private IRoomService _roomService;

        public TransactionManager(ITransactionDal fbTransactionDal, IUserService userService, IRoomService roomService)
        {
            _transactionDal = fbTransactionDal;
            _userService = userService;
            _roomService = roomService;
        }
        [CacheAspect(60)]
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

        public IDataResult<TransactionDetailDto> GetTransactionDetailDto(string transactionId)
        {
            var result = _transactionDal.GetTransactionDetailDtos().SingleOrDefault(t => t.TransactionId.Equals(transactionId));
            return new SuccessDataResult<TransactionDetailDto>(result);
        }

        [ValidationAspect(typeof(TransactionValidator), Priority = 1)]
        [CacheRemoveAspect("ITransactionService.Get")]
        [SecuredOperation("transaction")]
        public IResult Add(Transaction transaction)
        {
            transaction.Date = DateTime.UtcNow
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;
            transaction.UserId = _userService.GetCurrentUser().Data.Id;
            _transactionDal.Add(transaction);
            return new SuccessResult(Messages.TransactionAdded);
        }
        [ValidationAspect(typeof(TransactionValidator), Priority = 1)]
        [CacheRemoveAspect("ITransactionService.Get")]
        [SecuredOperation("transaction")]
        public IResult Update(Transaction transaction)
        {
            _transactionDal.Update(transaction);
            return new SuccessResult(Messages.TransactionUpdated);
        }
        [CacheRemoveAspect("ITransactionService.Get")]
        [SecuredOperation("transaction")]
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

        public IDataResult<List<TransactionDetailDto>> GetTransactionDetailDtos(string roomId)
        {
            var result = _transactionDal.GetTransactionDetailDtos().Where(c => c.RoomId.Equals(roomId)).ToList();
            return new SuccessDataResult<List<TransactionDetailDto>>(result);
        }

        public IDataResult<List<Transaction>> GetTransactionsByCategory(string categoryId)
        {
            var room = _roomService.GetCurrentRoom();
            var transactions = GetTransactionsForRoom(room.Data.Id);
            var classifiedTransactions = transactions.Data.Where(t => t.CategoryId.Equals(categoryId)).ToList();
            return new SuccessDataResult<List<Transaction>>(classifiedTransactions);
        }
    }
}
