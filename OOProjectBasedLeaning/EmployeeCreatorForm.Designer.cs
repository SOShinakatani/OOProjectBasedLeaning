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
        private void InitializeComponent()
        {
            button1 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(491, 16);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(102, 41);
            button1.TabIndex = 0;
            button1.Text = "従業員の作成";
            button1.UseVisualStyleBackColor = true;
            button1.Click += CreateGuestEvent;
            // 
            // EmployeeCreatorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(607, 419);
            Controls.Add(button1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "EmployeeCreatorForm";
            Text = "EmployeeCreatorForm";
            Load += EmployeeCreatorForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
    }
}