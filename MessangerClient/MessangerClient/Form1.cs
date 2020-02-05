using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MessangerClient
{
    public partial class Form1 : Form
    {
        Authorization user;
        //string currentPage;
        bool close;
        bool logout;
        Image imgGreen = Properties.Resources.greenImg;
        Image imgRed = Properties.Resources.redImg;
        Image imgYellow = Properties.Resources.yellowImg;
        Image imgGrey = Properties.Resources.greyImg;
        ImageList imgList = new ImageList();
        StartForm startForm;

        public Form1(Authorization _u, StartForm sf)
        {
            close = false;
            logout = false;
            startForm = sf;
            InitializeComponent();
            InitNotifyIcon();
            this.Icon = Properties.Resources.appicon;
            imgList.Images.Add(imgGreen);
            imgList.Images.Add(imgRed);
            imgList.Images.Add(imgYellow);
            imgList.Images.Add(imgGrey);
            onlineListView.SmallImageList = imgList;
            chatTabControlEx.ImageList = imgList;
            user = _u;
            user.UsersOnline += Auth_UsersOnline;
            user.UserChangedStatus += Auth_UserChanchedStatus;
            user.MessageRecieved += Auth_MessageRecieved;
            user.GetUsersOnline();
        }

        void InitNotifyIcon()
        {
            notifyIcon1.Icon = Properties.Resources.appicon;
            ContextMenu contextMenu = new ContextMenu();
            MenuItem exitNIMenuItem = new MenuItem();
            exitNIMenuItem.Text = "Exit";
            exitNIMenuItem.Click += new System.EventHandler(exitToolStripMenuItem_Click);

            MenuItem logoutNIMenuItem = new MenuItem();
            logoutNIMenuItem.Text = "Logout";
            logoutNIMenuItem.Click += new System.EventHandler(logoutToolStripMenuItem_Click);

            contextMenu.MenuItems.AddRange(
                 new System.Windows.Forms.MenuItem[] {logoutNIMenuItem, exitNIMenuItem });

            notifyIcon1.ContextMenu = contextMenu;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (chatTabControlEx.SelectedIndex >= 0)
            {
                TabPage tp = chatTabControlEx.SelectedTab;
                string name = tp.Name;
                RichTextBox rtb = ((RichTextBox)tp.Controls[name + "rtb"]);
                string buf = msgRichTextBox.Text;
                string msg = MessengerMsg.FormString(MsgType.SEND, name, buf);
                user.Write(msg);
                rtb.AppendText(String.Format("<{0}> {1} \n", "You", buf));
                msgRichTextBox.Clear();
            }
        }

        void Auth_MessageRecieved(object sender, MessageRecievedEventArgs e)
        {
            TabPage tp = chatTabControlEx.TabPages[e.User];
            if (tp == null)
            {
                this.Invoke(new Action(() =>
                {
                    int imgIndex = onlineListView.Items[e.User].ImageIndex;
                    tp = AddChatPage(new TabPage(), e.User, imgIndex);
                }));
            }
            RichTextBox rtb = ((RichTextBox)tp.Controls[e.User + "rtb"]);
            rtb.BeginInvoke(new Action(() =>
            {
                rtb.AppendText(String.Format("<{0}> {1} \n", e.User, e.Message));
            }));
            this.BeginInvoke(new Action(() =>
            {
                chatTabControlEx.HighlightTabPage(tp.Name);
                if (Form.ActiveForm != this)
                {
                    string balloonTitel = String.Format("New message from {0}", e.User);
                    string balloonText = e.Message.Length < 40 ? e.Message :
                    String.Format("{0}...", e.Message.Substring(0, 40));
                    ShowBalloon(balloonTitel, balloonText);
                }
            }));
            
        }

        void ShowBalloon(string titel, string msg)
        {
                notifyIcon1.BalloonTipTitle = titel;
                notifyIcon1.BalloonTipText = msg;
                notifyIcon1.BalloonTipIcon = ToolTipIcon.None;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(30000);
        }

        void Auth_UsersOnline(object sender, UsersEventArgs e)
        {
            if (e.Users.Count != 0)
            {
                onlineListView.BeginInvoke(new Action(() =>
                {
                    foreach (UsersEventInfo u in e.Users)
                    {
                        if (u.Name != user.name)
                        {
                            OnlineListAddItem(u.Name, u.Status);
                        }
                    }
                }));
            }
        }

        void TabControlStatus(string name, int code)
        {
            TabPage tp = chatTabControlEx.TabPages[name];
            if(tp != null)
            {
                int i = code - MsgType.USER_ONLINE;
                int index = i >= 0 && i < imgList.Images.Count ? i : 0;
                tp.ImageIndex = index;
            }
        }

        void OnlineListAddItem(string name, int code)
        {
            int i = code - MsgType.USER_ONLINE;
            int index = i >= 0 && i < imgList.Images.Count ? i : 0;
            ListViewItem item = onlineListView.FindItemWithText(name);
            if (item == null)
            {
                item = onlineListView.Items.Add(name, index);
                item.Name = name;
            }
            else
                item.ImageIndex = index;
        }

        void Auth_UserChanchedStatus(object sender, UserStatusEventArgs e)
        {
            onlineListView.BeginInvoke(new Action(() =>
            {
                OnlineListAddItem(e.User, e.Status);
                TabControlStatus(e.User, e.Status);
            }));
            if (e.Status == MsgType.USER_ONLINE)
                this.BeginInvoke(new Action(() =>
                {
                    if (Form.ActiveForm != this)
                    {
                        string balloonText = String.Format("{0} is Online", e.User);
                        ShowBalloon("Online:", balloonText);
                    }
                }));
        }

        private void chatTabControlEx_Selected(object sender, TabControlEventArgs e)
        {
            //if (e.TabPage != null)
            //    currentPage = e.TabPage.Name;
            //else
            //    currentPage = String.Empty;
        }

        void AddChatPage(string uName, int imgIndex)
        {
            AddChatPage(new TabPage(), uName, imgIndex);
        }

        TabPage AddChatPage(TabPage page, string uName, int imgIndex)
        {
            if (uName != null && chatTabControlEx.TabPages[uName] == null)
            {
                page.Text = uName;
                page.Name = uName;
                RichTextBox rtb = new RichTextBox();
                rtb.Dock = DockStyle.Fill;
                rtb.Name = uName + "rtb";
                rtb.ReadOnly = true;
                rtb.BackColor = Color.White;
                rtb.TextChanged += rtb_TextChanged;
                page.Controls.Add(rtb);
                chatTabControlEx.TabPages.Add(page);
                page.ImageIndex = imgIndex;
            }
            return page;
        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            rtb.SelectionStart = rtb.Text.Length;
            rtb.ScrollToCaret();
        }

        private void onlineListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem item = ((ListView)sender).SelectedItems[0];
            AddChatPage(item.Text, item.ImageIndex);
            chatTabControlEx.SelectTab(item.Text);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            close = true;
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !close)
            {
                if (!logout)
                {
                    e.Cancel = true;
                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();
                }
                else
                {
                    startForm.Resume(false);
                }
            }
            else
            {
                startForm.Resume(true);
            }
            close = false;
            logout = false;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logout = true;
            close = false;
            this.Close();
        }

        private void statusDDButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string statusText = e.ClickedItem.Text;
            e.ClickedItem.OwnerItem.Image = e.ClickedItem.Image;
            e.ClickedItem.OwnerItem.ToolTipText = statusText;
            ChangeStatus(statusText);

        }
        void ChangeStatus(string statusText)
        {
            UserStatus newStatus = user.Status;
            if (statusText == "Online")
            {
                newStatus = UserStatus.Online;
            }
            else if (statusText == "Do Not Disturb")
            {
                newStatus = UserStatus.DND;
            }
            else if (statusText == "Away")
            {
                newStatus = UserStatus.Away;
            }
            else return;

            if (newStatus != user.Status)
                user.Status = newStatus;
        }
    }
}