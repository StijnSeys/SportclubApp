using SportClub.Data.EntityModels;

namespace SportClub.UI.EventModels
{
    public class MemberEvent
    {

        public Club Club { get; private set; }

        public MemberEvent(Club club)
        {

            Club = club;

        }
    }
}
