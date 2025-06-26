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

            tracker = new TimeTrackerModel(NullCompany.Instance);

            logListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(400, 200)
            };
            this.Controls.Add(logListBox);

            tracker.LogUpdated += Tracker_LogUpdated;

            new EmployeeCreatorForm().Show();

            var homeForm = new HomeForm(tracker);
            homeForm.SetLogHandler(Tracker_LogUpdated); // ←これが大事！
            homeForm.Show();

            new CompanyForm().Show();

        }

        // ログ更新時に呼ばれるイベントハンドラ
        private void Tracker_LogUpdated(object? sender, string logMessage)
        {
            logListBox.Items.Insert(0, logMessage);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ロード時の初期化処理
            Console.WriteLine("Form1 がロードされました。");

            // 必要に応じて初期化処理を追加
            // 例: データベース接続、UIの初期状態設定など
        }

    }

}
