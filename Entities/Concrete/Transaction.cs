
using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Entities.Concrete
{
    [FirestoreData]
    public class Transaction : IEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string RoomId { get; set; }
        [FirestoreProperty]
        public string CategoryId { get; set; }
        [FirestoreProperty]
        public string Description { get; set; }
        [FirestoreProperty]
        public float Amount { get; set; }

        [FirestoreProperty]
        public string Date { get; set; }
    }
}