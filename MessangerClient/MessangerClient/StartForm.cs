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
    public partial class StartForm : Form
    {
        Authorization user;

        public StartForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.appicon;
            user = new Authorization();
            user.Connected += Auth_Connected;
            user.LogedIn += Auth_Logedin;
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            user.Connected -= Auth_Connected;
            user.LogedIn -= Auth_Logedin;
            RegForm regForm = new RegForm(user, this);
            regForm.ShowDialog();
            user.Connected += Auth_Connected;
            user.LogedIn += Auth_Logedin;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string userName = loginTextBox.Text;
            string userPass = passTextBox.Text;
            user.Connect(userName, userPass, true);
        }

        void Auth_Connected(object sender, ConnectedEventArgs e)
        {
            //this.BeginInvoke(new Action(() => { this.errorLabel.Text = e.Message; }));
        }

        void Auth_Logedin(object sender, LoginEventArgs e)
        {
            if (e.IsLogedin)
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.Hide(); 
                    Form1 talkForm = new Form1(user, this);
                    talkForm.Show();
                }));
            }
            else
            {
                errorLabel.BeginInvoke(new Action(() => { this.errorLabel.Text = e.Message; }));
            }
        }

        public void Resume(bool isExit)
        {
            if (isExit)
            {
                this.Close();
            }
            else 
            {
                errorLabel.BeginInvoke(new Action(() => { this.errorLabel.Text = String.Empty; }));
                this.Show();
                user.Logout();
            }
        }
    }
}
