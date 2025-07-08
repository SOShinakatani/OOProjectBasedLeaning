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

            company = new CompanyModel("�T���v�����");
            company.AddTimeTracker(tracker);

            panel = new TimeTrackerPanel(tracker, company)
            {
                Location = new Point(10, 10)
            };
            this.Controls.Add(panel);

            this.Text = "�z�[���t�H�[��";

            // �����\���̏]�ƈ����X�g��ǂݍ���
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
