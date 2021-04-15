
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Constants;
using Core.DataAccess.FirebaseDatabase;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using User = Core.Entities.Concrete.User;

namespace DataAccess.Concrete.Firebase
{
    public class FbUserDal : FirebaseRepositoryBase<User>, IUserDal
    {
        private IUserOperationClaimDal _userOperationClaimDal;
        private IOperationClaimDal _operationClaimDal;

        public FbUserDal(IOperationClaimDal operationClaimDal, IUserOperationClaimDal userOperationClaimDal) : base(
            FirebaseCollections.Users)
        {
            _operationClaimDal = operationClaimDal;
            _userOperationClaimDal = userOperationClaimDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            var operationClaims = from operationClaim in _operationClaimDal.GetAll()
                join userOperationClaim in _userOperationClaimDal.GetAll()
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == user.Id
                select new OperationClaim
                {
                    Id = operationClaim.Id,
                    Name = operationClaim.Name
                };
            return operationClaims.ToList();
        }

        public List<User> GetAllUsersWithFirebase()
        {
            FirestoreDb firestoreDb = FirestoreDb.Create(FirebasePaths.ProjectId);
            Query query = firestoreDb.Collection(FirebaseCollections.Users);
            QuerySnapshot snapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<User> list = new List<User>();
            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> entity = documentSnapshot.ToDictionary();
                    var passwordHash = (Blob)entity["PasswordHash"];
                    var passwordSalt= (Blob)entity["PasswordSalt"];
                    string json = JsonConvert.SerializeObject(entity);
                    User newEntity = JsonConvert.DeserializeObject<User>(json);
                    newEntity.Id = documentSnapshot.Id;
                    newEntity.PasswordHash = passwordHash;
                    newEntity.PasswordSalt = passwordSalt;
                    list.Add(newEntity);
                }
            }

            return list;
        }
        [Obsolete("non required")]
        private string ManipulateData(string data, string dataToManipulate)
        {
            string manipulatedData = "";
            int index = data.IndexOf(dataToManipulate)+29;
            for (int i = index; i < data.Length; i++)
            {
                if (data[i].Equals(']'))
                {
                    break;
                }

                manipulatedData += data[i];
            }

            return manipulatedData;
        }
    }
}