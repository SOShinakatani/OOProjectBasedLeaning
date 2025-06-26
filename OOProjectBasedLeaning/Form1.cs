namespace OOProjectBasedLeaning
{

    public partial class Form1 : Form
    {
        private ListBox logListBox;
        private TimeTrackerModel tracker;

        public Form1()
        {

            InitializeComponent();

            this.Text = "�o�ދ΃��O";

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
            homeForm.SetLogHandler(Tracker_LogUpdated); // �����ꂪ�厖�I
            homeForm.Show();

            new CompanyForm().Show();

        }

        // ���O�X�V���ɌĂ΂��C�x���g�n���h��
        private void Tracker_LogUpdated(object? sender, string logMessage)
        {
            logListBox.Items.Insert(0, logMessage);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ���[�h���̏���������
            Console.WriteLine("Form1 �����[�h����܂����B");

            // �K�v�ɉ����ď�����������ǉ�
            // ��: �f�[�^�x�[�X�ڑ��AUI�̏�����Ԑݒ�Ȃ�
        }

    }

}
