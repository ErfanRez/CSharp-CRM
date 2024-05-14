using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BE;
using BLL;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors.DateTimeAdv;
using Stimulsoft.Report;

namespace CRMPracticeProject.Forms
{
    public partial class InvoiceForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,     // x-coordinate of upper-left corner
                int nTopRect,      // y-coordinate of upper-left corner
                int nRightRect,    // x-coordinate of lower-right corner
                int nBottomRect,   // y-coordinate of lower-right corner
                int nWidthEllipse, // width of ellipse
                int nHeightEllipse // height of ellipse
            );

        public InvoiceForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        CustomerBLL cbll = new CustomerBLL();
        Customer c = new Customer();
        ProductBLL pbll = new ProductBLL();
        Product p = new Product();
        List<Product> Products = new List<Product>();
        MsBoxClass ms = new MsBoxClass();
        InvoiceBLL ibll = new InvoiceBLL();
        UserBLL ubll = new UserBLL();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        int id;

        void FillDataGrid1()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = Products;
            dataGridViewX2.Columns["Id"].Visible = false;
            dataGridViewX2.Columns["Stock"].Visible = false;
            dataGridViewX2.Columns["DeleteStatus"].Visible = false;
            dataGridViewX2.Columns["RegDate"].Visible = false;
            dataGridViewX2.Columns["Name"].HeaderText = "Product's Name";
            dataGridViewX2.Columns["Price"].HeaderText = "Price";
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = ibll.Read();
            dataGridViewX1.Columns["ID"].Visible = false;
            label12.Text = ibll.Count();

        }
        void ClearTxtBoxes()
        {
            label1.Text = "";
            label4.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";
            label14.Text = "";
            dataGridViewX2.DataSource = null;
        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            label14.Text = DateTime.Now.Date.ToString("yyyy/MM/dd");
            
            AutoCompleteStringCollection phone = new AutoCompleteStringCollection();
            foreach (var item in cbll.ReadCustomerPhone())
            {
                phone.Add(item);
            }
            textBoxX2.AutoCompleteCustomSource = phone;

            AutoCompleteStringCollection PNames = new AutoCompleteStringCollection();
            foreach (var item in pbll.ReadProductNames())
            {
                PNames.Add(item);
            }
            textBoxX4.AutoCompleteCustomSource = PNames;

            FillDataGrid1();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            c = cbll.SearchPhone(textBoxX2.Text);
            textBoxX2.Enabled = false;
            label1.Text = c.Name;
            label4.Text = c.Phone;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            p = pbll.SearchProduct(textBoxX4.Text);
            if (p.Stock >= 1)
            {
                Products.Add(p);
            }
            else
            {
                ms.MsShow("Error","Product's Stock Is Not Enough!","",false,true);
            }
            FillDataGrid1();
            string s = p.Name + " With Value " + p.Price.ToString("N0") + " Dollars ";
            listBox1.Items.Add(s);
            double sum = 0;
            foreach (var item in Products)
            {
                sum += item.Price;
            }
            label19.Text = sum.ToString("N0");
            label20.Text = (sum - Convert.ToDouble(textBoxX1.Text)).ToString("N0");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            if (ubll.Access(w.LoggedInUser, "Invoices Part", 2))
            {
                if ((dataGridViewX2.Rows.Count != 0) && (textBoxX2.Enabled == false))
                {



                    Invoice i = new Invoice();
                    i.RegDate = DateTime.Now;

                    if (checkBox1.Checked)
                    {
                        i.IsCheckedOut = true;
                        i.CheckOutDate = DateTime.Now;
                    }
                    else
                    {
                        i.IsCheckedOut = false;
                    }
                    DialogResult dr = ms.MsShow("Information", ibll.Create(i, c, Products) + " Do you wish to print the invoice? ", "", true, false);
                    if (dr == DialogResult.Yes)
                    {
                        StiReport sti = new StiReport();
                        sti.Load(@"C:\Users\ERFAN\Desktop\CRM-Source Files\Invoice.mrt"); //load the stimulsoft report file
                        sti.Dictionary.Variables["InvoiceNum"].Value = ibll.ReadInvoiceNum(); //filling the variables from the app
                        sti.Dictionary.Variables["Date"].Value = label14.Text;
                        sti.Dictionary.Variables["CustomerName"].Value = label1.Text;
                        sti.Dictionary.Variables["Phone"].Value = label4.Text;
                        sti.RegBusinessObject("ProductBO", Products); //filling the Bussiness Object from Product list of app
                        sti.Render();
                        sti.Show();
                    }

                }
                else
                {
                    ms.MsShow("Error", "Customer And Product Are Not Chosen!", "", false, true);
                }
            }
            else
            {
                ms.MsShow("Error", "You didn't approve customer or user or category yet", "", false, true);
            }

            FillDataGrid1();
            ClearTxtBoxes();


        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[0].Value);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Invoices Part", 4))
            {
                DialogResult dr = ms.MsShow("Warning", "Do you wish to delete the Invoice? ", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    ms.MsShow("Details", ibll.Delete(id), "", false, false);

                }
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
            }

            FillDataGrid1();
        }

    }
}
