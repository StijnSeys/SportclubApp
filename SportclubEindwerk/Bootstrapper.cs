using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Sportclub.UI.ViewModels;
using SportClubData.Data;

namespace Sportclub.UI
{
  public class Bootstrapper : BootstrapperBase
    {
        //dependency injection from caliburn micro
        private readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
            
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {

            RegisterDatabase();
            RegisterServices();

            //load the Shellviewmodel on startup
            DisplayRootViewFor<ShellViewModel>();

            //to fill DB with Dummy content
           // var dbClass = new tryoutDBClass();
          //  dbClass.CheckUpp();
        }

        //Methodes to fill the _container for DE
        private void RegisterDatabase()
        {
            var dbContext = new DbContext("name=SportClubEntities");
            _container.RegisterInstance(typeof(DbContext), "DbContext", dbContext);
        }


        private void RegisterServices()
        {

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            //find all ViewModel classes and put in _container (not good
            //practice but possible to use in small projects )
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(viewModelType,null,viewModelType));


            //hard coded adding the services for DE
            _container.RegisterPerRequest(typeof(ISportClubService), null, typeof(SportClubService));
            _container.RegisterPerRequest(typeof(ISportService), null, typeof(SportService));
            _container.RegisterPerRequest(typeof(IAddresService), null, typeof(AddressService));
            _container.RegisterPerRequest(typeof(IMaterialService),null, typeof(MaterialService));
            _container.RegisterPerRequest(typeof(IMemberService), null, typeof(MemberService));
        }


        //Configure the container --> Copy/Paste  this is always the same
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
