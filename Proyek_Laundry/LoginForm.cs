using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project
{
    public partial class LoginForm : Form
    {
        DatabaseEntities db;
        public LoginForm()
        {
            db = new DatabaseEntities();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (!ValidateEmail(txtEmail.Text))
            {
                return;
            }
            else if (!ValidatePassword(txtPassword.Text))
            {
                return;
            }


            var data = from x in db.Users
                       where x.UserEmail.Equals(email) && x.UserPassword.Equals(password)
                       select x;
         

            if (data.Count() < 1)
            {
                MessageBox.Show("No User Found!");
            }
            else
            {
                User user = data.First();
                HomeForm home = new HomeForm(user);

                home.Show();

                this.Hide();

            }

        }

        private void linkLabelSIgnUp_Click(object sender, EventArgs e)
        {


            RegisterForm register = new RegisterForm();
            register.Show();

            this.Hide();

        }


        //Function

        private bool ValidateEmail(string email)
        {
            if (email.Equals(""))
            {
                MessageBox.Show("Email Must Be Filled!");
                return false;
            }
            return true;

        }

        private bool ValidatePassword(string password)
        {
            if (password.Equals(""))
            {
                MessageBox.Show("Password Must Be Filled!");
                return false;
            }

            return true;
        }


    }
}
