using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Microsoft.Win32;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;
using Sportclub.UI.EventModels;


namespace SportClub.UI.ViewModels
{
    public class RegisterViewModel : Screen
    {

        private string _clubName;
        private string _password;
        private string _password2;
        private string _clubLogo = "../Resources/Images/no-picture.png";
        private string _errorMessage;

        private readonly IClubService _clubService;
        private readonly IEventAggregator _events;
        


        public RegisterViewModel(IClubService clubService, IEventAggregator events)
        {
            _clubService = clubService;
            _events = events;
            events.Subscribe(this);
        }

        public string ClubName
        {
            get
            {
                return _clubName;
            }
            set
            {
                _clubName = value;
                NotifyOfPropertyChange(() => ClubName);
            }
        }

        public string PassWord
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => PassWord);
            }
        }

        public string PassWord2
        {
            get
            {
                return _password2;
            }
            set
            {
                _password2 = value;
                NotifyOfPropertyChange(() => PassWord2);
            }
        }


        public string ClubLogo
        {
            get
            {
                return _clubLogo;
            }
            set
            {
                _clubLogo = value;
                NotifyOfPropertyChange(() => ClubLogo);
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {

                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }

        }

        private string _street;

        public string Street 
        {
            get { return _street; }
            set
            {
                _street = value;
                NotifyOfPropertyChange( () => Street);
            }
        }

        private string _number;

        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NotifyOfPropertyChange(() => Number);
            }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                NotifyOfPropertyChange(() => City);
            }
        }

        private int _postcode;

        public int PostCode
        {
            get { return _postcode; }
            set
            {
                _postcode = value;
                NotifyOfPropertyChange(() => PostCode);
            }
        }


        public void ChangeClubLogo()
        {

            OpenFileDialog f = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.png) | *.jpg; *.png"
            };

            if (f.ShowDialog() == true)
            {

                Uri fileUri = new Uri(f.FileName);

                ClubLogo = fileUri.ToString();
            }
        }

        public void RegisterClub()
        {


            if (PassWord != PassWord2)
            {
                ErrorMessage = "De paswoorden zijn verschillend";
                PassWord = "";
                PassWord2 = "";
            }
            else
            {
                var check = _clubService.CheckSportClub(ClubName);
                if (check)
                {
                    ErrorMessage = "Deze SportClub heeft al een account";
                    return;
                }
                var address = new Address
                 {
                     AddressId = Guid.NewGuid(),
                     Street = _street,
                     Number = _number,
                     City = _city,
                     PostCode = _postcode
                 };

                var club = new Club
                {
                    SportClubId = Guid.NewGuid(),
                    Name = _clubName,
                    Password = _password,
                    ClubLogo = _clubLogo,
                    Address = address
                };


                _clubService.CreateSportClub(club);

                _events.PublishOnUIThread(new LoginEvent("U bent geregistreerd en kan inloggen", _clubName));
                

            }


        }

    }
}
