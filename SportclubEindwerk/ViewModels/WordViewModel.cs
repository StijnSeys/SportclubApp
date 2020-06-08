using Caliburn.Micro;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using SportClub.Data.EntityModels;
using SportClub.UI.EventModels;
using Shape = Microsoft.Office.Interop.Word.Shape;

namespace SportClub.UI.ViewModels
{
    public class WordViewModel : Screen , IHandle<WordEvent>
    {

        private readonly IEventAggregator _event;
        private Club _club;
        private object _filePath;


        public WordViewModel(IEventAggregator eventAggregator)
        {
            _event = eventAggregator;
            eventAggregator.Subscribe(this);
        }


        private string _clubLogo ;

        public string ClubLogo
        {
            get { return _clubLogo; }
            set
            {
                _clubLogo = value;
                NotifyOfPropertyChange();
            }
        }

        private string _wordTitle;

        public string WordTitle
        {
            get { return _wordTitle; }
            set
            {
                _wordTitle = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(()=>CanCreateWord);
            }
        }

        private string _wordText;

        public string WordText
        {
            get { return _wordText; }
            set
            {
                _wordText = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(()=>CanCreateWord);
            }
        }

        private string _wordFooter;

        public string WordFooter
        {
            get { return _wordFooter; }
            set
            {
                _wordFooter = value;
                NotifyOfPropertyChange();
            }
        }


        private bool _yesLogo = true;

        public bool YesLogo
        {
            get { return _yesLogo; }
            set
            {
             
                _yesLogo = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _noLogo;

        public bool NoLogo
        {
            get { return _noLogo; }
            set
            {

                _noLogo = value;
                NotifyOfPropertyChange();
            }
        }
         public bool CanCreateWord
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrEmpty(WordText) && !string.IsNullOrEmpty(WordTitle))
                {
                    output = true;
                }

                return output;
            }
        }


        public void CreateWord()
        {

                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a new document
                _Document document = winword.Documents.Add();

                //Add header into the document
                foreach (Section section in document.Sections)
                {
                    //Get the header range and add the header details.
                    Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = WdColorIndex.wdBlue;
                    headerRange.Font.Size = 18;
                    headerRange.Text = WordTitle;
                }

                if (!string.IsNullOrEmpty(WordFooter))
                {

                    //Add the footers into the document
                    foreach (Section wordSection in document.Sections)
                    {
                        //Get the footer range and add the footer details.
                        Range footerRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        footerRange.Font.ColorIndex = WdColorIndex.wdDarkRed;
                        footerRange.Font.Size = 10;
                        footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        footerRange.Text = WordFooter;
                    }

                }
                //Add the Logo to the WordFile
                if (YesLogo)
                {

                    object startOfDoc = "\\startofdoc";

                    // Get a Range at the start of the document.
                    Range start = document.Bookmarks.get_Item(ref startOfDoc).Range;

                    // Add the picture to the Range's InlineShapes.

                    InlineShape inlineShape = start.InlineShapes.AddPicture(ClubLogo);

                    // Format the picture.
                    Shape shape = inlineShape.ConvertToShape();

                    // Wrap text around the picture's square.
                    shape.WrapFormat.Type = WdWrapType.wdWrapSquare;

                    // Align the picture on the upper right.
                    shape.Left = (float)WdShapePosition.wdShapeRight;
                    shape.Top = (float)WdShapePosition.wdShapeTop;
                    shape.Width = 100;
                    shape.Height = 100;
                }
  
                //adding text to document
                  document.Content.SetRange(0, 0);
                document.Content.Text = WordText;

              


                //Save the document
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Word document|*.docx";
                saveFileDialog1.Title = "Sla het document op";
                if (saveFileDialog1.ShowDialog() == true)
                {
                    string docName = saveFileDialog1.FileName;
                    if (docName.Length > 0)
                    {
                        _filePath = docName;
                        document.SaveAs(ref _filePath);
                       
                    }
                }

                document.Close();
                winword.Quit();
               
            
            _event.PublishOnUIThread(new MailEvent(_club, _filePath.ToString()));

            ClearForm();

        }


        public void BackButton()
        {

            ClearForm();
            _event.PublishOnUIThread(new MailEvent(_club));

        }

        
        private void ClearForm()
        {
            WordFooter = "";
            WordText = "";
            WordTitle = "";
            YesLogo = true;
        }

        public void Handle(WordEvent message)
        {

            ClubLogo = message.Club.ClubLogo;
            _club = message.Club;
        }

    }
}
