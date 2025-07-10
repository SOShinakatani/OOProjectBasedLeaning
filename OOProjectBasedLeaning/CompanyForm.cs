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
