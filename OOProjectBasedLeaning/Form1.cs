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

            this.Text = "�o�ދ΃��O";

            // TimeTrackerModel ��������
            tracker = new TimeTrackerModel(NullCompany.Instance);

            // ���O�\���p ListBox ���쐬
            logListBox = new ListBox
            {
                Location = new Point(10, 10),
                Size = new Size(400, 200)
            };
            this.Controls.Add(logListBox);

            // ���O�X�V�C�x���g�̓o�^
            tracker.LogUpdated += Tracker_LogUpdated;

            // HomeForm ���ɍ쐬
            var homeForm = new HomeForm(tracker);
            homeForm.SetLogHandler(Tracker_LogUpdated);
            homeForm.Show();

            // HomeForm ��n���� EmployeeCreatorForm ���쐬
            var creatorForm = new EmployeeCreatorForm(homeForm);
            creatorForm.Show();

            // CompanyForm ��\��
            new CompanyForm().Show();
        }

        // ���O�X�V���ɌĂ΂��C�x���g�n���h��
        private void Tracker_LogUpdated(object? sender, string logMessage)
        {
            logListBox.Items.Insert(0, logMessage);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form1 �����[�h����܂����B");
        }
    }
}
