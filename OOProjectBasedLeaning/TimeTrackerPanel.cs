using System;
using System.Drawing;
using System.Windows.Forms;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    public partial class TimeTrackerPanel : Panel
    {
        private TimeTrackerModel timeTracker;
        private Company company;
        private ComboBox employeeComboBox;
        private Button punchInButton;
        private Button punchOutButton;

        public event EventHandler<string>? LogUpdated;

        public TimeTrackerPanel(TimeTrackerModel timeTracker, Company company)
        {
            this.timeTracker = timeTracker;
            this.company = company;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(400, 150);

            employeeComboBox = new ComboBox
            {
                Location = new Point(10, 10),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(employeeComboBox);

            punchInButton = new Button
            {
                Text = "出勤",
                Location = new Point(220, 10),
                Size = new Size(80, 30)
            };
            punchInButton.Click += PunchInButton_Click;
            this.Controls.Add(punchInButton);

            punchOutButton = new Button
            {
                Text = "退勤",
                Location = new Point(310, 10),
                Size = new Size(80, 30)
            };
            punchOutButton.Click += PunchOutButton_Click;
            this.Controls.Add(punchOutButton);
        }

        private void PunchInButton_Click(object sender, EventArgs e)
        {
            if (employeeComboBox.SelectedItem is Employee employee)
            {
                try
                {
                    timeTracker.PunchIn(employee.Id); // 修正点①
                    LogUpdated?.Invoke(this, $"{employee.Name} が出勤しました。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PunchOutButton_Click(object sender, EventArgs e)
        {
            if (employeeComboBox.SelectedItem is Employee employee)
            {
                try
                {
                    timeTracker.PunchOut(employee.Id); // 修正点②
                    LogUpdated?.Invoke(this, $"{employee.Name} が退勤しました。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void RefreshEmployeeList()
        {
            employeeComboBox.Items.Clear();
            employeeComboBox.DisplayMember = "Name"; // 表示名を設定

            foreach (var emp in EmployeeRepository.GetAll())
            {
                employeeComboBox.Items.Add(emp);
            }
        }

        public void UpdateStatus()
        {
            foreach (var emp in EmployeeRepository.GetAll())
            {
                bool isWorking = timeTracker.IsAtWork(emp.Id); // 修正点③
                string status = $"{emp.Name} は {(isWorking ? "勤務中" : "退勤済み")}です。";
                Console.WriteLine(status);
                LogUpdated?.Invoke(this, status);
            }
        }
    }
}
