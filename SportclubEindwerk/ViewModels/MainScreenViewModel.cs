using System;
using Caliburn.Micro;
using Microsoft.Win32;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;
using SportClub.UI.EventModels;



namespace SportClub.UI.ViewModels
{
    public class MainScreenViewModel : Screen, IHandle<MainScreenEvent>
    {


        private readonly IEventAggregator _event;
        private readonly IClubService _clubService;


        public MainScreenViewModel(IEventAggregator events, IClubService clubService)
        {

            _event = events;
            events.Subscribe(this);

            _clubService = clubService;
        }

        private Club _club;
        private string _loginLogo;
        private string _loggedInClub;

        public string LoginLogo
        {
            get { return _loginLogo; }
            set
            {
                _loginLogo = value;
                NotifyOfPropertyChange(() => LoginLogo);

            }
        }

        public string LoggedInClub
        {
            get { return _loggedInClub; }
            set
            {
                _loggedInClub = value;
                NotifyOfPropertyChange(() => LoggedInClub);
            }
        }


        public void OrderMaterial()
        {

            _event.PublishOnUIThread(new MaterialEvent(_club));

        }

        public void MemberManagement()
        {
            _event.PublishOnUIThread(new MemberEvent(_club));
        }

        public void CreateMail()
        {
            _event.PublishOnUIThread(new MailEvent(_club));
        }


        public void ChangeLogo()
        {

            OpenFileDialog f = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.png) | *.jpg; *.png"
            };

            if (f.ShowDialog() == true)
            {

                Uri fileUri = new Uri(f.FileName);

                _club.ClubLogo = fileUri.ToString();
            }

            _clubService.UpdateSportClub(_club);
            _event.PublishOnUIThread(new MainScreenEvent(_club));
        }

        public void Handle(MainScreenEvent message)
        {

            LoginLogo = message.Club.ClubLogo;
            LoggedInClub = message.Club.Name;

            _club = message.Club;
        }































        //public void ReadMaterial()
        //{
        //    Excel.Application tryout = new Excel.Application();

        //    Excel.Workbook workbook = tryout.Workbooks.Open("C:\\Users\\user\\Desktop\\Eindwerk\\Materiaal.xlsx");

        //    Excel.Worksheet worksheet = workbook.Sheets[1];

        //    Excel.Range range = worksheet.UsedRange;

        //    Dictionary<string, Decimal> materialList = new Dictionary<string, decimal>();
        //    string key;

        //    Decimal price;

        //    for (int i = 0; i < range.Rows.Count; i++)
        //    {
        //        key = range.Cells[i, 1].Value.ToString();
        //        price = range.Cells[i, 2].Value.ToString();

        //        Material material = new Material();
        //        material.MaterialName = key;
        //        material.Price = price;


        //        _materialService.CreateMaterial(material);

        //    }

        //    foreach (Excel.Range item in range)
        //    {
        //        key = item.Cells[1, 1].ToString();


        //    }

        //}

    }
}
