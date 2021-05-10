
using Core.Entities.Concrete;

namespace Core.Utilities.RoomInvitation
{
    public class InvitationHelper:IInvitationHelper
    {
        private ICodeGenerator _generator;

        public InvitationHelper(ICodeGenerator generator)
        {
            _generator = generator;
        }

        public Invitation CreateInvitation(Room room)
        {
            return new Invitation
            {
                InvitationCode = _generator.Generate(),
                RoomId = room.Id
            };
        }
    }
}