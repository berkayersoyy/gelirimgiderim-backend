using System;
using System.Collections.Generic;
using Core.Constants;
using Core.Entities.Abstract;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace Core.DataAccess.Firebase
{
    public class FirebaseRepositoryBase<T> : IEntityRepository<T>
    where T : class, IEntity, new()
    {
        private string _collectionName;
        private FirestoreDb _firestoreDb;

        public FirebaseRepositoryBase(string collectionName)
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
            documentReference.SetAsync(entity, SetOptions.MergeAll).GetAwaiter().GetResult();
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
                return tempEntity;
            }
            else
            {
                return null;
                // RESULT REFACTORING YAPILACAK
            }
        }

        public List<T> GetAll()
        {
            Query query = _firestoreDb.Collection(_collectionName);
            return QuerySnapshots(query);
        }

        public void Update(T entity)
        {
            DocumentReference documentReference = _firestoreDb.Collection(_collectionName).Document(entity.Id);
            documentReference.SetAsync(entity,SetOptions.MergeAll).GetAwaiter().GetResult();
        }

        public List<T> QuerySnapshots(Query query)
        {
            QuerySnapshot snapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<T> list = new List<T>();

            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string,object> entity= documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(entity);
                    T newEntity = JsonConvert.DeserializeObject<T>(json);
                    newEntity.Id = documentSnapshot.Id;
                    list.Add(newEntity);
                }
            }

            return list;
        }
    }
}