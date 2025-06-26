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
        //private Button createButton;

        //public EmployeeCreatorForm()
        //{
        //    InitializeComponent();

        //    // 従業員作成ボタンの初期化とイベント登録
        //    createButton = new Button
        //    {
        //        Text = "従業員を作成",
        //        Location = new Point(10, 10),
        //        Size = new Size(120, 30)
        //    };
        //    createButton.Click += CreateGuestEvent;

        //    Controls.Add(createButton);
        //}

        // フォームロード時の初期化処理
        private void EmployeeCreatorForm_Load(object sender, EventArgs e)
        {
            this.Text = "従業員作成フォーム";

        }

    }
}
