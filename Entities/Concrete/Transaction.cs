using Core.Entities.Abstract;
using Google.Cloud.Firestore;
using Google.Type;

namespace Entities.Concrete
{
    [FirestoreData]
    public class Transaction:IEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string CategoryId { get; set; }
        [FirestoreProperty]
        public string Description { get; set; }
        [FirestoreProperty]
        public float Amount { get; set; }

        [FirestoreProperty] 
        public DateTime Date { get; set; }
    }
}