using System;
using Caliburn.Micro;
using Microsoft.Win32;
using SportClub.Data.EntityModels;
using SportClub.UI.EventModels;
using SportClub.UI.Models;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace SportClub.UI.ViewModels
{
    public class MailViewModel : Screen, IHandle<MailEvent>
    {

        private readonly IEventAggregator _event;

        public MailViewModel(IEventAggregator eventAggregator)
        {

            _event = eventAggregator;

            _event.Subscribe(this);

        }

        private Club _club;



        //The list to for the listbox Members
        private IList<MailListModel> _memberMailList = new List<MailListModel>();

        public IList<MailListModel> MemberMailList
        {
            get { return _memberMailList; }
            set
            {
                _memberMailList = value;
                NotifyOfPropertyChange();
            }
        }

        ////List for the selectPerSprot
        //private IList<Sport> _clubSports = new List<Sport>();

        //public IList<Sport> ClubSports
        //{
        //    get { return _clubSports; }
        //    set
        //    {
        //        _clubSports = value;
        //        NotifyOfPropertyChange();
        //    }
        //}

        //List selected members for mailTo
        private IList<MailListModel> _selectedMembers;

        public IList<MailListModel> SelectedMembers
        {
            get { return _selectedMembers; }
            set
            {
                _selectedMembers = value;
                NotifyOfPropertyChange();
            }
        }

        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                NotifyOfPropertyChange();
            }
        }



        //the subject for the outlook Mail
        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => CanSendMail);
            }
        }

        private string _mailBody;

        public string MailBody
        {
            get { return _mailBody; }
            set
            {
                _mailBody = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => CanSendMail);
            }
        }

        private string _cc;

        public string Cc
        {
            get { return _cc; }
            set
            {
                _cc = value;
                NotifyOfPropertyChange();
            }
        }

        private string _bcc;

        public string Bcc
        {
            get { return _bcc; }
            set
            {
                _bcc = value;
                NotifyOfPropertyChange();
            }
        }


        private bool _allMembers = true;

        public bool AllMembers
        {
            get { return _allMembers; }
            set
            {
                _allMembers = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _sportMembers;

        public bool SportMembers
        {
            get { return _sportMembers; }
            set
            {
                _sportMembers = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _manualMembers;

        public bool ManualMembers
        {
            get { return _manualMembers; }
            set
            {
                _manualMembers = value;
                NotifyOfPropertyChange();
            }
        }

        public bool CanSendMail
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(MailBody))
                {
                    output = true;
                }

                return output;
            }
        }

        public IList<string> MailList()
        {
            IList<string> emails = new List<string>();

            if (ManualMembers)
            {
                foreach (var member in SelectedMembers)
                {
                    emails.Add(member.Member.Email);

                }

            }
            else
            {

                foreach (var member in MemberMailList)
                {
                    emails.Add(member.Member.Email);
                }

            }

            return emails;
        }

        public void BackButton()
        {

            _event.PublishOnUIThread(new MainScreenEvent(_club));
        }

        public void SendMail()
        {

            try
            {
                //list for email adresses
                IList<string> sendMailAdressList = MailList();

                var outlookApp = new Microsoft.Office.Interop.Outlook.Application();
                _MailItem oMailItem = outlookApp.CreateItem(OlItemType.olMailItem);


                // Recipient
                Recipients oRecips = oMailItem.Recipients;
                foreach (string recipient in MailList())
                {

                    Recipient oRecip = oRecips.Add(recipient);
                    oRecip.Resolve();
                }

                //Add CC 
                //Add Subject ,body, bcc etc...
                if (!string.IsNullOrEmpty(Cc))
                {

                    try
                    {
                        var eMailValidator = new System.Net.Mail.MailAddress(Cc);
                        oMailItem.CC = Cc;
                    }
                    catch (FormatException ex)
                    {
                        MessageBoxResult except = MessageBox.Show("Cc is niet correct");
                        return;
                    }

                }
             
                if (!string.IsNullOrEmpty(Bcc))
                {

                    try
                    {
                        var eMailValidator = new System.Net.Mail.MailAddress(Bcc);
                        oMailItem.BCC = Bcc;
                    }
                    catch (FormatException ex)
                    {
                        MessageBoxResult except = MessageBox.Show("BCC is niet correct");
                        return;
                    }

                }

                oMailItem.Subject = Subject;
                oMailItem.Body = MailBody;


                if (!string.IsNullOrEmpty(FilePath))
                {
                    // need to check to see if file exists before we attach !
                    if (!File.Exists(FilePath))
                        MessageBox.Show("Kan het Word document niet laden ");
                    else
                    {
                        Attachment attachment = oMailItem.Attachments.Add(FilePath,OlAttachmentType.olByValue, 1 , FilePath);
                    }
                }

                //Display the mailbox
                oMailItem.Display(true);

            }
            catch (Exception objEx)
            {
                MessageBoxResult except = MessageBox.Show("Mail kan niet aangemaakt worden. Controleer de gegevens.");
            }

            NotifyOfPropertyChange(()=> CanSendMail);

        }

        public void WordDocument()
        {
            string sMessageBoxText = "Heb je al een word document?";
            string sCaption = "Word";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:

                    var openFileDialog = new OpenFileDialog
                    {
                        Filter = "Word Documents|*.docx"
                    };

                
                    if (openFileDialog.ShowDialog() == true)
                    {

                        FilePath = System.IO.Path.GetFullPath(openFileDialog.FileName);
                    }


                    break;

                case MessageBoxResult.No:

                    _event.PublishOnUIThread(new WordEvent(_club));
                    break;

            }


        }



        
        //private Sport _selectedSport;

        //public Sport SelectedSport
        //{
        //    get { return _selectedSport; }
        //    set
        //    {
        //        _selectedSport = value;
                
        //        NotifyOfPropertyChange();
               
        //    }
        //}


        //public void LoadSportMembers()
        //{
        //    if (SelectedSport == null) return;
        //    MemberMailList.Clear();

        //    foreach (var member in _club.Members)
        //    {
        //        var sports = member.Sports;

        //        foreach (var sport in sports)
        //        {
        //            if (sport != SelectedSport) continue;
        //            var model = new MailListModel
        //            {
        //                Member = member
        //            };

        //            MemberMailList.Add(model);
        //        }
        //    }


        //}

        public void LoadAllMembers()
        {
            MemberMailList.Clear();
            if (_club.Members.Count == 0) return;

            foreach (var member in _club.Members)
            {
                MailListModel model = new MailListModel()
                {
                    Member = member
                };

                MemberMailList.Add(model);
            }

        }
     

        public void Handle(MailEvent message)
        {

            MemberMailList.Clear();

            _club = message.Club;

            LoadAllMembers();

            if (message.FilePath != null)
            {

                FilePath = message.FilePath;

            }

            //foreach (var sport in message.Club.Sports)
            //{
            //    ClubSports.Add(sport);
            //}

            // LoginLogo = message.Club.ClubLogo;
            // LoggedInClub = message.Club.Name;


        }
    }
}
