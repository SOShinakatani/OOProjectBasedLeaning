namespace OOProjectBasedLeaning
{

    public partial class HomeForm : Form
    {

        public HomeForm()
        {

            InitializeComponent();

            // 1. 本物の会社（CompanyModel）を作る
            var company = new CompanyModel("サンプル会社");

            // 2. TimeTrackerModel にその会社を渡して作る
            var timeTracker = new TimeTrackerModel(company);

            // 3. 会社側にも TimeTracker を登録しておく（双方向リンク）
            company.AddTimeTracker(timeTracker);

            // 4. パネルを作る（companyとtimeTrackerの両方を渡す）
            var panel = new TimeTrackerPanel(timeTracker, company);

            // 5. パネルをフォームに追加
            this.Controls.Add(panel); ;

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }
    }

}
