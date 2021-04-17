using Core.Entities.Concrete;

namespace Entities.Dtos
{
    public class UserForLeaveRoom
    {
        public User User { get; set; }
        public Room Room { get; set; }
    }
}