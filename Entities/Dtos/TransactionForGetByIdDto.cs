using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class TransactionForGetByIdDto:IDto
    {
        public string Id { get; set; }
    }
}