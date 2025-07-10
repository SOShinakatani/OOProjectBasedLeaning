using System;
using System.Drawing;
using System.Windows.Forms;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    /// <summary>
    /// 従業員情報を表示・編集するためのパネルクラス。
    /// 編集モードの切り替えや、従業員情報の更新に対応。
    /// </summary>
    public class EmployeePanel : Panel, Observer, EmployeePanel.Editable
    {
        private Employee employee;
        private EditMode editMode = EditMode.On;
        private TextBox employeeNameTextBox;

        /// <summary>
        /// 従業員パネルのコンストラクタ。
        /// </summary>
        /// <param name="employee">表示対象の従業員</param>
        public EmployeePanel(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
        }

        /// <summary>
        /// 編集可能なパネルとしてのインターフェース。
        /// 表示モードと編集モードの切り替えを定義。
        /// </summary>
        public interface Editable
        {
            void ChangeDisplayMode();
            void ChangeEditMode();
        }

        /// <summary>
        /// パネルの初期化処理。ラベルとテキストボックスを配置。
        /// </summary>
        private void InitializeComponent()
        {
            Label employeeNameLabel = new Label
            {
                Text = "名前：",
                AutoSize = true,
                Location = new Point(20, 10)
            };

            employeeNameTextBox = new TextBox
            {
                Text = employee.Name,
                Location = new Point(140, 6),
                Width = 160
            };

            Controls.Add(employeeNameLabel);
            Controls.Add(employeeNameTextBox);
        }

        /// <summary>
        /// 編集モードに切り替える。テキストボックスを有効化。
        /// </summary>
        public void ChangeEditMode()
        {
            editMode = EditMode.On;
            employeeNameTextBox.Enabled = true;
        }

        /// <summary>
        /// 表示モードに切り替える。テキストボックスを無効化。
        /// </summary>
        public void ChangeDisplayMode()
        {
            editMode = EditMode.Off;
            employeeNameTextBox.Enabled = false;
        }

        /// <summary>
        /// 現在が編集モードかどうかを判定。
        /// </summary>
        /// <returns>編集モードなら true</returns>
        public bool IsEditMode()
        {
            return editMode == EditMode.On;
        }

        /// <summary>
        /// 従業員情報が更新されたときに呼び出される。
        /// テキストボックスの表示内容を更新。
        /// </summary>
        /// <param name="sender">更新された従業員オブジェクト</param>
        public void Update(object sender)
        {
            if (sender is Employee updatedEmployee)
            {
                employee = updatedEmployee;
                employeeNameTextBox.Text = employee.Name;
            }
        }

        /// <summary>
        /// 編集モードの状態を表す列挙型。
        /// </summary>
        private enum EditMode
        {
            On,
            Off
        }
    }
}
