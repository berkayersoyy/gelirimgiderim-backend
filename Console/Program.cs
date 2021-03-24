using System;
using Core.DataAccess.Firebase;
using DataAccess.Concrete.Firebase;
using Entities.Concrete;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            FbTransactionDal transactionDal = new FbTransactionDal();
            transactionDal.Add(new Transaction
            {
                Amount = 10,
                Description = "aa",
                CategoryId = "aa"
            });
        }
    }
}
