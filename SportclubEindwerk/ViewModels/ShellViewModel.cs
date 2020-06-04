using Caliburn.Micro;
using Sportclub.UI.EventModels;
using SportClub.UI.EventModels;

namespace SportClub.UI.ViewModels
{
    //Conductor caliburn micro 
    public class ShellViewModel : Conductor<object>, IHandle<CreateAccountEvent>, IHandle<MainScreenEvent>, IHandle<LoginEvent>,IHandle<MaterialEvent>
    {
        private readonly MainScreenViewModel _mainScreenViewModel;
        private readonly MaterialViewModel _materialViewModel;

        
        public ShellViewModel(IEventAggregator events, MainScreenViewModel mainScreenViewModel, MaterialViewModel materialViewModel)
        {

            //Create instances to access the handelrs inside the viewmodel
            _mainScreenViewModel = mainScreenViewModel;
            _materialViewModel = materialViewModel;

            //Adding and listening to events 
            events.Subscribe(this);


            //open ViewModel on startup
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public sealed override void ActivateItem(object item)
        {
            base.ActivateItem(item);
        }


        public void Handle(CreateAccountEvent message)

        {

            ActivateItem(IoC.Get<RegisterViewModel>());

        }
        public void Handle(LoginEvent message)
        {
            
            ActivateItem(IoC.Get<LoginViewModel>());

        }

        public void Handle(MainScreenEvent message)
        {
            
            ActivateItem(_mainScreenViewModel);
        }

        public void Handle(MaterialEvent message)
        {
           ActivateItem(_materialViewModel);
        }
    }
}

