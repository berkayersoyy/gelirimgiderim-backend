using Core.Entities.Abstract;
using Entities.Concrete;

namespace Entities.Dtos
{
    public class CategoryWithRoomId:IDto
    {
        public Category Category { get; set; }
        public string RoomId { get; set; }
    }
}