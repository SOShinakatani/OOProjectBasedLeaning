using System;
using System.Drawing;
using System.Windows.Forms;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    public partial class HomeForm : Form
    {
        private TimeTrackerModel tracker;
        private TimeTrackerPanel panel;
        private ComboBox employeeComboBox;

        public HomeForm(TimeTrackerModel tracker)
        {
            this.tracker = tracker;
            InitializeComponent();

            var company = new CompanyModel("サンプル会社");
            company.AddTimeTracker(tracker);

            panel = new TimeTrackerPanel(tracker, company);
            this.Controls.Add(panel);

            employeeComboBox = new ComboBox
            {
                Location = new Point(10, 220),
                Size = new Size(200, 30)
            };
            Controls.Add(employeeComboBox);

            this.Text = "ホームフォーム";
        }

        public void SetLogHandler(EventHandler<string> handler)
        {
            panel.LogUpdated += handler;
        }

        public void RefreshEmployeeList()
        {
            employeeComboBox.Items.Clear();
            foreach (var emp in EmployeeRepository.GetAll())
            {
                employeeComboBox.Items.Add(emp);
            }
        }
    }
}
