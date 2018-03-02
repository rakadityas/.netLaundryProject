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
    public partial class OrderForm : Form
    {
        User user;
        DatabaseEntities db;
        string key;
        int cekH = 0;



        public OrderForm(User u)
        {
            user = u;
            db = new DatabaseEntities();
            InitializeComponent();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            refreshTableHeader();
            refreshTableDetail();

            txtLaundryID.Enabled = false;
            txtLaundryName.Enabled = false;
            txtPrice.Enabled = false;
            txtQuantity.Enabled = false;
            txtProductID.Enabled = false;
            txtGrandTotal.Enabled = false;
            txtTransactionID.Enabled = false;
            txtUserID.Enabled = false;
            numericUpDown1.Enabled = true;

            IDGenerator();

            txtUserID.Text = user.UserID;
            txtQuantity.Text = "0";
            txtGrandTotal.Text = "0";




            grandTotalGenerator();

        }

        private void refreshTableHeader()
        {
            dataGridView1.DataSource = (from x in db.PriceLists select new { x.ProductID, x.ProductName,x.ProductPrice});
            
        }

        private void refreshTableDetail()
        {


            dataGridView2.DataSource = (from x in db.HeaderTransactions
                                        join y in db.DetailTransactions on x.TransactionID equals y.TransactionID
                                        join z in db.PriceLists on y.ProductID equals z.ProductID
                                        where x.Status.Equals("Pending") && x.UserID == user.UserID
                                        select new { z.ProductID, z.ProductName, y.Quantity,  TotalPrice = y.Quantity * z.ProductPrice });


            grandTotalGenerator();
        }

        private void IDGenerator()
        {
            var cekIdExistance = (from x in db.HeaderTransactions
                                  join y in db.DetailTransactions on x.TransactionID equals y.TransactionID
                                  join z in db.PriceLists on y.ProductID equals z.ProductID
                                  where x.Status.Equals("Pending") && x.UserID == user.UserID
                                  select x.TransactionID);

            if (cekIdExistance.Count() == 0)
            {


                var data = from x in db.HeaderTransactions
                           select x;

                if (data.Count() != 0)
                {


                    String generator = string.Format("HT{0}", (data.Count() + 1).ToString("000"));
                    txtTransactionID.Text = generator;


                }
                else
                {

                    txtTransactionID.Text = "HT001";
                }
            }
            else
            {
                var data = (from x in db.HeaderTransactions
                            where x.Status.Equals("Pending") && x.UserID == user.UserID
                            select x.TransactionID).First().ToString();
                txtTransactionID.Text = data;

            }
        }

        private void grandTotalGenerator()
        {
            int QTY = 0,  PRICE = 0, quantity = 0, GTOTAL = 0;

            key = txtTransactionID.Text;

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



            for (int i = 0; i < qty.Count(); i++)
            {
                QTY = Int32.Parse(qty.ToArray()[i].Value.ToString());
                PRICE = Int32.Parse(price.ToArray()[i].Value.ToString());

                GTOTAL = GTOTAL + (QTY * PRICE);

                quantity = quantity + QTY;

            }

            txtGrandTotal.Text = GTOTAL.ToString();

        }

        private void btnAddtoCart_Click(object sender, EventArgs e)
        {

            if (txtLaundryID.Text.Equals(""))
            {
                MessageBox.Show("Please Choose Laundry First");
            }
            else if (numericUpDown1.Value.Equals(0))
            {
                MessageBox.Show("Please Fill the Quantity More Than 0");
            }
            else
            {
                key = txtLaundryID.Text;

                 var data  = (from x in db.HeaderTransactions
                              join y in db.DetailTransactions on x.TransactionID equals y.TransactionID
                              join z in db.PriceLists on y.ProductID equals z.ProductID
                              where x.Status.Equals("Pending") && z.ProductID==key && x.UserID == user.UserID
                              select new { z.ProductID, z.ProductName, y.Quantity, TotalPrice = y.Quantity * z.ProductPrice });



                 if (data.Count() == 0)
                 {

                     DetailTransaction d = new DetailTransaction();

                     var cekHeaderExist = (from x in db.HeaderTransactions
                                           where x.Status.Equals("Pending") && x.UserID == user.UserID
                                           select x).ToList();

                     cekH = cekHeaderExist.Count();

   

                     if (cekHeaderExist.Count() == 0)
                     {
                         HeaderTransaction h = new HeaderTransaction();

                         h.TransactionID = txtTransactionID.Text;
                         h.UserID = txtUserID.Text;
                         h.Status = "Pending";

                         db.HeaderTransactions.AddObject(h);
                         db.SaveChanges();
                     }

                     d.TransactionID = txtTransactionID.Text;
                     d.ProductID = txtLaundryID.Text;
                     d.Quantity = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
                     d.Price = Int32.Parse( txtPrice.Text);


                     
                     db.DetailTransactions.AddObject(d);
                     db.SaveChanges();

                     refreshTableDetail();


                 }
                 else
                 {

                     DetailTransaction d = (from x in db.DetailTransactions join y in db.HeaderTransactions on x.TransactionID equals y.TransactionID
                                            where x.ProductID == key && y.Status == "Pending" && y.UserID == user.UserID
                                            select x).First();

                     int qtt = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));

                     d.Quantity = d.Quantity + qtt;

                     db.SaveChanges();
                     refreshTableDetail();


                 }
                              
            }

            grandTotalGenerator();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtProductID.Text == "")
            {
                MessageBox.Show("Please Choose the Cart to be Deleted First!");
            }
            else
            {
                DetailTransaction dd = (from x in db.DetailTransactions join y in db.HeaderTransactions on x.TransactionID equals y.TransactionID
                                       where x.ProductID == txtProductID.Text && y.Status == "Pending" && y.UserID == user.UserID
                                       select x).First();

                db.DetailTransactions.DeleteObject(dd);
                db.SaveChanges();

                refreshTableDetail();

            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Waiting");

            HeaderTransaction h = (from x in db.HeaderTransactions
                                   join y in db.DetailTransactions
                                   on x.TransactionID equals y.TransactionID
                                   where x.TransactionID == txtTransactionID.Text && x.Status == "Pending"
                                   select x).First();
            h.Status = "Waiting";

            db.SaveChanges();

            refreshTableDetail();

            txtGrandTotal.Text = "0";

            IDGenerator();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {   
            txtLaundryID.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
            txtLaundryName.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
            txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductPrice"].Value.ToString();

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            txtProductID.Text = dataGridView2.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
            txtQuantity.Text = dataGridView2.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();




        }




    }
}
