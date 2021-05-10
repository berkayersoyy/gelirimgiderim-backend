using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class InvitationForJoinRoomDto:IDto
    {
        public string InvitationCode { get; set; }
    }
}