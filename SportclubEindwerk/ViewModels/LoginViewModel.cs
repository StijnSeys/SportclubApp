using Caliburn.Micro;
using SportClub.Data.ServiceContracts;
using SportClub.UI.EventModels;

namespace SportClub.UI.ViewModels
{
    public class LoginViewModel : Screen, IHandle<LoginEvent>
    {

        private string _clubName;
        private string _password;
        private string _errorMessage;
        private string _okMessage;

        private readonly IClubService _sportClubService;
        private readonly IEventAggregator _events;


        public LoginViewModel(IClubService clubService, IEventAggregator events)
        {
            _sportClubService = clubService;

            _events = events;

            _events.Subscribe(this);


        }

        public string ClubName
        {
            get { return _clubName; }
            set
            {
                _clubName = value;
                ErrorMessage = "";
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => CanLogIn);
            }

        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                ErrorMessage = "";
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {

                _errorMessage = value;
                NotifyOfPropertyChange();
            }

        }
        public string OkMessage
        {
            get { return _okMessage; }
            set
            {

                _okMessage = value;
                NotifyOfPropertyChange();
            }

        }

        //check for input before enable loginButton 
        public bool CanLogIn
        {
            get
            {
                var output = ClubName?.Length > 0 && Password?.Length > 0;

                return output;
            }
        }

        public void LogIn()
        {
            var sportClub = _sportClubService.LoginSportClub(Password, ClubName);

            if (sportClub == null)
            {
                //check if sportClub exists show appropriate message
                var exist = _sportClubService.CheckSportClub(ClubName);
                if (exist)
                {
                    ErrorMessage = "Verkeerd paswoord";
                    return;
                }

                ErrorMessage = " Deze sportclub heeft nog geen account";
            }
            else
            {
                //navigate to MainWindowViewmodel
                _events.PublishOnUIThread(new MainScreenEvent(sportClub));

                //clear the Textboxes for logout

                ClubName = "";
                Password = null;


            }

        }

        public void CreateAccount()
        {

            //navigate to registerVieModel
            _events.PublishOnUIThread(new CreateAccountEvent());

        }

        public void Handle(LoginEvent message)
        {
            //need to set passwordbox to null or will not be checked for changes on return to the viewmodel=> long search
            Password = null;

            OkMessage = message.Text;
            ClubName = message.ClubName;


        }
    }
}
