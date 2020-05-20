using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SportclubEindwerk.Model;

namespace SportclubEindwerk.ViewModel
{
    public class ShellViewModel : Screen
    {
        private string _firstName = "Stijn";
        private int _lastName;
        private BindableCollection<Member> _members = new BindableCollection<Member>();
        private Member _selectedMember;


        public ShellViewModel()
        {
            
        }
        public string FirstName
        {
            get
            {
                return _firstName;

            }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
            }

        }

        public int LastName
        {
            get
            {
                return _lastName; 

            }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);

            }
        }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        //mvvm list
        public BindableCollection<Member> Members
        {
            get { return _members; }
            set { _members = value; }
        }

        public Member SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                _selectedMember = value;
                NotifyOfPropertyChange(() => SelectedMember);
            }
        }
    }
}