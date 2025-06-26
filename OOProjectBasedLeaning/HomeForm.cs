using OOProjectBasedLeaning;
namespace OOProjectBasedLeaning
{

    public partial class HomeForm : Form
    {
        private TimeTrackerModel tracker;
        private TimeTrackerPanel panel;

        public HomeForm(TimeTrackerModel tracker)
        {

            InitializeComponent();

            this.tracker = tracker;

            var company = new CompanyModel("ƒTƒ“ƒvƒ‹‰ïŽÐ");
            company.AddTimeTracker(tracker);

            panel = new TimeTrackerPanel(tracker, company);
            this.Controls.Add(panel);
        }

        public void SetLogHandler(EventHandler<string> handler)
        {
            panel.LogUpdated += handler;
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }
    }

}
