using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Core.Entities.Concrete
{
  [FirestoreData]
  public class UserClaim:IEntity
  {
    [FirestoreProperty]
    public string Id { get; set; }
    [FirestoreProperty]
    public string UserId { get; set; }
    [FirestoreProperty]
    public string ClaimId { get; set; }
    [FirestoreProperty]
    public string RoomId { get; set; }
  }
}
