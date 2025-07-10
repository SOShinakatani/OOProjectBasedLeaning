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
        private Label lblStatus;

        public event EventHandler<string>? LogUpdated;

        public TimeTrackerPanel(TimeTrackerModel timeTracker, Company company)
        {
            this.timeTracker = timeTracker;
            this.company = company;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // パネルサイズを適当に設定
            this.Size = new Size(600, 500);

            // プルダウンリスト（社員選択）
            employeeComboBox = new ComboBox
            {
                Location = new Point(10, 10),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(employeeComboBox);

            // 出勤ボタン
            punchInButton = new Button
            {
                Text = "出勤",
                Location = new Point(220, 10),
                Size = new Size(80, 30)
            };
            punchInButton.Click += PunchInButton_Click;
            this.Controls.Add(punchInButton);

            // 退勤ボタン
            punchOutButton = new Button
            {
                Text = "退勤",
                Location = new Point(310, 10),
                Size = new Size(80, 30)
            };
            punchOutButton.Click += PunchOutButton_Click;
            this.Controls.Add(punchOutButton);

            lblStatus = new Label
            {
                Text = "状態: -",
                Location = new Point(10, 50),
                AutoSize = true
            };
            this.Controls.Add(lblStatus);

        }

        private void PunchInButton_Click(object sender, EventArgs e)
        {
            if (employeeComboBox.SelectedItem is Employee employee)
            {
                try
                {
                    timeTracker.PunchIn(employee.Id);
                    LogUpdated?.Invoke(this, $"{employee.Name} が出勤しました。");

                    lblStatus.Text = $"{employee.Name} さんは現在、出勤中";
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
                    timeTracker.PunchOut(employee.Id);
                    LogUpdated?.Invoke(this, $"{employee.Name} が退勤しました。");

                    lblStatus.Text = $"{employee.Name} さんは現在、退勤済み";
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
            employeeComboBox.DisplayMember = "Name";

            foreach (var emp in EmployeeRepository.GetAll())
            {
                employeeComboBox.Items.Add(emp);
            }
        }

        public void UpdateStatus()
        {
            foreach (var emp in EmployeeRepository.GetAll())
            {
                bool isWorking = timeTracker.IsAtWork(emp.Id);
                string status = $"{emp.Name} は {(isWorking ? "勤務中" : "退勤済み")}です。";
                Console.WriteLine(status);
                LogUpdated?.Invoke(this, status);
            }
        }

        private void UpdateSelectedEmployeeStatus()
        {
            if (employeeComboBox.SelectedItem is Employee emp)
            {
                bool isWorking = timeTracker.IsAtWork(emp.Id);
                lblStatus.Text = $"{emp.Name} さんは現在、" + (isWorking ? "出勤中" : "退勤済み");
            }
        }

    }
}
