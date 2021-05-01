using System;
using Core.Entities.Concrete;

namespace Entities.Dtos
{
  [Obsolete]
    public class UserForLeaveRoom
    {
        public User User { get; set; }
        public Room Room { get; set; }
    }
}
