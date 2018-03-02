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
    public partial class ChangePasswordForm : Form
    {
        User user;
        DatabaseEntities db;


        public ChangePasswordForm(User u)
        {
            db = new DatabaseEntities();

            user = u;

            InitializeComponent();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {

            if (!ValidateEmail(txtEmail.Text))
            {
                return;
            }
            else if (!ValidatePassword(txtConfirmPass.Text, txtNewPass.Text, txtOldPass.Text))
            {
                return;
            }
            else
            {
                User u = (from x in db.Users
                          where x.UserID == user.UserID
                          select x).First();

                u.UserPassword = txtNewPass.Text;
                db.SaveChanges();

                this.Close();
            }



        }


        //
        private bool ValidateEmail(string email)
        {
            int countAt = 0, countDot = 0;


            if (email.Equals(""))
            {
                MessageBox.Show("Email Must Be Filled!");
                return false;
            }

            for (int i = 0; i < email.Length; i++)
            {
                if (email.ElementAt(i) == '@')
                {
                    countAt++;

                    if (i == 0 || i == email.Length - 1)
                    {
                        MessageBox.Show("Email is not valid");
                        return false;
                    }
                    else if (email.ElementAt(i + 1) == '.' || email.ElementAt(i - 1) == '.')
                    {
                        MessageBox.Show("Email is not valid");
                        return false;
                    }
                    else if (countAt > 1)
                    {
                        MessageBox.Show("Email is not valid");
                        return false;
                    }
                }
                else if (email.ElementAt(i) == '.')
                {
                    countDot++;
                }
            }

            if (countDot == 0 || countAt == 0)
            {
                MessageBox.Show("Email is not valid");
                return false;
            }
            else if (user.UserEmail != txtEmail.Text)
            {
                MessageBox.Show("Email is wrong");
                return false;
            }

            return true;

        }


        private bool ValidatePassword(string confirmPass, string newPass, string oldPass)
        {
            if (oldPass.Equals(""))
            {
                MessageBox.Show("Old Password Must Be Filled!");
                return false;
            }
            else if (user.UserPassword != oldPass)
            {
                MessageBox.Show("Old Password is wrong");
                return false;
            }
            else if (newPass.Equals(""))
            {
                MessageBox.Show("New Password Must Be Filled!");
                return false;
            }
            else if (confirmPass.Equals(""))
            {
                MessageBox.Show("Confirm Password Must Be Filled!");
                return false;
            }
            else if (confirmPass != newPass)
            {
                MessageBox.Show("New Password and Confirm Password must be the same");
                return false;
            }


            return true;
        }
    }
}
