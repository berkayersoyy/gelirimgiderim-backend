using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Entities.Concrete
{
    [FirestoreData]
    public class UserRoom:IEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string UserId { get; set; }
        [FirestoreProperty]
        public string RoomId { get; set; }
    }
}