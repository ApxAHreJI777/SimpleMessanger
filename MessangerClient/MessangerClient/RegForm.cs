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
    public partial class RegForm : Form
    {
        Color defBlack = Color.Black;
        Color defRed = Color.Red;

        Authorization user;
        StartForm startForm;
        string name = String.Empty;
        string pass = String.Empty;
        public RegForm(Authorization _u, StartForm _sf)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.appicon;
            startForm = _sf;
            user = _u;
            user.Registered += Auth_Registered;
        }

        private void regButton_Click(object sender, EventArgs e)
        { 
            loginLabel.ForeColor = defBlack;
            passLabel1.ForeColor = defBlack;
            passLabel2.ForeColor = defBlack;
            name = loginTextBox.Text;
            pass = passTextBox1.Text;
            string confPass = passTextBox2.Text;

            if (name == String.Empty)
            {
                errorLabel.Text = "Enter name";
                loginLabel.ForeColor = defRed;
                return;
            }
            if (pass == String.Empty)
            {
                errorLabel.Text = "Enter password";
                passLabel1.ForeColor = defRed;
                return;
            }
            if (pass != confPass)
            {
                errorLabel.Text = "Password and Conformation password must match";
                passLabel2.ForeColor = defRed;
                return;
            }
            byte valCode = Utils.ValidateUserInfo(name, pass);
            if (valCode == MsgType.OK)
            {
                user.Connect(name, pass, false);
            }
            else 
            {
                errorLabel.Text = GetInfoMessage(valCode);
            }
        }

        void ChengeTextColor(TextBox tb, Color color)
        {
            tb.ForeColor = color;
        }

        void Auth_Registered(object sender, RegisterEventArgs e)
        {
            if (e.Code == MsgType.OK)
            {
                startForm.loginTextBox.BeginInvoke(new Action(() => { startForm.loginTextBox.Text = name; }));
                startForm.passTextBox.BeginInvoke(new Action(() => { startForm.passTextBox.Text = pass; }));
                startForm.errorLabel.BeginInvoke(new Action(() => { startForm.errorLabel.Text = e.Message; }));
                this.DialogResult = DialogResult.OK;
            }
            errorLabel.BeginInvoke(new Action(() => { errorLabel.Text = e.Message; }));
        }

        string GetInfoMessage(byte code)
        {
            string msg = "Can't register";
            if (code == MsgType.OK)
            {
                msg = "Registered succesfully";
            }
            else if (code == MsgType.NAME_TOO_LONG)
            {
                msg = "Name must be less than 40 symbols";
                loginLabel.ForeColor = defRed;
            }
            else if (code == MsgType.PASSWORD_TOO_SMALL)
            {
                msg = "Password must be more than 4 symbols";
                passLabel1.ForeColor = defRed;
            }
            else if (code == MsgType.PASSWORD_TOO_LONG)
            {
                msg = "Password must be less than 20 symbols";
                passLabel1.ForeColor = defRed;
            }
            else if (code == MsgType.INVALID_NAME)
            {
                msg = "Name must not contein \"< > / ; :\" symbols";
                loginLabel.ForeColor = defRed;
            }
            else if (code == MsgType.NAME_EXISTS)
            {
                msg = "User with this name already exists";
                loginLabel.ForeColor = defRed;
            }
            return msg;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
