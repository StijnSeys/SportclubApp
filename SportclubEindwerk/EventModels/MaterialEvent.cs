using SportClub.Data.EntityModels;

namespace SportClub.UI.EventModels
{
    public class MaterialEvent
    {

        public Club Club { get; private set; }

        public MaterialEvent(Club club)
        {

            Club = club;
        }




    }
}
