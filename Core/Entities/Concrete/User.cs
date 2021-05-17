using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Core.Entities.Concrete
{
    [FirestoreData]
    public class User:IEntity
    {
        [FirestoreProperty] 
        public string Id { get; set; }
        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string LastName { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public Blob PasswordHash { get; set; }
        [FirestoreProperty]
        public Blob PasswordSalt { get; set; }
        [FirestoreProperty]
        public bool Status { get; set; }
    }
    //TODO Add Aspects to Business and business rules to business
}