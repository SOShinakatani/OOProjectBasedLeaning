using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{

    public partial class CompanyForm : Form
    {

        private Company company = NullCompany.Instance;

        public CompanyForm()
        {
            //InitializeComponent();

            //company = new CompanyModel("MyCompany");

            //// タイムレコーダーのパネルを設置する
            //var clockInButton = new Button
            //{
            //    Text = "出勤",
            //    Location = new Point(20, 20),
            //    Size = new Size(100, 30)
            //};
            //clockInButton.Click += (sender, e) =>
            //{
            //    // 仮の従業員ID（例：1000）で出勤処理
            //    var employee = company.FindEmployeeById(1000);
            //    company.ClockIn(employee);
            //    MessageBox.Show($"{employee.Name} が出勤しました。");
            //};

            //var clockOutButton = new Button
            //{
            //    Text = "退勤",
            //    Location = new Point(140, 20),
            //    Size = new Size(100, 30)
            //};
            //clockOutButton.Click += (sender, e) =>
            //{
            //    var employee = company.FindEmployeeById(1000);
            //    company.ClockOut(employee);
            //    MessageBox.Show($"{employee.Name} が退勤しました。");
            //};

            //this.Controls.Add(clockInButton);
            //this.Controls.Add(clockOutButton);


        }


        private void CompanyForm_Load(object sender, EventArgs e)
        {
            // フォームのタイトルを設定
            this.Text = company.Name;

            // 従業員一覧やタイムトラッカーの初期表示などをここで行う
            // 例: 従業員一覧を ListBox に表示するなど
        }

    }

}
