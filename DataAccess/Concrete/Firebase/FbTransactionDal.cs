using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Firebase
{
    public class FbTransactionDal:FirebaseRepositoryBase<Transaction>,ITransactionDal
    {
        public FbTransactionDal() : base(FirebaseCollections.Transactions)
        {

        }
    }
}