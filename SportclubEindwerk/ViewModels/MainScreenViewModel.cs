using Caliburn.Micro;
using Sportclub.UI.EventModels;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using SportClub.UI.EventModels;
using Excel = Microsoft.Office.Interop.Excel;



namespace SportClub.UI.ViewModels
{
    public class MainScreenViewModel : Screen , IHandle<MainScreenEvent>
    {

        private readonly IMaterialService _materialService;

        public MainScreenViewModel(IMaterialService materialService, IEventAggregator events)
        {
            _materialService = materialService;

            events.Subscribe(this);
        }

        private string _loginLogo;
        private string _loggedInClub;

        public string LoginLogo
        {
            get { return _loginLogo; }
            set
            {
                _loginLogo = value;
                NotifyOfPropertyChange(()=> LoginLogo);

            }
        }

        public string LoggedInClub
        {
            get { return _loggedInClub; }
            set
            {
                _loggedInClub = value;
                NotifyOfPropertyChange(()=> LoggedInClub);
            }
        }

        private List<string> _memberList;

        public List<string> MemberList
        {
            get { return _memberList; }
            set
            {
                _memberList = value;
                NotifyOfPropertyChange(()=> MemberList);
            }
        }


        public void Handle(MainScreenEvent message)
        {
            List<string> membersName = new List<string>();
            LoginLogo = message.Club.ClubLogo;
            LoggedInClub = message.Club.Name;

            foreach (var member in message.Club.Members)
            {
                membersName.Add(member.FirstName + "  "+ member.LastName);
            }

            MemberList = membersName;
        }































        public void ReadMaterial()
        {
            Excel.Application tryout = new Excel.Application();

            Excel.Workbook workbook = tryout.Workbooks.Open("C:\\Users\\user\\Desktop\\Eindwerk\\Materiaal.xlsx");

            Excel.Worksheet worksheet = workbook.Sheets[1];

            Excel.Range range = worksheet.UsedRange;

            Dictionary<string, Decimal> materialList = new Dictionary<string, decimal>();
            string key;

            Decimal price;

            for (int i = 0; i < range.Rows.Count; i++)
            {
                key = range.Cells[i, 1].Value.ToString();
                price = range.Cells[i, 2].Value.ToString();

                Material material = new Material();
                material.MaterialName = key;
                material.Price = price;
                

                _materialService.CreateMaterial(material);

            }

            foreach (Excel.Range item in range)
            {
                key = item.Cells[1, 1].ToString();


            }

        }

    }
}
