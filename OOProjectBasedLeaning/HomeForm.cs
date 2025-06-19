namespace OOProjectBasedLeaning
{

    public partial class HomeForm : Form
    {

        public HomeForm()
        {

            InitializeComponent();

            var timeTracker = new TimeTrackerModel(NullCompany.Instance); // Company �̑���ɉ��̂��̂��g��
            var panel = new TimeTrackerPanel(timeTracker);

            this.Controls.Add(panel);

        }

    }

}
