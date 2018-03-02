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
    public partial class MasterLaundryForm : Form
    {
        DatabaseEntities db;

        User user;


        private int functionNumber;

        public MasterLaundryForm(User u)
        {
            db = new DatabaseEntities();

            user = u;

            InitializeComponent();
        }

        private void MasterLaundryForm_Load(object sender, EventArgs e)
        {
            refreshTable();

            refreshUI();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLaundryID.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
            txtLaundryName.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
            txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductPrice"].Value.ToString();
        }


        private void btnInsert_Click(object sender, EventArgs e)
        {

            
                txtLaundryID.Enabled = false;
                txtLaundryName.Enabled = true;
                txtPrice.Enabled = true;

                btnInsert.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                btnSave.Enabled = true;
                btnCancel.Enabled = true;

                txtLaundryID.Text = "";
                txtLaundryName.Text = "";
                txtPrice.Text = "";

            
                functionNumber = 1;


                //ID GENERATOR
                var data = from x in db.PriceLists
                           select x;

                if (data.Count() != 0)
                {
                    String generator = string.Format("PD{0}", (data.Count() + 1).ToString("000"));
                    txtLaundryID.Text = generator;
                }
                else
                {
                    txtLaundryID.Text = "PD001";
                }
        }

        private void refreshTable()
        {
            dataGridView1.DataSource = (from x in db.PriceLists select new { x.ProductID, x.ProductName, x.ProductPrice });
        }

        private void refreshUI()
        {
            txtLaundryID.Enabled = false;
            txtLaundryName.Enabled = false;
            txtPrice.Enabled = false;

            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            btnInsert.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            txtLaundryID.Text = "";
            txtLaundryName.Text = "";
            txtPrice.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool correct = false;

            //validation
            if (txtLaundryName.Text == "")
            {
                MessageBox.Show("Please Fill the Laundry Name Field");
            } 
            else if (validateName(txtLaundryName.Text))
            {
                MessageBox.Show("Laundry Name must be alphabet only");
            }
            else if (txtPrice.Text == "")
            {
                MessageBox.Show("Please Fill the Laundry Price Field");
            }
            else if (!validatePrice(txtPrice.Text))
            {
                MessageBox.Show("Laundry Price must be numeric");
            }
            else
            {
                correct = true;
            }


            if (functionNumber == 1 && correct == true)
            {
                MessageBox.Show("Laundry has been inserted!");

                string id = txtLaundryID.Text;
                string name = txtLaundryName.Text;
                int price = Int32.Parse(txtPrice.Text);

                PriceList p = new PriceList();
                p.ProductID = id;
                p.ProductName = name;
                p.ProductPrice = price;

                db.PriceLists.AddObject(p);
                db.SaveChanges();

                refreshUI();
                refreshTable();
            }
            else if (functionNumber == 2 && correct == true)
            {
                MessageBox.Show("Laundry has been updated!");

                string id = txtLaundryID.Text;
                string name = txtLaundryName.Text;
                int price = Int32.Parse(txtPrice.Text);

                PriceList p = (from x in db.PriceLists
                               where x.ProductID == txtLaundryID.Text
                               select x).First();

                p.ProductName = name;
                p.ProductPrice = price;

                db.SaveChanges();

                refreshUI();
                refreshTable();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            functionNumber = 1;

            if (txtLaundryName.Text.Equals(""))
            {
                MessageBox.Show("Select Product First");
            }
            else
            {
                functionNumber = 2;
                txtLaundryID.Enabled = false;
                txtLaundryName.Enabled = true;
                txtPrice.Enabled = true;

                btnInsert.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLaundryID.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
            txtLaundryName.Text =dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
            txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductPrice"].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtLaundryName.Text.Equals(""))
            {
                MessageBox.Show("Select Product First");
            }
            else
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this product?", "DELETE", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    MessageBox.Show("Succesfully delete a product");

                    PriceList p = (from x in db.PriceLists
                                   where x.ProductID == txtLaundryID.Text
                                   select x).First();

                    db.PriceLists.DeleteObject(p);
                    db.SaveChanges();

                    refreshUI();
                    refreshTable();
                }
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            refreshTable();
            refreshUI();
        }

        public bool validatePrice(String price)
        {
            for (int i = 0; i < price.Length; i++)
            {
                if (!char.IsDigit(price.ToCharArray()[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public bool validateName(String name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                if (!char.IsLetter(name.ToCharArray()[i]))
                {
                    return true;
                }
            }
            return false;
        }


    }

    
}
