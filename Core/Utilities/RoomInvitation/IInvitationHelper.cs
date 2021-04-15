using Core.Entities.Concrete;

namespace Core.Utilities.RoomInvitation
{
    public interface IInvitationHelper
    {
        public Invitation CreateInvitation(Room room);
    }
}