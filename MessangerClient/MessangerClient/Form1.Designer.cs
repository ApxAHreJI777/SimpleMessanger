namespace MessangerClient
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusDDButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.onlinStatusItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doNotDStatusItem = new System.Windows.Forms.ToolStripMenuItem();
            this.awayStatusItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendButton = new System.Windows.Forms.Button();
            this.onlineListView = new System.Windows.Forms.ListView();
            this.msgRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.chatTabControlEx = new MessangerClient.CustomTabControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.sendButton, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.onlineListView, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.msgRichTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chatTabControlEx, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(552, 362);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusDDButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(552, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusDDButton
            // 
            this.statusDDButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusDDButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlinStatusItem,
            this.doNotDStatusItem,
            this.awayStatusItem});
            this.statusDDButton.Image = global::MessangerClient.Properties.Resources.greenImg;
            this.statusDDButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statusDDButton.Name = "statusDDButton";
            this.statusDDButton.Size = new System.Drawing.Size(29, 22);
            this.statusDDButton.Text = "toolStripDropDownButton1";
            this.statusDDButton.ToolTipText = "Online";
            this.statusDDButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusDDButton_DropDownItemClicked);
            // 
            // onlinStatusItem
            // 
            this.onlinStatusItem.Image = global::MessangerClient.Properties.Resources.greenImg;
            this.onlinStatusItem.Name = "onlinStatusItem";
            this.onlinStatusItem.Size = new System.Drawing.Size(155, 22);
            this.onlinStatusItem.Text = "Online";
            // 
            // doNotDStatusItem
            // 
            this.doNotDStatusItem.Image = global::MessangerClient.Properties.Resources.redImg;
            this.doNotDStatusItem.Name = "doNotDStatusItem";
            this.doNotDStatusItem.Size = new System.Drawing.Size(155, 22);
            this.doNotDStatusItem.Text = "Do Not Disturb";
            // 
            // awayStatusItem
            // 
            this.awayStatusItem.Image = global::MessangerClient.Properties.Resources.yellowImg;
            this.awayStatusItem.Name = "awayStatusItem";
            this.awayStatusItem.Size = new System.Drawing.Size(155, 22);
            this.awayStatusItem.Text = "Away";
            // 
            // sendButton
            // 
            this.sendButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.sendButton.Location = new System.Drawing.Point(3, 330);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(92, 29);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send Message";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // onlineListView
            // 
            this.onlineListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.onlineListView.Location = new System.Drawing.Point(444, 44);
            this.onlineListView.Name = "onlineListView";
            this.tableLayoutPanel1.SetRowSpan(this.onlineListView, 2);
            this.onlineListView.Size = new System.Drawing.Size(105, 280);
            this.onlineListView.TabIndex = 5;
            this.onlineListView.UseCompatibleStateImageBehavior = false;
            this.onlineListView.View = System.Windows.Forms.View.SmallIcon;
            this.onlineListView.ItemActivate += new System.EventHandler(this.onlineListView_ItemActivate);
            // 
            // msgRichTextBox
            // 
            this.msgRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgRichTextBox.Location = new System.Drawing.Point(3, 229);
            this.msgRichTextBox.Name = "msgRichTextBox";
            this.msgRichTextBox.Size = new System.Drawing.Size(435, 95);
            this.msgRichTextBox.TabIndex = 1;
            this.msgRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(444, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Users Online:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(552, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "Messanger";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // chatTabControlEx
            // 
            this.chatTabControlEx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatTabControlEx.Location = new System.Drawing.Point(3, 28);
            this.chatTabControlEx.Name = "chatTabControlEx";
            this.tableLayoutPanel1.SetRowSpan(this.chatTabControlEx, 2);
            this.chatTabControlEx.SelectedIndex = 0;
            this.chatTabControlEx.Size = new System.Drawing.Size(435, 195);
            this.chatTabControlEx.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 386);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Messanger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox msgRichTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.ListView onlineListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton statusDDButton;
        private System.Windows.Forms.ToolStripMenuItem onlinStatusItem;
        private System.Windows.Forms.ToolStripMenuItem doNotDStatusItem;
        private System.Windows.Forms.ToolStripMenuItem awayStatusItem;
        private CustomTabControl chatTabControlEx;
    }
}

