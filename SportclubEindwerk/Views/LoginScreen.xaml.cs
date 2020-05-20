using System.Windows;
using Sportclub.UI.ViewModels;

namespace Sportclub.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen(MemberViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
