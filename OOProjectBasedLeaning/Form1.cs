namespace OOProjectBasedLeaning
{

    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();

            // �]�ƈ��̍쐬
            new EmployeeCreatorForm().Show();

            // ��
            new HomeForm().Show();

            // ���
            new CompanyForm().Show();

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
