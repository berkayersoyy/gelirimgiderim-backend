using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Entities.Concrete
{
    [FirestoreData]
    public class SharedCategory:IEntity
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string CategoryName { get; set; }
        [FirestoreProperty] public string ImagePath { get; set; }
        [FirestoreProperty] public string ImageFileName { get; set; }
    }
}