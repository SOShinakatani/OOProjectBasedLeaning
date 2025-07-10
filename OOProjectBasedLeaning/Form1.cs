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
        /// �t�H�[���̊�{�ݒ�
        /// </summary>
        private void InitializeForm()
        {
            this.Text = "�o�ދ΃��O";
        }

        /// <summary>
        /// TimeTrackerModel�̏������ƃC�x���g�o�^
        /// </summary>
        private void InitializeTracker()
        {
            tracker = new TimeTrackerModel(NullCompany.Instance);
            tracker.LogUpdated += Tracker_LogUpdated;
        }

        /// <summary>
        /// ���O�\���pListBox�̐ݒ�
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
        /// �q�t�H�[���iHomeForm�AEmployeeCreatorForm�j�̍쐬�ƕ\��
        /// </summary>
        private void InitializeChildForms()
        {
            var homeForm = new HomeForm(tracker);
            homeForm.SetLogHandler(Tracker_LogUpdated);
            homeForm.Show();

            var creatorForm = new EmployeeCreatorForm(homeForm);
            creatorForm.Show();

            // new CompanyForm().Show(); // �� �K�v�ł���ΗL����
        }

        /// <summary>
        /// ���O�X�V���ɌĂ΂��C�x���g�n���h��
        /// </summary>
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
