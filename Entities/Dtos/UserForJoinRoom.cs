using Core.Entities.Concrete;

namespace Entities.Dtos
{
    public class UserForJoinRoom
    {
        public User User { get; set; }
        public string Invitation { get; set; }
    }
}