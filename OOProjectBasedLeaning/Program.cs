using System;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{
    internal static class Program
    {
        /// <summary>
        /// �A�v���P�[�V�����̃��C���G���g���|�C���g
        /// </summary>
        [STAThread]
        static void Main()
        {
            // �A�v���P�[�V�����ݒ�̏������i��DPI�ݒ��f�t�H���g�t�H���g�Ȃǁj
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
