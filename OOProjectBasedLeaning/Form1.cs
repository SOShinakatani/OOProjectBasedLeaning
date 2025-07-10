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
            InitializeForm();
            InitializeTracker();
            InitializeLogListBox();
            InitializeChildForms();
        }

        /// <summary>
        /// フォームの基本設定
        /// </summary>
        private void InitializeForm()
        {
            this.Text = "出退勤ログ";
        }

        /// <summary>
        /// TimeTrackerModelの初期化とイベント登録
        /// </summary>
        private void InitializeTracker()
        {
            tracker = new TimeTrackerModel(NullCompany.Instance);
            tracker.LogUpdated += Tracker_LogUpdated;
        }

        /// <summary>
        /// ログ表示用ListBoxの設定
        /// </summary>
        private void InitializeLogListBox()
        {
            logListBox = new ListBox
            {
                Location = new Point(10, 10),
                Size = new Size(500, 200)
            };
            Controls.Add(logListBox);
        }

        /// <summary>
        /// 子フォーム（HomeForm、EmployeeCreatorForm）の作成と表示
        /// </summary>
        private void InitializeChildForms()
        {
            var homeForm = new HomeForm(tracker);
            homeForm.SetLogHandler(Tracker_LogUpdated);
            homeForm.Show();

            var creatorForm = new EmployeeCreatorForm(homeForm);
            creatorForm.Show();

            // new CompanyForm().Show(); // ← 必要であれば有効化
        }

        /// <summary>
        /// ログ更新時に呼ばれるイベントハンドラ
        /// </summary>
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
