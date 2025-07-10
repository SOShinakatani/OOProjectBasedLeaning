using System;
using System.Windows.Forms;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    /// <summary>
    /// 従業員を作成するためのフォームクラス
    /// </summary>
    public partial class EmployeeCreatorForm : Form
    {
        // 新しい従業員に割り当てるID（初期値は10000）
        private int nextEmployeeId = 10000;

        // 従業員一覧を表示するホーム画面への参照
        private readonly HomeForm homeForm;

        /// <summary>
        /// フォームのコンストラクタ
        /// </summary>
        /// <param name="homeForm">ホーム画面のインスタンス</param>
        public EmployeeCreatorForm(HomeForm homeForm)
        {
            this.homeForm = homeForm;
            InitializeComponent();
        }

        /// <summary>
        /// フォームのロード時にタイトルを設定
        /// </summary>
        private void EmployeeCreatorForm_Load(object sender, EventArgs e)
        {
            this.Text = "従業員作成フォーム";
        }

        /// <summary>
        /// 「作成」ボタンがクリックされたときの処理
        /// </summary>
        private void CreateGuestEvent(object sender, EventArgs e)
        {
            try
            {
                // 入力された従業員名を取得
                string employeeName = nameTextBox.Text.Trim();

                // 名前が空の場合は警告を表示して処理終了
                if (string.IsNullOrEmpty(employeeName))
                {
                    MessageBox.Show("従業員名を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 新しい従業員を作成
                var newEmployee = new EmployeeModel(nextEmployeeId++, employeeName);

                // リポジトリに追加
                EmployeeRepository.Add(newEmployee);

                // ホーム画面の従業員一覧を更新
                homeForm?.RefreshEmployeeList();

                // 作成完了メッセージを表示
                MessageBox.Show($"従業員「{newEmployee.Name}」を作成しました。", "作成完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // エラー発生時のメッセージ表示
                MessageBox.Show($"従業員作成中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
