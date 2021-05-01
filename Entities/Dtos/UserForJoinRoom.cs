using System;
using Core.Entities.Concrete;

namespace Entities.Dtos
{
  [Obsolete]
    public class UserForJoinRoom
    {
        public User User { get; set; }
        public string Invitation { get; set; }
    }
}
