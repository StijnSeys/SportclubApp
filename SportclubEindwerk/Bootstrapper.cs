using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using SportclubEindwerk.ViewModel;

namespace SportclubEindwerk
{
  public class Bootstrapper : BootstrapperBase
    {

        public Bootstrapper()
        {
            Initialize();
            
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
            //Fill DB with dummy data
          //  tryoutDBClass dbClass = new tryoutDBClass();
          //  dbClass.CheckUpp();
        }
    }
}
