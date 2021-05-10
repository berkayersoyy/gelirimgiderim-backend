using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class UserForGetByMailDto:IDto
    {
        public string Email { get; set; }
    }
}