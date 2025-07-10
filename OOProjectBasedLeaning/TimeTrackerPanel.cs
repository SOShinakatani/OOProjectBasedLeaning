using System;
using System.Drawing;
using System.Windows.Forms;
using OOProjectBasedLeaning;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    public partial class TimeTrackerPanel : Panel
    {
        private readonly TimeTrackerModel timeTracker;
        private readonly Company company;
        private ComboBox employeeComboBox;
        private ComboBox locationComboBox;
        private Button punchInButton;
        private Button punchOutButton;
        private Label lblStatus;

        public event EventHandler<string>? LogUpdated;

        public TimeTrackerPanel(TimeTrackerModel timeTracker, Company company)
        {
            this.timeTracker = timeTracker ?? throw new ArgumentNullException(nameof(timeTracker));
            this.company = company ?? throw new ArgumentNullException(nameof(company));
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(600, 500);

            employeeComboBox = new ComboBox
            {
                Location = new Point(10, 10),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DisplayMember = "Name"
            };
            employeeComboBox.SelectedIndexChanged += (s, e) => UpdateSelectedEmployeeStatus();
            this.Controls.Add(employeeComboBox);

            locationComboBox = new ComboBox
            {
                Location = new Point(220, 10),
                Size = new Size(120, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            locationComboBox.Items.AddRange(Enum.GetNames(typeof(WorkLocation)));
            locationComboBox.SelectedIndex = 0;
            this.Controls.Add(locationComboBox);

            punchInButton = new Button
            {
                Text = "出勤",
                Location = new Point(350, 10),
                Size = new Size(80, 30)
            };
            punchInButton.Click += PunchInButton_Click;
            this.Controls.Add(punchInButton);

            punchOutButton = new Button
            {
                Text = "退勤",
                Location = new Point(440, 10),
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
                    var location = GetSelectedLocation();
                    timeTracker.PunchIn(employee.Id, location);
                    
                    lblStatus.Text = $"{employee.Name} さんは現在、出勤中（{location}）";
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
                    var location = GetSelectedLocation();
                    timeTracker.PunchOut(employee.Id, location);
                    
                    lblStatus.Text = $"{employee.Name} さんは現在、退勤済み";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private WorkLocation GetSelectedLocation()
        {
            if (Enum.TryParse(locationComboBox.SelectedItem?.ToString(), out WorkLocation location))
            {
                return location;
            }
            return WorkLocation.Office;
        }

        public void RefreshEmployeeList()
        {
            employeeComboBox.Items.Clear();

            foreach (var emp in EmployeeRepository.GetAll())
            {
                employeeComboBox.Items.Add(emp);
            }

            if (employeeComboBox.Items.Count > 0)
            {
                employeeComboBox.SelectedIndex = 0;
            }
            else
            {
                lblStatus.Text = "状態: -";
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
            else
            {
                lblStatus.Text = "状態: -";
            }
        }
    }
}
