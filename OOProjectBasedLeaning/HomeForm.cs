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
        private Company company;

        public HomeForm(TimeTrackerModel tracker)
        {
            this.tracker = tracker;
            InitializeComponent();

            company = new CompanyModel("サンプル会社");
            company.AddTimeTracker(tracker);

            panel = new TimeTrackerPanel(tracker, company)
            {
                Location = new Point(10, 10)
            };
            this.Controls.Add(panel);

            this.Text = "ホームフォーム";

            // 初期表示の従業員リストを読み込み
            RefreshEmployeeList();
        }

        public void SetLogHandler(EventHandler<string> handler)
        {
            panel.LogUpdated += handler;
        }

        public void RefreshEmployeeList()
        {
            panel.RefreshEmployeeList();
        }
    }
}
