
namespace OOProjectBasedLeaning
{
    partial class EmployeeCreatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private TextBox nameTextBox;

        private void InitializeComponent()
        {
            //button1 = new Button();
            nameTextBox = new TextBox(); // ← 追加
            SuspendLayout();

            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(50, 50);
            nameTextBox.Size = new Size(300, 27);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.TabIndex = 1;

            // 
            // button1
            // 
            //button1.Location = new Point(491, 16);
            //button1.Margin = new Padding(3, 4, 3, 4);
            //button1.Name = "button1";
            //button1.Size = new Size(102, 41);
            //button1.TabIndex = 0;
            //button1.Text = "従業員の作成";
            //button1.UseVisualStyleBackColor = true;
            //button1.Click += CreateGuestEvent;

            // 
            // EmployeeCreatorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(607, 419);
            Controls.Add(button1);
            Controls.Add(nameTextBox); // ← 追加
            Margin = new Padding(3, 4, 3, 4);
            Name = "EmployeeCreatorForm";
            Text = "EmployeeCreatorForm";
            Load += EmployeeCreatorForm_Load;
            ResumeLayout(false);
            PerformLayout(); // ← Layout更新
        }


        private void CreateGuestEvent(object sender, EventArgs e)
        {
            string employeeName = nameTextBox.Text;
            MessageBox.Show($"従業員「{employeeName}」を作成しました。");
        }


        #endregion

        private Button button1;
    }
}