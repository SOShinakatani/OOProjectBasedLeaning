using System;
using System.Drawing;
using System.Windows.Forms;
using OOProjectBasedLeaning.Models; // Assuming EmployeeModel and EmployeeRepository are in this namespace

namespace OOProjectBasedLeaning
{
    public partial class EmployeeCreatorForm : Form
    {
        private int employeeId = 10000;
        private Button createButton;
        private HomeForm homeForm;

        // HomeFormを引数に取るコンストラクタ
        public EmployeeCreatorForm(HomeForm homeForm)
        {
            this.homeForm = homeForm;
            InitializeComponent(); // UIコンポーネントの初期化はここで行われます

            // ボタンのインスタンス化とプロパティ設定
            createButton = new Button
            {
                Text = "従業員を作成",
                Location = new Point(10, 10),
                Size = new Size(120, 30)
            };
            // イベントハンドラの割り当て（ここが重要：同じイベントを二重に登録しない）
            createButton.Click += CreateGuestEvent;

            Controls.Add(createButton); // フォームにボタンを追加
        }

        // デフォルトコンストラクタ (もし使用しないなら削除しても良い)
        public EmployeeCreatorForm()
        {
            InitializeComponent(); // デフォルトコンストラクタでもUIの初期化は必要
        }

        private void EmployeeCreatorForm_Load(object sender, EventArgs e)
        {
            this.Text = "従業員作成フォーム";
        }

        // 従業員作成イベントハンドラ
        private void CreateGuestEvent(object sender, EventArgs e)
        {
            try
            {
                // employeeIdをインクリメントし、新しい従業員を作成
                // 注意: employeeId++ は現在の値を渡してからインクリメントします。
                // したがって、IDは10000、名前は従業員10001となります。
                // もしIDと名前の数字を合わせたい場合は、employeeIdを先にインクリメントするか、
                // 名前生成時に現在のemployeeIdを使うように調整してください。
                var newEmployee = new EmployeeModel(employeeId, $"従業員{employeeId}");
                employeeId++; // IDはここでインクリメント

                EmployeeRepository.Add(newEmployee); // リポジトリに新しい従業員を追加

                // HomeFormが存在する場合のみリストを更新
                if (homeForm != null)
                {
                    homeForm.RefreshEmployeeList();
                }
                else
                {
                    // HomeFormがnullの場合のログ出力など
                    Console.WriteLine("Warning: homeForm is null, cannot refresh employee list.");
                }

                MessageBox.Show($"従業員 {newEmployee.Name} を作成しました。", "作成完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"従業員作成中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}