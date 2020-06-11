using SportClub.Data.EntityModels;

namespace SportClub.UI.EventModels
{
    public class MailEvent
    {

        public Club Club { get; private set; }

        public string FilePath { get; private set; }

        public MailEvent(Club club)
        {

            Club = club;

        }

        public MailEvent(Club club, string filePath)
        {
            Club = club;
            FilePath = filePath;
        }
    }
}
