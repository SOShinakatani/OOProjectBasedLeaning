using System;
using System.Drawing;
using System.Windows.Forms;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    public partial class HomeForm : Form
    {
        private TimeTrackerModel tracker;
        private TimeTrackerPanel panel;
        private Company company;

        public HomeForm(TimeTrackerModel tracker)
        {
            this.tracker = tracker;

            InitializeComponent();
            InitializeCompany();
            InitializePanel();
            InitializeFormUI();

            RefreshEmployeeList(); // 初期表示の従業員リストを読み込み
        }

        /// <summary>
        /// Companyの初期化とTimeTrackerの登録
        /// </summary>
        private void InitializeCompany()
        {
            company = new CompanyModel("サンプル会社");
            company.AddTimeTracker(tracker);
        }

        /// <summary>
        /// TimeTrackerPanelの初期化とフォームへの追加
        /// </summary>
        private void InitializePanel()
        {
            panel = new TimeTrackerPanel(tracker, company)
            {
                Location = new Point(10, 10)
            };
            Controls.Add(panel);
        }

        /// <summary>
        /// フォームの見た目設定
        /// </summary>
        private void InitializeFormUI()
        {
            this.Text = "ホームフォーム";
        }

        /// <summary>
        /// 外部からログ更新用イベントを登録する
        /// </summary>
        public void SetLogHandler(EventHandler<string> handler)
        {
            panel.LogUpdated += handler;
        }

        /// <summary>
        /// TimeTrackerPanel に従業員リストの更新を指示
        /// </summary>
        public void RefreshEmployeeList()
        {
            panel.RefreshEmployeeList();
        }
    }
}
