using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Entities.Concrete
{
    [FirestoreData]
    public class Category:IEntity
    {

        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string CategoryName { get; set; }
        [FirestoreProperty]
        public string ImagePath { get; set; }
        [FirestoreProperty]
        public string RoomId { get; set; }

        //TODO category management needed.
        //TODO doughnut chart management needed.
        //TODO business analyses needed.
    }
}