﻿using System.Data.Entity;
using System.Windows;
using Caliburn.Micro;
using SportClub.Data.DataContext;
using SportClub.Data.ServiceContracts;
using Sportclub.UI.EventModels;
using SportClub.UI.EventModels;

namespace SportClub.UI.ViewModels
{
   public class LoginViewModel : Screen , IHandle<LoginEvent> 
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
                ErrorMessage = "";
                NotifyOfPropertyChange(()=> Password);
                NotifyOfPropertyChange(()=> CanLogIn);
            }
		}

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
               
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }

        }
        public string OkMessage
        {
            get { return _okMessage; }
            set
            {

                _okMessage = value;
                NotifyOfPropertyChange(() => OkMessage);
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
              //check if sportclub exists show appropriate message
              var exist =  _sportClubService.CheckSportClub(ClubName);
            if (exist)
            {
                ErrorMessage = "Password Incorrect";
                return;
            }

            ErrorMessage = "SportClub is not registered";
            
          }
          else
          {
              //navigate to MainWindowViewmodel
              _events.PublishOnUIThread(new MainScreenEvent(sportClub));
          }
          
        }

        public void CreateAccount()
        {

            //navigate to registerVieModel
            _events.PublishOnUIThread(new CreateAccountEvent());
        }
         
        public void Handle(LoginEvent message)
        {

            OkMessage = message.Text;
            ClubName = message.ClubName;

        }
   }
}
