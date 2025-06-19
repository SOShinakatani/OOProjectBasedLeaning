namespace OOProjectBasedLeaning
{

    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();

            // 従業員の作成
            new EmployeeCreatorForm().Show();

            // 家
            new HomeForm().Show();

            // 会社
            new CompanyForm().Show();

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
