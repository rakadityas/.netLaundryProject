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
    public partial class ManageUserForm : Form
    {
        int functionNumber = 0;

        DatabaseEntities db;

        public ManageUserForm()
        {
            db = new DatabaseEntities();
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool correct = false;

            string m = cbRolename.Text;


            if (!ValidateUsername(txtUsername.Text))
            {
                return;
            }
            else if (!ValidatePassword(txtPassword.Text))
            {
                return;
            }
            else if (!ValidateEmail(txtEmail.Text))
            {
                return;
            }
            else if (!ValidateAddress(txtAddress.Text))
            {
                return;
            }
            else if (!ValidatePhone(txtPhoneNum.Text))
            {
                return;
            }
            else if (m == "")
            {
                MessageBox.Show("Please Choose the Role");
                return;
            }
            else
            {
                correct = true;
            }

            if (functionNumber == 1 && correct == true)
            {
                MessageBox.Show("User has been inserted!");

                User u = new User();

                u.UserName = txtUsername.Text;
                u.UserPassword = txtPassword.Text;
                u.UserEmail = txtEmail.Text;
                u.UserPhoneNumber = txtPhoneNum.Text;
                u.UserAddress = txtAddress.Text;

                if (m == "Member")
                {
                    u.RoleName = "Member";
                }
                else
                {
                    u.RoleName = "Admin";
                }

                //ID GENERATOR
                var data = from x in db.Users
                           select x;

                if (data.Count() != 0)
                {
                    String generator = string.Format("US{0}", (data.Count() + 1).ToString("000"));
                    u.UserID = generator;
                }
                else
                {
                    u.UserID = "US001";
                }

                db.Users.AddObject(u);
                db.SaveChanges();


                refreshTable();
                refreshUI();
            }
            else if (functionNumber == 2 && correct == true)
            {
                MessageBox.Show("User has been updated!");

                User u = (from x in db.Users
                          where x.UserID == txtUserID.Text
                          select x).First();

                u.UserName = txtUsername.Text;
                u.UserPassword = txtPassword.Text;
                u.UserEmail = txtEmail.Text;
                u.UserPhoneNumber = txtPhoneNum.Text;
                u.UserAddress = txtAddress.Text;
                u.RoleName = "Member";

                db.SaveChanges();

                refreshTable();
                refreshUI();
            }


        }



        private bool ValidateUsername(string Username)
        {
            if (Username.Equals(""))
            {
                MessageBox.Show("Username Must be Filled");
                return false;
            }



            for (int i = 0; i < Username.Length; i++)
            {
                if (char.IsDigit(Username.ToCharArray()[i]))
                {
                    MessageBox.Show("Username Must be alphabet");
                    return false;
                }
                return true;

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

        private void ManageUserForm_Load(object sender, EventArgs e)
        {
            cbRolename.Items.Add("Member");
            cbRolename.Items.Add("Admin");

            refreshTable();
            refreshUI();


        }

        private void refreshUI()
        {
            btnInsert.Enabled = true;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            txtUserID.Enabled = false;
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtEmail.Enabled = false;
            txtAddress.Enabled = false;
            txtPhoneNum.Enabled = false;
            cbRolename.Enabled = false;

            txtUserID.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPhoneNum.Text = "";
            cbRolename.Text = "";
        }

        private void refreshTable()
        {
            dataGridView1.DataSource = (from x in db.Users 
                                        where x.RoleName.Equals("Member")
                                        select new { x.UserID, x.UserName, x.UserPassword, x.UserEmail, x.UserAddress, x.UserPhoneNumber, x.RoleName });
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            functionNumber = 1;

            btnInsert.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            txtUserID.Enabled = false;
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtEmail.Enabled = true;
            txtAddress.Enabled = true;
            txtPhoneNum.Enabled = true;
            cbRolename.Enabled = true;

            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPhoneNum.Text = "";
            cbRolename.Text = "";


            //ID GENERATOR
           var data = from x in db.Users
                      select x;

           if (data.Count() != 0)
           {
               String generator = string.Format("US{0}", (data.Count() + 1).ToString("000"));
               txtUserID.Text = generator;
           }
           else
           {
               txtUserID.Text = "US001";
           }



        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Select user first");
            }
            else
            { 
            functionNumber = 2;

            btnInsert.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            txtUserID.Enabled = false;
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtEmail.Enabled = true;
            txtAddress.Enabled = true;
            txtPhoneNum.Enabled = true;
            cbRolename.Enabled = true;
            }



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (txtUserID.Text.Equals(""))
            {
                MessageBox.Show("Select user first");
            }
            else
            {
                btnInsert.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;

                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this user?", "DELETE", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    MessageBox.Show("Succesfully delete a user");

                    User uu = (from x in db.Users
                                   where x.UserID == txtUserID.Text
                                   select x).First();

                    db.Users.DeleteObject(uu);
                    db.SaveChanges();

                    refreshUI();
                    refreshTable();
                }
            }



        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = true;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            refreshTable();
            refreshUI();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUserID.Text = dataGridView1.Rows[e.RowIndex].Cells["UserID"].Value.ToString();
            txtUsername.Text = dataGridView1.Rows[e.RowIndex].Cells["UserName"].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells["UserPassword"].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells["UserEmail"].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["UserAddress"].Value.ToString();
            txtPhoneNum.Text = dataGridView1.Rows[e.RowIndex].Cells["UserPhoneNumber"].Value.ToString();
            cbRolename.Text = dataGridView1.Rows[e.RowIndex].Cells["RoleName"].Value.ToString();

        }

    }
}
