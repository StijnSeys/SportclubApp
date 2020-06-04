using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SportClub.Data.EntityModels;
using SportClub.Data.Services;
using SportClub.UI.EventModels;
using SportClub.UI.Models;

namespace SportClub.UI.ViewModels
{
  public class MemberViewModel : Screen, IHandle<MemberEvent>
    {

        private readonly IEventAggregator _event;
        private readonly MemberService _memberService;
        private Club _club;

        public MemberViewModel(IEventAggregator eventAggregator, MemberService memberService)
        {
            _event = eventAggregator;
            _memberService = memberService;

            eventAggregator.Subscribe(this);
        }



        private List<MemberListModel> _memberList = new List<MemberListModel>();
        public List<MemberListModel> MemberList
        {
            get { return _memberList; }
            set
            {
                _memberList = value;
                NotifyOfPropertyChange(() => MemberList);
            }
        }


        private IList<Sport> _selectedSports;
        public IList<Sport> SelectedSports
        {
            get
            {
                return _selectedSports;
            }
            set
            {
                _selectedSports = value;
                NotifyOfPropertyChange();
            }
        }


        private MemberListModel _selectedMember;
        public MemberListModel  SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                _selectedMember = value;

                FirstName = _selectedMember.Member.FirstName;
                LastName = _selectedMember.Member.LastName;
                Email = _selectedMember.Member.Email;
                Street = _selectedMember.Member.Address.Street;
                Number = _selectedMember.Member.Address.Number;
                City = _selectedMember.Member.Address.City;
                Postcode = _selectedMember.Member.Address.PostCode;
                NotifyOfPropertyChange();

            }
        }


        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange();

            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange();

            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange();
            }
        }

        private string _street;

        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                NotifyOfPropertyChange();
            }
        }

        private string _number;

        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NotifyOfPropertyChange();
            }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                NotifyOfPropertyChange();
            }
        }

        private int _postcode;

        public int Postcode
        {
            get { return _postcode; }
            set
            {
                _postcode = value;
                NotifyOfPropertyChange();
            }
        }

        public void Handle(MemberEvent message)
        {
            MemberList.Clear();
            if (message.Club.Members.Count != 0)
            {
                foreach (var member in message.Club.Members)
                {
                    MemberListModel model = new MemberListModel
                    {
                        Member = member
                    };

                    MemberList.Add(model);
                }
                
            }
            _club = message.Club;

        }
    }
}
