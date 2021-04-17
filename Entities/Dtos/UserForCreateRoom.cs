using Core.Entities.Abstract;
using Core.Entities.Concrete;

namespace Entities.Dtos
{
    public class UserForCreateRoom:IDto
    {
        public User User { get; set; }
        public Room Room { get; set; }
    }
}