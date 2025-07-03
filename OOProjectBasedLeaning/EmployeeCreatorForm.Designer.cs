using System.Drawing;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{
    partial class EmployeeCreatorForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox nameTextBox;
        private Button createButton;

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
            components = new System.ComponentModel.Container();
            nameTextBox = new TextBox();
            createButton = new Button();

            SuspendLayout();

            // nameTextBox
            nameTextBox.Location = new Point(50, 50);
            nameTextBox.Size = new Size(300, 27);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.TabIndex = 0;

            // createButton
            createButton.Location = new Point(50, 100);
            createButton.Size = new Size(120, 30);
            createButton.Name = "createButton";
            createButton.Text = "従業員を作成";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += CreateGuestEvent;

            // EmployeeCreatorForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 200);
            Controls.Add(nameTextBox);
            Controls.Add(createButton);
            Name = "EmployeeCreatorForm";
            Text = "従業員作成フォーム";
            Load += EmployeeCreatorForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
