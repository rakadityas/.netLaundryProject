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
    
    public partial class ReviewForm : Form
    {
        User user;
        DatabaseEntities db;
        string keyProductID;

        public ReviewForm(User u)
        {
            user = u;
            db = new DatabaseEntities();
            InitializeComponent();
        }

        private void ReviewForm_Load(object sender, EventArgs e)
        {
            refreshTable();
            txtReviewID.Enabled = false;

            var data = from x in db.Reviews
                       select x;

            if (data.Count() != 0)
            {
                String generator = string.Format("RV{0}", (data.Count() + 1).ToString("000"));
                txtReviewID.Text = generator;
            }
            else
            {
                txtReviewID.Text = "RV001";
            }



        }

        private void refreshTable()
        {
            dataGridView1.DataSource = (from x in db.PriceLists select new { x.ProductID, x.ProductName, x.ProductPrice });

        }

        private void refreshTableBawah()
        {
            dataGridView2.DataSource = (from x in db.Reviews select new { x.ReviewID, x.Reviews });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView2.DataSource==null)
            {
                MessageBox.Show("Please Choose Product First");
            }
            else if (txtBoxReview.Text.Equals(""))
            {
                MessageBox.Show("Review Must be Filled");
            }
            else
            {
              

                Review review = new Review();

                review.ReviewID = txtReviewID.Text;
                review.Reviews = txtBoxReview.Text;
                review.ProductID = keyProductID;
                review.UserID = user.UserID;

                db.Reviews.AddObject(review);
                db.SaveChanges();

                txtBoxReview.Text = "";

                //ID GENERATOR
                var data = from x in db.Reviews
                           select x;

                if (data.Count() != 0)
                {
                    String generator = string.Format("RV{0}", (data.Count() + 1).ToString("000"));
                    txtReviewID.Text = generator;
                }
             
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            keyProductID = dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
            string keyUserID = user.UserID;

            dataGridView2.DataSource = (from x in db.Reviews
                                        where x.ProductID == keyProductID
                                        select new { x.ReviewID, x.Reviews });
        }
    }
}
