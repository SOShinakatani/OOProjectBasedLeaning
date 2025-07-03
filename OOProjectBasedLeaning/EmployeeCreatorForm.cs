using System;
using System.Drawing;
using System.Windows.Forms;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    public partial class EmployeeCreatorForm : Form
    {
        private int employeeId = 10000;
        private HomeForm homeForm;

        public EmployeeCreatorForm(HomeForm homeForm)
        {
            this.homeForm = homeForm;
            InitializeComponent(); // UI 初期化
        }

        private void EmployeeCreatorForm_Load(object sender, EventArgs e)
        {
            this.Text = "従業員作成フォーム";
        }

        private void CreateGuestEvent(object sender, EventArgs e)
        {
            try
            {
                string employeeName = nameTextBox.Text.Trim();
                if (string.IsNullOrEmpty(employeeName))
                {
                    MessageBox.Show("従業員名を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newEmployee = new EmployeeModel(employeeId++, employeeName);
                EmployeeRepository.Add(newEmployee);

                homeForm?.RefreshEmployeeList();

                MessageBox.Show($"従業員「{newEmployee.Name}」を作成しました。", "作成完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"従業員作成中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
