using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class UserRoom:IEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoomId { get; set; }
    }
}