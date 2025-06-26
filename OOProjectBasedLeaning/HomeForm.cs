namespace OOProjectBasedLeaning
{

    public partial class HomeForm : Form
    {

        public HomeForm()
        {

            InitializeComponent();

            // 1. �{���̉�ЁiCompanyModel�j�����
            var company = new CompanyModel("�T���v�����");

            // 2. TimeTrackerModel �ɂ��̉�Ђ�n���č��
            var timeTracker = new TimeTrackerModel(company);

            // 3. ��Б��ɂ� TimeTracker ��o�^���Ă����i�o���������N�j
            company.AddTimeTracker(timeTracker);

            // 4. �p�l�������icompany��timeTracker�̗�����n���j
            var panel = new TimeTrackerPanel(timeTracker, company);

            // 5. �p�l�����t�H�[���ɒǉ�
            this.Controls.Add(panel); ;

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }
    }

}
