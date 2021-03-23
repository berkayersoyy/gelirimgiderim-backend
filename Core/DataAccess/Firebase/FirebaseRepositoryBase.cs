using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Constants;
using Core.DataAccess.Abstract;
using Google.Cloud.Firestore;

namespace Core.DataAccess.Firebase
{
    public class FirebaseEntityRepositoryBase<T> : IEntityRepository<T>
    where T : class, IEntity, new()
    {
        private string _collectionName;
        private FirestoreDb _firestoreDb;

        public FirebaseEntityRepositoryBase(string collectionName)
        {
            string filePath = FirebasePaths.FilePath;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            _firestoreDb = FirestoreDb.Create(FirebasePaths.ProjectId);
            _collectionName = collectionName;
        }
        public void Add(T entity)
        {
            CollectionReference collectionReference = _firestoreDb.Collection(_collectionName);
            DocumentReference documentReference = collectionReference.AddAsync(entity).GetAwaiter().GetResult();
            entity.Id = documentReference.Id;
        }

        public void Delete(T entity)
        {
            DocumentReference documentReference = _firestoreDb.Collection(_collectionName).Document(entity.Id);
            documentReference.DeleteAsync().GetAwaiter().GetResult();
        }

        public T Get(T entity)
        {
            DocumentReference documentReference = _firestoreDb.Collection(_collectionName).Document(entity.Id);
            DocumentSnapshot snapshot = documentReference.GetSnapshotAsync().GetAwaiter().GetResult();
            if (snapshot.Exists)
            {
                T tempEntity = snapshot.ConvertTo<T>();
                tempEntity.Id = snapshot.Id;
                return tempEntity;
            }
            else
            {
                return null;
            }
        }

        public List<T> GetAll(Expression<Func<bool, T>> expression = null)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            DocumentReference documentReference = _firestoreDb.Collection(_collectionName).Document(entity.Id);

        }
    }
}