using Caliburn.Micro;
using Sportclub.UI.EventModels;
using SportClub.UI.EventModels;

namespace SportClub.UI.ViewModels
{
    //Conductor caliburn micro 
    public class ShellViewModel : Conductor<object>, IHandle<CreateAccountEvent>, IHandle<MainScreenEvent>, IHandle<LoginEvent>,IHandle<MaterialEvent>,IHandle<MemberEvent>
    {
        private readonly MainScreenViewModel _mainScreenViewModel;
        private readonly MaterialViewModel _materialViewModel;
        private readonly LoginViewModel _loginViewModel;
        private readonly MemberViewModel _memberViewModel;
      
        
        public ShellViewModel(IEventAggregator events, MainScreenViewModel mainScreenViewModel, MaterialViewModel materialViewModel , LoginViewModel loginViewModel, MemberViewModel memberViewModel)
        {
           
            //Create instances to access the handlers inside the viewmodel
            _mainScreenViewModel = mainScreenViewModel;
            _materialViewModel = materialViewModel;
            _loginViewModel = loginViewModel;
            _memberViewModel = memberViewModel;

            //Adding and listening to events 
            events.Subscribe(this);


            //open ViewModel on startup
            ActivateItem(_loginViewModel);
        }

        public sealed override void ActivateItem(object item)
        {
            base.ActivateItem(item);
        }

        public void LoginScreen()
        {
            
            ActivateItem(IoC.Get<LoginViewModel>());
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
    }
}

