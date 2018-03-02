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
    public partial class HomeForm : Form
    {
       string role;

       User user;

        MasterLaundryForm Master;
        ManageUserForm ManageUser;
        ViewTransactionForm ViewTrans;
        ChangePasswordForm ChangePass;
        OrderForm Order;
        ReviewForm Review;

        public HomeForm(User u)
        {
            if (u.RoleName.Equals("Member"))
            {
                role = "Member";
            }
            else if (u.RoleName.Equals("Admin"))
            {
                role = "Admin";
            }

            user = u;

      

            InitializeComponent();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            if (role.Equals("Admin"))
            {
                menuOrder.Visible = false;
                menuGiveReview.Visible = false;
                menuManageProduct.Visible = true;

                menuManageUser.Visible = true;
                menuChangePassword.Visible = true;
                menuLogout.Visible = true;
            }
            else if (role.Equals("Member"))
            {
                menuOrder.Visible = true;
                menuGiveReview.Visible = true;
                menuManageProduct.Visible = false;

                menuManageUser.Visible = false;
                menuChangePassword.Visible = true;
                menuLogout.Visible = true;
            }
        }

        private void menuManageProduct_Click(object sender, EventArgs e)
        {
            if (Master == null || Master.IsDisposed)
            { 
                Master = new MasterLaundryForm(user);
                Master.MdiParent = this;
            }

            Master.Show();

    
        }

        private void menuManageUser_Click(object sender, EventArgs e)
        {
            if (ManageUser == null || ManageUser.IsDisposed)
            { 
             ManageUser = new ManageUserForm();
            ManageUser.MdiParent = this;
            }

            ManageUser.Show();

    
        }

        private void mainTransaction_Click(object sender, EventArgs e)
        {
            if (ViewTrans == null || ViewTrans.IsDisposed)
            {
                ViewTrans = new ViewTransactionForm(user);
                ViewTrans.MdiParent = this;
            }
                ViewTrans.Show();

        }

        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            if (ChangePass == null || ChangePass.IsDisposed)
            {
                ChangePass = new ChangePasswordForm(user);
                ChangePass.MdiParent = this;
            }
            ChangePass.Show();

 
        }

        private void menuOrder_Click(object sender, EventArgs e)
        {
            if (Order == null || Order.IsDisposed)
            {
                Order = new OrderForm(user);
                Order.MdiParent = this;
            }
            Order.Show();


        }

        private void menuGiveReview_Click(object sender, EventArgs e)
        {
            if (Review == null || Review.IsDisposed)
            {
                Review = new ReviewForm(user);
                Review.MdiParent = this;
            }
            Review.Show();


        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            LoginForm Login = new LoginForm();
            Login.Show();

            this.Close();
        }

        private void HomeForm_Leave(object sender, EventArgs e)
        {

        }




    }
}
