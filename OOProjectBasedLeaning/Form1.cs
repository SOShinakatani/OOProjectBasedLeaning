using System;
using System.Drawing;
using System.Windows.Forms;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    public partial class Form1 : Form
    {
        private ListBox logListBox;
        private TimeTrackerModel tracker;

        public Form1()
        {
            InitializeComponent();

            this.Text = "出退勤ログ";

            // TimeTrackerModel を初期化
            tracker = new TimeTrackerModel(NullCompany.Instance);

            // ログ表示用 ListBox を作成
            logListBox = new ListBox
            {
                Location = new Point(10, 10),
                Size = new Size(400, 200)
            };
            this.Controls.Add(logListBox);

            // ログ更新イベントの登録
            tracker.LogUpdated += Tracker_LogUpdated;

            // HomeForm を先に作成
            var homeForm = new HomeForm(tracker);
            homeForm.SetLogHandler(Tracker_LogUpdated);
            homeForm.Show();

            // HomeForm を渡して EmployeeCreatorForm を作成
            var creatorForm = new EmployeeCreatorForm(homeForm);
            creatorForm.Show();

            // CompanyForm を表示
            new CompanyForm().Show();
        }

        // ログ更新時に呼ばれるイベントハンドラ
        private void Tracker_LogUpdated(object? sender, string logMessage)
        {
            logListBox.Items.Insert(0, logMessage);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form1 がロードされました。");
        }
    }
}
