using System.Data.Entity;
using System.Windows;
using Caliburn.Micro;
using SportClub.Data.DataContext;
using SportClub.Data.ServiceContracts;

namespace Sportclub.UI.ViewModels
{
   public class LoginViewModel : Screen
   {

		private string _clubName;
        private string _password;
        private readonly IClubService _sportClubService;


        public LoginViewModel(IClubService sportClubService)
        {
           _sportClubService = sportClubService;
        }

		public string ClubName
		{
			get { return _clubName; }
            set
            {
                _clubName = value;
                NotifyOfPropertyChange(()=> ClubName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        
		}

		public string Password
		{
			get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(()=> Password);
                NotifyOfPropertyChange(()=> CanLogIn);
            }
		}

        //check for input before enable loginButton 
        public bool CanLogIn
        {
            get
            {
                bool output = false;

                if (ClubName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public void LogIn()
        {
            var sportClub =  _sportClubService.LoginSportClub(Password, ClubName);

          if (sportClub == null)
          {
              MessageBoxResult result =
                  MessageBox.Show("Account niet correct!","Bevesteging", MessageBoxButton.OK, MessageBoxImage.Question);
          } 

        }

	}
}
