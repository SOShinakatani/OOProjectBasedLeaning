using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{
    /// <summary>
    /// 従業員を作成し、パネルとしてフォームに追加するフォーム。
    /// </summary>
    public partial class EmployeeCreatorForm : Form
    {
        private int employeeId = 10000;
        private Button createButton;

        public EmployeeCreatorForm()
        {
            InitializeComponent();

            // ボタンの初期化とイベント登録
            createButton = new Button
            {
                Text = "従業員を作成",
                Location = new Point(10, 10),
                Size = new Size(120, 30)
            };
            createButton.Click += CreateGuestEvent;

            Controls.Add(createButton);
        }

        /// <summary>
        /// 従業員作成イベント。新しい従業員パネルをフォームに追加します。
        /// </summary>
        private void CreateGuestEvent(object sender, EventArgs e)
        {
            var newEmployee = CreateEmployee();

            var panel = new EmployeePanel(newEmployee)
            {
                Location = new Point(10, 50 + (Controls.Count - 1) * 60),
                Width = 300
            };

            Controls.Add(panel);
        }

        /// <summary>
        /// 新しい従業員インスタンスを生成します。
        /// </summary>
        private Employee CreateEmployee()
        {
            employeeId++;
            return new EmployeeModel(employeeId, "Employee" + employeeId);
        }

        /// <summary>
        /// フォームロード時の初期化処理。
        /// </summary>
        private void EmployeeCreatorForm_Load(object sender, EventArgs e)
        {
            this.Text = "従業員作成フォーム";
        }
    }
}
