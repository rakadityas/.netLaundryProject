namespace Project
{
    partial class HomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainMenuProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGiveReview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManageProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.mainProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManageUser = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuProduct,
            this.mainTransaction,
            this.mainProfile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(940, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainMenuProduct
            // 
            this.mainMenuProduct.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOrder,
            this.menuGiveReview,
            this.menuManageProduct});
            this.mainMenuProduct.Name = "mainMenuProduct";
            this.mainMenuProduct.Size = new System.Drawing.Size(61, 20);
            this.mainMenuProduct.Text = "Product";
            // 
            // menuOrder
            // 
            this.menuOrder.Name = "menuOrder";
            this.menuOrder.Size = new System.Drawing.Size(162, 22);
            this.menuOrder.Text = "Order";
            this.menuOrder.Click += new System.EventHandler(this.menuOrder_Click);
            // 
            // menuGiveReview
            // 
            this.menuGiveReview.Name = "menuGiveReview";
            this.menuGiveReview.Size = new System.Drawing.Size(162, 22);
            this.menuGiveReview.Text = "Give Review";
            this.menuGiveReview.Click += new System.EventHandler(this.menuGiveReview_Click);
            // 
            // menuManageProduct
            // 
            this.menuManageProduct.Name = "menuManageProduct";
            this.menuManageProduct.Size = new System.Drawing.Size(162, 22);
            this.menuManageProduct.Text = "Manage Product";
            this.menuManageProduct.Click += new System.EventHandler(this.menuManageProduct_Click);
            // 
            // mainTransaction
            // 
            this.mainTransaction.Name = "mainTransaction";
            this.mainTransaction.Size = new System.Drawing.Size(80, 20);
            this.mainTransaction.Text = "Transaction";
            this.mainTransaction.Click += new System.EventHandler(this.mainTransaction_Click);
            // 
            // mainProfile
            // 
            this.mainProfile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManageUser,
            this.menuChangePassword,
            this.menuLogout});
            this.mainProfile.Name = "mainProfile";
            this.mainProfile.Size = new System.Drawing.Size(53, 20);
            this.mainProfile.Text = "Profile";
            // 
            // menuManageUser
            // 
            this.menuManageUser.Name = "menuManageUser";
            this.menuManageUser.Size = new System.Drawing.Size(168, 22);
            this.menuManageUser.Text = "Manage User";
            this.menuManageUser.Click += new System.EventHandler(this.menuManageUser_Click);
            // 
            // menuChangePassword
            // 
            this.menuChangePassword.Name = "menuChangePassword";
            this.menuChangePassword.Size = new System.Drawing.Size(168, 22);
            this.menuChangePassword.Text = "Change Password";
            this.menuChangePassword.Click += new System.EventHandler(this.menuChangePassword_Click);
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(168, 22);
            this.menuLogout.Text = "Log Out";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(940, 418);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HomeForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HomeForm_Load);
            this.Leave += new System.EventHandler(this.HomeForm_Leave);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuProduct;
        private System.Windows.Forms.ToolStripMenuItem mainTransaction;
        private System.Windows.Forms.ToolStripMenuItem mainProfile;
        private System.Windows.Forms.ToolStripMenuItem menuOrder;
        private System.Windows.Forms.ToolStripMenuItem menuGiveReview;
        private System.Windows.Forms.ToolStripMenuItem menuManageProduct;
        private System.Windows.Forms.ToolStripMenuItem menuManageUser;
        private System.Windows.Forms.ToolStripMenuItem menuChangePassword;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
    }
}