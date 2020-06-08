using SportClub.Data.EntityModels;

namespace SportClub.UI.EventModels
{
    public class WordEvent
    {

        public Club Club { get; private set; }

        public WordEvent(Club club)
        {

            Club = club;

        }


    }
}
