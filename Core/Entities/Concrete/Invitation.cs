using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class Invitation:IEntity
    {
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string Code { get; set; }
    }
}