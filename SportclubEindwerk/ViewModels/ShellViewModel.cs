using Caliburn.Micro;
using Sportclub.UI.EventModels;
using SportClub.UI.EventModels;

namespace SportClub.UI.ViewModels
{
    //Conductor caliburn micro 
    public class ShellViewModel : Conductor<object>, IHandle<CreateAccountEvent>, IHandle<MainScreenEvent>, IHandle<LoginEvent>
    {
        private readonly SimpleContainer _container;

        
        public ShellViewModel(IEventAggregator events, SimpleContainer container)
        {
          
            
            _container = container;

            //Adding and listening to events 
            events.Subscribe(this);

            //Create instance of viewModels to trigger the handler inside the viewModels for input(Good practice ??? )
            _container.GetInstance<MainScreenViewModel>();


            //open ViewModel on startup
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public sealed override void ActivateItem(object item)
        {
            base.ActivateItem(item);
        }


        public void Handle(CreateAccountEvent message)

        {

            ActivateItem(_container.GetInstance<RegisterViewModel>());

        }
        public void Handle(LoginEvent message)
        {
            
            ActivateItem(_container.GetInstance<LoginViewModel>());

        }

        public void Handle(MainScreenEvent message)
        {
            
            ActivateItem(_container.GetInstance<MainScreenViewModel>());
        }
    }
}

