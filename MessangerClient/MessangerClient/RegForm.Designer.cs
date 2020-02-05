namespace MessangerClient
{
    partial class RegForm
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
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.passTextBox1 = new System.Windows.Forms.TextBox();
            this.passTextBox2 = new System.Windows.Forms.TextBox();
            this.regButton = new System.Windows.Forms.Button();
            this.loginLabel = new System.Windows.Forms.Label();
            this.passLabel1 = new System.Windows.Forms.Label();
            this.passLabel2 = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(142, 26);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(130, 20);
            this.loginTextBox.TabIndex = 0;
            // 
            // passTextBox1
            // 
            this.passTextBox1.Location = new System.Drawing.Point(142, 52);
            this.passTextBox1.Name = "passTextBox1";
            this.passTextBox1.Size = new System.Drawing.Size(130, 20);
            this.passTextBox1.TabIndex = 1;
            // 
            // passTextBox2
            // 
            this.passTextBox2.Location = new System.Drawing.Point(142, 78);
            this.passTextBox2.Name = "passTextBox2";
            this.passTextBox2.Size = new System.Drawing.Size(130, 20);
            this.passTextBox2.TabIndex = 2;
            // 
            // regButton
            // 
            this.regButton.Location = new System.Drawing.Point(22, 109);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(120, 30);
            this.regButton.TabIndex = 3;
            this.regButton.Text = "Register";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.regButton_Click);
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(100, 29);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(36, 13);
            this.loginLabel.TabIndex = 4;
            this.loginLabel.Text = "Login:";
            // 
            // passLabel1
            // 
            this.passLabel1.AutoSize = true;
            this.passLabel1.Location = new System.Drawing.Point(80, 55);
            this.passLabel1.Name = "passLabel1";
            this.passLabel1.Size = new System.Drawing.Size(56, 13);
            this.passLabel1.TabIndex = 5;
            this.passLabel1.Text = "Password:";
            // 
            // passLabel2
            // 
            this.passLabel2.AutoSize = true;
            this.passLabel2.Location = new System.Drawing.Point(19, 81);
            this.passLabel2.Name = "passLabel2";
            this.passLabel2.Size = new System.Drawing.Size(117, 13);
            this.passLabel2.TabIndex = 6;
            this.passLabel2.Text = "Password confomation:";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(0, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(11, 13);
            this.errorLabel.TabIndex = 7;
            this.errorLabel.Text = "*";
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(148, 109);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(123, 30);
            this.backButton.TabIndex = 8;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // RegForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 163);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.passLabel2);
            this.Controls.Add(this.passLabel1);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.regButton);
            this.Controls.Add(this.passTextBox2);
            this.Controls.Add(this.passTextBox1);
            this.Controls.Add(this.loginTextBox);
            this.Name = "RegForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.TextBox passTextBox1;
        private System.Windows.Forms.TextBox passTextBox2;
        private System.Windows.Forms.Button regButton;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.Label passLabel1;
        private System.Windows.Forms.Label passLabel2;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Button backButton;
    }
}