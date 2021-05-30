using System.Collections.Generic;
using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Core.Entities.Concrete
{
  [FirestoreData]
  public class Claim:IEntity
  {
    [FirestoreProperty]
    public string Id { get; set; }
    [FirestoreProperty]
    public string RoomId { get; set; }
    [FirestoreProperty]
    public string Name { get; set; }
    [FirestoreProperty]
    public List<string> ClaimProperties { get; set; }
  }
}
