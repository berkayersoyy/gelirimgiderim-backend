



using System.Collections.Generic;
using Core.Entities.Concrete;
using DataAccess.Concrete.Firebase;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> ab = new List<string>();
            ab.Add("category");
            ab.Add("transaction");
            ab.Add("room");
            ab.Add("claim");
            FbSharedClaimDal a = new FbSharedClaimDal();
            a.Add(new SharedClaim
            {
                Name = "admin",
                ClaimProperties = ab
            });
        }


  
    }
}
