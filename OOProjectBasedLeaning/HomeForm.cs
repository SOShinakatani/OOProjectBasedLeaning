namespace OOProjectBasedLeaning
{

    public partial class HomeForm : Form
    {

        public HomeForm()
        {

            InitializeComponent();

            var timeTracker = new TimeTrackerModel(NullCompany.Instance); // Company ‚Ì‘ã‚í‚è‚É‰¼‚Ì‚à‚Ì‚ðŽg‚¤
            var panel = new TimeTrackerPanel(timeTracker);

            this.Controls.Add(panel);

        }

    }

}
