
using SportClub.Data.EntityModels;

namespace SportClub.UI.EventModels
{
    public class MainScreenEvent
    {

        public Club Club { get; private set; }

        public MainScreenEvent(Club club)
        {

            Club = club;
        }

    }
}
