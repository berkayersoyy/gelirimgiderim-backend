using Google.Cloud.Firestore;

namespace Core.Entities.Abstract
{
    [FirestoreData]
    public interface IEntity
    {
        [FirestoreProperty]
        string Id { get; set; }
    }
}