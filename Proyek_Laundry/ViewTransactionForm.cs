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
    public partial class ViewTransactionForm : Form
    {
        User user;
        HeaderTransaction h;
        string role;
        string key;
        
        
        DatabaseEntities db;
        
        public ViewTransactionForm(User u)
        {
            user = u;

            role = user.RoleName;
            db = new DatabaseEntities();
            InitializeComponent();
        }

        //MEMBER MEMBER MEMBER
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void refreshTableHeader()
        {
            if (role == "Admin")
            {
                dataGridView1.DataSource = (from x in db.HeaderTransactions select new { x.TransactionID, x.UserID, x.Status });
            }
            else if (role == "Member")
            {
                dataGridView1.DataSource = (from x in db.HeaderTransactions
                                            where x.UserID == user.UserID
                                            select new { x.TransactionID, x.Status });
            }
        }



        private void ViewTransactionForm_Load(object sender, EventArgs e)
        {
            refreshTableHeader();

            txtGrandTotal.Enabled = false;
            txtTotalQuantity.Enabled = false;


            if (role == "Admin")
            {
               
                btnUpdateStatus.Visible = true;
            }
            if (role == "Member")
            {
                btnUpdateStatus.Visible = false;
            }
        }

       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            int QTY = 0,  PRICE = 0, quantity = 0, GTOTAL = 0;

            key = dataGridView1.Rows[e.RowIndex].Cells["TransactionID"].Value.ToString();

            

            dataGridView2.DataSource = (from x in db.DetailTransactions
                                        join y in db.PriceLists
                                        on x.ProductID equals y.ProductID
                                        where x.TransactionID == key
                                        select new { y.ProductName, x.Quantity, x.Price });
            
            //GRAND TOTAL GENERATOR
            var qty = (from x in db.DetailTransactions
                        join y in db.PriceLists
                                          on x.ProductID equals y.ProductID
                        where x.TransactionID == key
                        select x.Quantity);

            var price = (from x in db.DetailTransactions
                        join y in db.PriceLists
                                          on x.ProductID equals y.ProductID
                        where x.TransactionID == key
                        select x.Price);

         
            
            for (int i = 0; i< qty.Count(); i++)
            {
                QTY = Int32.Parse(qty.ToArray()[i].Value.ToString());
                PRICE = Int32.Parse(price.ToArray()[i].Value.ToString());

                GTOTAL = GTOTAL+(QTY * PRICE);

                quantity = quantity + QTY;
            }

            txtTotalQuantity.Text = quantity.ToString();
            txtGrandTotal.Text = GTOTAL.ToString();
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (txtGrandTotal.Text.Equals(""))
            {
                MessageBox.Show("Please Choose Transaction Header Data First");
            }
            else
            {
                var data = from x in db.HeaderTransactions
                           where x.TransactionID==key
                           select x;

                h = data.First();

                    if (h.Status.Equals("Waiting"))
                    {
                        h.Status="Washing";

                        db.SaveChanges();
                    }
                    else if (h.Status.Equals("Washing"))
                    {
                        h.Status="Finished";

                        db.SaveChanges();
                    }
            }

            refreshTableHeader();
        }


    }
}
