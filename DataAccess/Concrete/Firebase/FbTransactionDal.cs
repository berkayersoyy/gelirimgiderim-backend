using Core.Constants;
using Core.DataAccess.Firebase;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Firebase
{
    public class FbTransactionDal:FirebaseRepositoryBase<Transaction>,ITransactionDal
    {
        public FbTransactionDal(string transactions = "transactions") : base(transactions)
        {

        }
    }
}