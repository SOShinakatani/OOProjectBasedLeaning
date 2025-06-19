using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{

    public class TimeTrackerPanel : Panel
    {

        private TimeTracker timeTracker;
        private Button btnPunchIn;
        private Button btnPunchOut;
        private Label lblStatus;

        // テスト用に仮の従業員IDを指定（本番ではログイン情報と連携する）
        private int employeeId = 1;

        public TimeTrackerPanel(TimeTracker timeTracker)
        {

            this.timeTracker = timeTracker;

            InitializeComponent();

        }

        private void InitializeComponent()
        {
            // パネルサイズを適当に設定
            this.Size = new Size(300, 150);

            // 出勤ボタン
            btnPunchIn = new Button
            {
                Text = "出勤",
                Location = new Point(10, 10),
                Size = new Size(100, 30)
            };
            btnPunchIn.Click += BtnPunchIn_Click;

            // 退勤ボタン
            btnPunchOut = new Button
            {
                Text = "退勤",
                Location = new Point(120, 10),
                Size = new Size(100, 30)
            };
            btnPunchOut.Click += BtnPunchOut_Click;

            // 状態表示ラベル
            lblStatus = new Label
            {
                Text = "状態: 未出勤",
                Location = new Point(10, 60),
                AutoSize = true
            };

            // パネルに追加
            this.Controls.Add(btnPunchIn);
            this.Controls.Add(btnPunchOut);
            this.Controls.Add(lblStatus);


            // Initialize UI components for the Time Tracker panel
            // This could include buttons for PunchIn, PunchOut, and displaying status

        }

        private void BtnPunchIn_Click(object sender, EventArgs e)
        {
            try
            {
                timeTracker.PunchIn(employeeId);
                lblStatus.Text = "状態: 出勤中";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出勤エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPunchOut_Click(object sender, EventArgs e)
        {
            try
            {
                timeTracker.PunchOut(employeeId);
                lblStatus.Text = "状態: 退勤済み";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "退勤エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Methods to handle user interactions like PunchIn, PunchOut, etc.

    }

}
