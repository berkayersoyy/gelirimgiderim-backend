using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class TransactionDetailDto:IDto
    {
        public string TransactionId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryImagePath { get; set; }
        public string CategoryFileName { get; set; }
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public float Amount { get; set; }
        public double Date { get; set; }
    }
}