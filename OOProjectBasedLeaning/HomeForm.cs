namespace OOProjectBasedLeaning
{

    public partial class HomeForm : Form
    {

        public HomeForm()
        {

            InitializeComponent();

            var timeTracker = new TimeTrackerModel(NullCompany.Instance); // Company の代わりに仮のものを使う
            var panel = new TimeTrackerPanel(timeTracker);

            this.Controls.Add(panel);

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }
    }

}
