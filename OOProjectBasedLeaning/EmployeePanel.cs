using System;
using System.Drawing;
using System.Windows.Forms;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    public class EmployeePanel : Panel, Observer, EmployeePanel.Editable
    {
        private Employee employee;
        private EditMode editMode = EditMode.On;
        private TextBox employeeNameTextBox;

        public EmployeePanel(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
        }

        public interface Editable
        {
            void ChangeDisplayMode();
            void ChangeEditMode();
        }

        private void InitializeComponent()
        {
            Label employeeNameLabel = new Label
            {
                Text = "Name:",
                AutoSize = true,
                Location = new Point(20, 10)
            };

            employeeNameTextBox = new TextBox
            {
                Text = employee.Name,
                Location = new Point(140, 6),
                Width = 160
            };

            Controls.Add(employeeNameLabel);
            Controls.Add(employeeNameTextBox);
        }

        public void ChangeEditMode()
        {
            editMode = EditMode.On;
            employeeNameTextBox.Enabled = true;
        }

        public void ChangeDisplayMode()
        {
            editMode = EditMode.Off;
            employeeNameTextBox.Enabled = false;
        }

        public bool IsEditMode()
        {
            return editMode == EditMode.On;
        }

        public void Update(object sender)
        {
            if (sender is Employee updatedEmployee)
            {
                employee = updatedEmployee;
                employeeNameTextBox.Text = employee.Name;
            }
        }

        private enum EditMode
        {
            On,
            Off
        }
    }

    public interface Observer
    {
        void Update(object sender);
    }
}
