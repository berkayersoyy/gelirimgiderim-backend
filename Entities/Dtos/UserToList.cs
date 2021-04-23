using Core.Entities.Abstract;
using Google.Cloud.Firestore;

namespace Entities.Dtos
{
  [FirestoreData]
  public class UserToList : IDto
  {
    [FirestoreProperty]
    public string Id { get; set; }
    [FirestoreProperty]
    public string Email { get; set; }
    [FirestoreProperty]
    public string FirstName { get; set; }
    [FirestoreProperty]
    public string LastName { get; set; }
    [FirestoreProperty]
    public bool Status { get; set; }
  }
}
