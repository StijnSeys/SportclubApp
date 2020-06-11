using Caliburn.Micro;
using Microsoft.Win32;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;
using SportClub.UI.EventModels;
using SportClub.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace SportClub.UI.ViewModels
{
    public class MemberViewModel : Screen, IHandle<MemberEvent>
    {

        private readonly IEventAggregator _event;
        private readonly IMemberService _memberService;
        private readonly ISportService _sportService;
        private Club _club;

        public MemberViewModel(IEventAggregator eventAggregator, IMemberService memberService, ISportService sportService)
        {
            _event = eventAggregator;
            _memberService = memberService;
            _sportService = sportService;

            eventAggregator.Subscribe(this);
        }

        //can only bind 1 class property to show in listView
        //use a memberListModel to show the first and last name in the listView 
        private List<MemberListModel> _memberList = new List<MemberListModel>();
        public List<MemberListModel> MemberList
        {
            get { return _memberList; }
            set
            {
                _memberList = value;
                NotifyOfPropertyChange();

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
        public MemberListModel SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                _selectedMember = value;

                if (SelectedMember != null)
                {
                    FirstName = _selectedMember.Member.FirstName;
                    LastName = _selectedMember.Member.LastName;
                    Email = _selectedMember.Member.Email;
                    Street = _selectedMember.Member.Address.Street;
                    Number = _selectedMember.Member.Address.Number;
                    City = _selectedMember.Member.Address.City;
                    Postcode = _selectedMember.Member.Address.PostCode;
                }


                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => CanEditMember);
                NotifyOfPropertyChange(() => CanDeleteMember);
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

        public bool CanEditMember
        {
            get
            {
                bool output = false;

                if (SelectedMember != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void EditMember()
        {

            Member selectedMember = SelectedMember.Member;

            selectedMember.Email = Email;
            selectedMember.FirstName = FirstName;
            selectedMember.LastName = LastName;

            selectedMember.Address.Street = Street;
            selectedMember.Address.City = City;
            selectedMember.Address.Number = Number;
            selectedMember.Address.PostCode = Postcode;

            MemberListModel changedMember = new MemberListModel
            {
                Member = selectedMember
            };

            //change member in de existing list so the changes are visible for the user
            MemberList[MemberList.FindIndex(m => m.Member.MemberId == selectedMember.MemberId)] = changedMember;

            //make the change to the DB
            _memberService.UpdateMember(selectedMember);

            ClearFields();
        }

        public bool CanDeleteMember
        {
            get
            {
                bool output = false;

                if (SelectedMember != null)
                {
                    output = true;
                }

                return output;
            }
        }


        //remove selected member
        public void DeleteMember()
        {

            string sMessageBoxText = "Wilt u " + SelectedMember.Member.FirstName + " verwijderen?";
            string sCaption = "Verwijderen";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:

                    Member selectedMember = SelectedMember.Member;

                    //remove member in de existing list so the changes are visible for the user
                    foreach (var member in MemberList)
                    {
                        if (member.Member == selectedMember)
                        {
                            _club.Members.Remove(member.Member);
                        }
                    }

                    //make the change to the DB
                    _memberService.DeleteMember(selectedMember);

                    ClearFields();

                    break;

                case MessageBoxResult.No:

                    return;

            }

        }

        public void ClearFields()
        {
            City = null;
            Street = null;
            Email = null;
            FirstName = null;
            LastName = null;
            Postcode = 0;
            Number = null;
            SelectedMember = null;
            MemberList = new List<MemberListModel>();
            foreach (var member in _club.Members)
            {
                var model = new MemberListModel
                {
                    Member = member
                };

                MemberList.Add(model);
            }

        }

        public void BackButton()
        {

            _event.PublishOnUIThread(new MainScreenEvent(_club));
            ClearFields();

        }



        //Get the members info out of the excel
        public void UpdateMemberList()
        {

            OpenFileDialog excelFile = new OpenFileDialog
            {
                Filter = "Excel Worksheets|*.xlsx"
            };

            if (excelFile.ShowDialog() == true)
            {

                Excel.Application memberExcel = new Excel.Application();

                Excel.Workbook workbook = memberExcel.Workbooks.Open(excelFile.FileName);

                Excel.Worksheet worksheet = workbook.Sheets[1];

                Excel.Range range = worksheet.UsedRange;

                IList<MemberListModel> excelMembers = new List<MemberListModel>();

                //start at 2 because excel starts counting at 1 and first line is not for data
                //loop over the excel list en get the members
                for (var i = 2; i < range.Rows.Count; i++)
                {
                    MemberListModel excelMember = new MemberListModel
                    {
                        Member = new Member
                        {
                            Address = new Address(),
                            Sports = new List<Sport>(),

                            FirstName = range.Cells[i, 1].Value.ToString(),
                            LastName = range.Cells[i, 2].Value.ToString(),
                            Email = range.Cells[i, 3].Value.ToString()
                        }
                    };

                    excelMember.Member.Address.Street = range.Cells[i, 4].Value.ToString();
                    excelMember.Member.Address.Number = range.Cells[i, 5].Value.ToString();
                    excelMember.Member.Address.City = range.Cells[i, 6].Value.ToString();
                    excelMember.Member.Address.PostCode = (int)range.Cells[i, 7].Value;


                    string name = range.Cells[i, 8].Value.ToString();

                    var sport = _sportService.GetSportByName(name);

                    excelMember.Member.Sports.Add(sport);

                    if (range.Cells[i, 9].Value != null)
                    {
                        name = range.Cells[i, 9].Value.ToString();

                        sport = _sportService.GetSportByName(name);

                        excelMember.Member.Sports.Add(sport);
                    }

                    excelMembers.Add(excelMember);
                }

                MemberList = excelMembers.ToList();

                //Db Task
                //first own implementation of threads can and need to run in the background
                Task update = new Task(UpdateMemberDb);
                update.Start();

                // make sure that excel is properly closed 
                workbook.Close(true, null, null);
                memberExcel.Quit();

                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(memberExcel);
            }

        }

        //method for task to add de new memberList to the database and make sure to have no duplicate data in database
        private void UpdateMemberDb()
        {

            var newMembers = MemberList.Select(member => member.Member).ToList();

            var oldClubMembers = _club.Members.ToList();

            //get the members to add to the database => dont add duplicate members
            var updateList = newMembers.Where(m => oldClubMembers.All(mem => mem.Email != m.Email)).ToList();


            //get the members to delete the sportClub or from the database
            var removeList = oldClubMembers.Where(m => newMembers.All(mem => mem.Email != m.Email)).ToList();


            //database calls
            //create or update new members
            foreach (var member in updateList)
            {

                var memberExist = _memberService.GetMember(member.MemberId);
                if (memberExist != null)
                {

                    memberExist.SportClubs.Add(_club);

                    _memberService.UpdateMember(memberExist);

                }
                else
                {
                    member.Address.AddressId = Guid.NewGuid();
                    member.MemberId = Guid.NewGuid();
                    member.SportClubs = new List<Club>();
                    member.SportClubs.Add(_club);
                    _memberService.CreateMember(member);
                }

            }

            //delete or update old members
            foreach (var member in removeList)
            {
                if (member.SportClubs.Count > 1)
                {
                    member.SportClubs.Remove(_club);
                    _memberService.UpdateMember(member);
                }
                else
                {
                    _memberService.DeleteMember(member);
                }

            }


            SelectedMember = null;
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
