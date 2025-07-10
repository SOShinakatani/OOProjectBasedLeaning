using System;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメインエントリポイント
        /// </summary>
        [STAThread]
        static void Main()
        {
            // アプリケーション設定の初期化（高DPI設定やデフォルトフォントなど）
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
