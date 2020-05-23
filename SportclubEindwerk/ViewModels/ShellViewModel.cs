using Caliburn.Micro;
using Sportclub.UI.Models;
using SportClub.Data.ServiceContracts;

namespace Sportclub.UI.ViewModels
{
    //Conductor caliburn micro
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel(LoginViewModel loginViewModel)
        {
            ActivateItem(loginViewModel);
        }


    }
}















//TRYOUT CONTENT
//public string FirstName
//{
//    get
//    {
//        return _firstName;

//    }
//    set
//    {
//        _firstName = value;
//        NotifyOfPropertyChange(() => FirstName);
//        NotifyOfPropertyChange(() => FullName);
//    }

//}

//public int LastName
//{
//    get
//    {
//        return _lastName; 

//    }
//    set
//    {
//        _lastName = value;
//        NotifyOfPropertyChange(() => LastName);

//    }
//}

//public string FullName
//{
//    get { return $"{FirstName} {LastName}"; }
//}

////mvvm list
//public BindableCollection<Member> Members
//{
//    get { return _members; }
//    set { _members = value; }
//}

//public Member SelectedMember
//{
//    get { return _selectedMember; }
//    set
//    {
//        _selectedMember = value;
//        NotifyOfPropertyChange(() => SelectedMember);
//    }
//}
