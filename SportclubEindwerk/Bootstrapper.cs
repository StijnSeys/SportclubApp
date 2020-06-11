
using Caliburn.Micro;
using Sportclub.UI.Helpers;
using SportClub.Data.DataContext;
using SportClub.Data.ServiceContracts;
using SportClub.Data.Services;
using SportClub.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SportClub.Data.FillDataHelpers;

namespace SportClub.UI
{
    public class Bootstrapper : BootstrapperBase
    {

        //dependency injection from caliburn micro
        private readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();

            //  Add the passwordboxHelper Class
            ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxHelper.BoundPasswordProperty,
                "Password",
                "PasswordChanged");

        }

        protected override void Configure()
        {

            _container.Instance(_container);

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            //Methodes to fill the _container for DI
            RegisterDatabase();
            RegisterServices();
        }
        private void RegisterDatabase()
        {
            var dbContext = new SportClubDBContext();
            _container.RegisterInstance(typeof(SportClubDBContext), "DbContext", dbContext);
        }
        private void RegisterServices()
        {

            //find all ViewModel classes and put in _container for DI(not a good practice but possible to use in small projects )
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(viewModelType, null, viewModelType));


            //hard coded adding the services for DI
            _container.PerRequest<IClubService, ClubService>();
            _container.PerRequest<ISportService, SportService>();
            _container.PerRequest<IAddressService, AddressService>();
            _container.PerRequest<IMemberService, MemberService>();
            _container.PerRequest<IMaterialService, MaterialService>();

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {

            //load the ShellViewmodel on startup
            DisplayRootViewFor<ShellViewModel>();

            //  fill DB with Dummy content
            //var dbClass = new tryoutDBClass();
            // dbClass.DummyData();
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
