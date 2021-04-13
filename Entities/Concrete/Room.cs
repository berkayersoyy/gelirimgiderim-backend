using System.Collections.Generic;
using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Entities.Concrete
{
    [FirestoreData]
    public class Room:IEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }

    }
}