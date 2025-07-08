namespace OOProjectBasedLeaning
{
    partial class EmployeeCreatorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button createButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(12, 12);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(260, 23);
            this.nameTextBox.TabIndex = 0;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(12, 50);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(100, 30);
            this.createButton.TabIndex = 1;
            this.createButton.Text = "作成";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.CreateGuestEvent);
            // 
            // EmployeeCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.nameTextBox);
            this.Name = "EmployeeCreatorForm";
            this.Text = "従業員作成フォーム";
            this.Load += new System.EventHandler(this.EmployeeCreatorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
