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

            RefreshEmployeeList(); // �����\���̏]�ƈ����X�g��ǂݍ���
        }

        /// <summary>
        /// Company�̏�������TimeTracker�̓o�^
        /// </summary>
        private void InitializeCompany()
        {
            company = new CompanyModel("�T���v�����");
            company.AddTimeTracker(tracker);
        }

        /// <summary>
        /// TimeTrackerPanel�̏������ƃt�H�[���ւ̒ǉ�
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
        /// �t�H�[���̌����ڐݒ�
        /// </summary>
        private void InitializeFormUI()
        {
            this.Text = "�z�[���t�H�[��";
        }

        /// <summary>
        /// �O�����烍�O�X�V�p�C�x���g��o�^����
        /// </summary>
        public void SetLogHandler(EventHandler<string> handler)
        {
            panel.LogUpdated += handler;
        }

        /// <summary>
        /// TimeTrackerPanel �ɏ]�ƈ����X�g�̍X�V���w��
        /// </summary>
        public void RefreshEmployeeList()
        {
            panel.RefreshEmployeeList();
        }
    }
}
