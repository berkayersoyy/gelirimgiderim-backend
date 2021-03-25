
using DataAccess.Abstract;
using DataAccess.Concrete.Firebase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests
{
    [TestClass]
    public class FirebaseTests
    {
        private ITransactionDal transactionDal;
        
        [TestMethod]
        public void Is_All_Data_Retrieved_From_Firestore()
        {
            transactionDal = new FbTransactionDal();
            var list = transactionDal.GetAll();
            Assert.AreEqual(1,list.Count);
        }
        
    }
}