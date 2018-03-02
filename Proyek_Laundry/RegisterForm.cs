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
    public partial class RegisterForm : Form
    {
        DatabaseEntities db;
        BindingSource bsUsers;

        User user;



        public RegisterForm()
        {
            InitializeComponent();
            db = new DatabaseEntities();
            bsUsers = new BindingSource();

        }

        int counter = 0;

        private void btnRegister_Click(object sender, EventArgs e)
        {

            string confPass = txtPasswordConfirm.Text;
 





            if (!ValidatePassword(txtPassword.Text))
            {
                return;
            }
            else if (confPass.Equals(""))
            {
                MessageBox.Show("Confirm Password Must be Filled");
                return;
            }
            else if (confPass != txtPassword.Text)
            {
                MessageBox.Show("Password and Confirm Password must be the same");
                return;
            }
            else if (!ValidateEmail(txtEmail.Text))
            {
                return;
            }
            else if (!ValidatePhone(txtPhone.Text))
            {
                return;
            }
            else if (!ValidateAddress(txtAddress.Text))
            {
                return;
            }
            else
            { 
                User u = new User();

                u.UserName = txtUsername.Text;
                u.UserPassword = txtPassword.Text;
                u.UserEmail = txtEmail.Text;
                u.UserPhoneNumber = txtPhone.Text;
                u.UserAddress = txtAddress.Text;
                u.RoleName = "Member";

                //ID GENERATOR
                var data = from x in db.Users
                           select x;

                if (data.Count() != 0)
                {
                    String generator = string.Format("US{0}", (data.Count() + 2).ToString("000"));
                    u.UserID = generator;
                }
                else
                {
                    u.UserID = "US001";
                }

                db.Users.AddObject(u);
                db.SaveChanges();

                user = u;
            }


            {
                HomeForm home = new HomeForm(user);
                home.Show();

                this.Hide();
            }
           
        }

        private void linkLabelHaveAcc_Click(object sender, EventArgs e)
        {
            LoginForm Login = new LoginForm();

            Login.Show();

            this.Hide();
        }


        //
        private bool ValidateUsername(string Username)
        {
            if (Username.Equals(""))
            {
                MessageBox.Show("Username Must be Filled");
                counter++;
                return false;
            }



            for (int i = 0; i < Username.Length; i++)
            {

                if (!char.IsLetter(Username.ToCharArray()[i]))
                {
                    MessageBox.Show("Username Must be alphabet");
                    counter++;
                    return false;
                }


            }



            counter = 0;

            return true;
        }

        private bool ValidatePassword(string password)
        {
            if (password.Equals(""))
            {
                MessageBox.Show("Password Must Be Filled!");
                counter++;
                return false;
            }


            return true;
        }

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

            return true;

        }

        private bool ValidatePhone(string phone)
        {
            if (phone.Equals(""))
            {
                MessageBox.Show("Phone number Must Be Filled!");
      

                return false;
            }


            for (int i = 0; i < phone.Length; i++)
            {
                if (!char.IsDigit(phone.ToCharArray()[i]))
                {
                    MessageBox.Show("Phone number Must be numeric");
                    return false;
                }
            }
            return true;

        }

        //
        private bool ValidateAddress(string Address)
        {
            if (Address.Equals(""))
            {
                MessageBox.Show("Address Must Be Filled!");
                return false;
            }
            else if (!Address.Contains("street"))
            {
                MessageBox.Show("Address Must be Ends With 'street'");
                return false;
            }
            return true;
        }


        //Belom jadi
        private bool ColorFalse(TextBox Curr)
        {
            Curr.BackColor = Color.Red;
            return true;
        }

        private bool ColorTrue(TextBox Curr)
        {
            Curr.BackColor = Color.White;
            return true;
        }

        //private string generatenewID()
        //{
        //    var query = from x in db.Users
        //                select x;
        //    var dataCount = query.Count();
        //    var ID = string.Format("US{0:D3}", dataCount + 1);
        //}

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            //string id = generatenewID();
            //txtUsername.Text = id;
        }


    }   
}
