using Caliburn.Micro;
using SportClub.UI.EventModels;


namespace SportClub.UI.ViewModels
{
    //Conductor caliburn micro 
    public class ShellViewModel : Conductor<object>, IHandle<CreateAccountEvent>, IHandle<MainScreenEvent>, IHandle<LoginEvent>, IHandle<MaterialEvent>, IHandle<MemberEvent>, IHandle<MailEvent>, IHandle<WordEvent>
    {
        private readonly MainScreenViewModel _mainScreenViewModel;
        private readonly MaterialViewModel _materialViewModel;
        private readonly LoginViewModel _loginViewModel;
        private readonly MemberViewModel _memberViewModel;
        private readonly MailViewModel _mailViewModel;
        private readonly WordViewModel _wordViewModel;


        public ShellViewModel(IEventAggregator events, MainScreenViewModel mainScreenViewModel, MaterialViewModel materialViewModel, LoginViewModel loginViewModel, MemberViewModel memberViewModel, MailViewModel mailViewModel, WordViewModel wordViewModel)
        {

            //Create instances to access the handlers inside the viewmodel
            _mainScreenViewModel = mainScreenViewModel;
            _materialViewModel = materialViewModel;
            _loginViewModel = loginViewModel;
            _memberViewModel = memberViewModel;
            _mailViewModel = mailViewModel;
            _wordViewModel = wordViewModel;


            //Adding and listening to events 
            events.Subscribe(this);


            //open ViewModel on startup
            ActivateItem(_loginViewModel);
        }


        public sealed override void ActivateItem(object item)
        {
            base.ActivateItem(item);
        }

        //when logout is pressed
        public void LoginScreen()
        {

            ActivateItem(_loginViewModel);
        }

        public void Handle(CreateAccountEvent message)

        {

            ActivateItem(IoC.Get<RegisterViewModel>());

        }
        public void Handle(LoginEvent message)
        {

            ActivateItem(_loginViewModel);

        }

        public void Handle(MainScreenEvent message)
        {

            ActivateItem(_mainScreenViewModel);
        }

        public void Handle(MaterialEvent message)
        {
            ActivateItem(_materialViewModel);
        }

        public void Handle(MemberEvent message)
        {
            ActivateItem(_memberViewModel);
        }

        public void Handle(MailEvent message)
        {
            ActivateItem(_mailViewModel);
        }

        public void Handle(WordEvent message)
        {
            ActivateItem(_wordViewModel);
        }
    }
}

