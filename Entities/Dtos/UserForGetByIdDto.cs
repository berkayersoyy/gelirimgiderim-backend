using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class UserForGetByIdDto:IDto
    {
        public string Id { get; set; }
    }
}