using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Core.Entities.Concrete
{
    [FirestoreData]
    public class Invitation:IEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string RoomId { get; set; }
        [FirestoreProperty]
        public string Code { get; set; }
        ///TODO Expire time will be added
    }
}