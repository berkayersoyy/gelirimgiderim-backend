using System;
using System.Collections.Generic;
using System.Text;
using Core.Constants;
using Core.Entities.Abstract;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace Core.DataAccess.FirebaseDatabase
{
    public class FirebaseRepositoryBase<T> : IEntityRepository<T>
    where T : class, IEntity, new()
    {
        private string _collectionName;
        private FirestoreDb _firestoreDb;

        /// <summary>
        /// Firebase Repository for doing CRUD operations with entities
        /// </summary>
        /// <param name="collectionName"></param>
        public FirebaseRepositoryBase(string collectionName)
        {
            string filePath = FirebasePaths.FilePathForLocal;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            _firestoreDb = FirestoreDb.Create(FirebasePaths.ProjectId);
            _collectionName = collectionName;
        }
        /// <summary>
        /// Add entity to firebase database.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            CollectionReference collectionReference = _firestoreDb.Collection(_collectionName);
            DocumentReference documentReference = collectionReference.AddAsync(entity).GetAwaiter().GetResult();
            entity.Id = documentReference.Id;
            documentReference.SetAsync(entity, SetOptions.MergeAll).GetAwaiter().GetResult();
        }
        /// <summary>
        /// Deletes entity from firebase database.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            DocumentReference documentReference = _firestoreDb.Collection(_collectionName).Document(entity.Id);
            documentReference.DeleteAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Fetches the entity from firebase database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns><typeparam name="T"></typeparam></returns>
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
            }
        }
        /// <summary>
        /// Fetches the list of entities from firebase database.
        /// </summary>
        /// <returns><typeparam name="T"></typeparam>List of T</returns>
        public List<T> GetAll()
        {
            Query query = _firestoreDb.Collection(_collectionName);
            return QuerySnapshots(query);
        }
        /// <summary>
        /// Updates the entity to firebase database.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            DocumentReference documentReference = _firestoreDb.Collection(_collectionName).Document(entity.Id);
            documentReference.SetAsync(entity,SetOptions.MergeAll).GetAwaiter().GetResult();
        }
        /// <summary>
        /// Queries the firebase database for data which want to be fetched.
        /// </summary>
        /// <param name="query"></param>
        /// <returns><typeparam name="T">List of T</typeparam></returns>
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