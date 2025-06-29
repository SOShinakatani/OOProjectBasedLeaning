﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOProjectBasedLeaning;
using OOProjectBasedLeaning.Models;



namespace OOProjectBasedLeaning
{

    public class TimeTrackerPanel : Panel
    {
        private ComboBox employeeSelector;

        // フィールド（クラス内で使う変数）
        private TimeTracker timeTracker; // ← ここで一度だけ定義
        private Company company;

        private Button btnPunchIn;
        private Button btnPunchOut;
        private Label lblStatus;
        private Label lblPunchInTime;
        private Label lblPunchOutTime;

        // テスト用に仮の従業員IDを指定（本番ではログイン情報と連携する）
        private int employeeId = 1;


        public TimeTrackerPanel(TimeTracker timeTracker, Company company)
        {
            this.timeTracker = timeTracker;
            this.company = company;
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

            employeeSelector = new ComboBox
            {
                Location = new Point(230, 10),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            if (company is CompanyModel model)
            {
                var field = typeof(CompanyModel)
                    .GetField("employees", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (field != null)
                {
                    var employeeDict = field.GetValue(model) as Dictionary<int, Employee>;
                    if (employeeDict != null)
                    {
                        var employeeList = employeeDict.Values.ToList();
                        employeeSelector.Items.AddRange(employeeList.ToArray());

                        if (employeeSelector.Items.Count > 0)
                        {
                            employeeSelector.SelectedIndex = 0;
                        }
                    }
                }
            }

            // 状態表示ラベル
            lblStatus = new Label
            {
                Text = "状態: 未出勤",
                Location = new Point(10, 50),
                AutoSize = true
            };

            lblPunchInTime = new Label
            {
                Text = "出勤時間: -",
                Location = new Point(10, 80),
                AutoSize = true
            };

            lblPunchOutTime = new Label
            {
                Text = "退勤時間: -",
                Location = new Point(10, 110),
                AutoSize = true
            };

            Controls.Add(btnPunchIn);
            Controls.Add(btnPunchOut);
            Controls.Add(employeeSelector);
            Controls.Add(lblStatus);
            Controls.Add(lblPunchInTime);
            Controls.Add(lblPunchOutTime);

        }

        public event EventHandler<string>? LogUpdated;

        private void BtnPunchIn_Click(object sender, EventArgs e)
        {
            if (employeeSelector.SelectedItem is Employee employee)
            {
                try
                {
                    timeTracker.PunchIn(employee.Id);
                    lblStatus.Text = $"{employee.Name} さんは現在、出勤中";

                    if (timeTracker is TimeTrackerModel model &&
                        model.TryGetPunchInTime(employee.Id, out DateTime time))
                    {
                        lblPunchInTime.Text = $"出勤時間: {time:yyyy/MM/dd HH:mm}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "出勤エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }


        private void BtnPunchOut_Click(object sender, EventArgs e)
        {
            if (employeeSelector.SelectedItem is Employee employee)
    {
        try
        {
            timeTracker.PunchOut(employee.Id);
            lblStatus.Text = $"{employee.Name} さんは現在、退勤済み";

            if (timeTracker is TimeTrackerModel model &&
                model.TryGetPunchOutTime(employee.Id, out DateTime time))
            {
                lblPunchOutTime.Text = $"退勤時間: {time:yyyy/MM/dd HH:mm}";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "退勤エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

            // Methods to handle user interactions like PunchIn, PunchOut, etc.

        }

    }
}
