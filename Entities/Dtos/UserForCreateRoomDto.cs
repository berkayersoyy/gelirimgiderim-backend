using System;
using Core.Entities.Abstract;
using Core.Entities.Concrete;

namespace Entities.Dtos
{
  [Obsolete]
    public class UserForCreateRoomDto:IDto
    {
        public User User { get; set; }
        public Room Room { get; set; }
    }
}
