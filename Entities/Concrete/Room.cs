using System.Collections.Generic;
using Core.Entities.Abstract;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Room:IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

    }
}