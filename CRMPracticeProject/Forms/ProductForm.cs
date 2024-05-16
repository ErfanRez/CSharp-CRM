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

namespace CRMPracticeProject
{
    public partial class ProductForm : Form
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

        public ProductForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        int id;
        ProductBLL pbll = new ProductBLL();
        MsBoxClass ms = new MsBoxClass();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        UserBLL ubll = new UserBLL();

        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = pbll.Read();
            dataGridViewX1.Columns["ID"].Visible = false;
            label12.Text = pbll.ProductCount();
        }

        void ClearTxtBoxes()
        {
            textBoxX2.Text = "";
            textBoxX1.Text = "";
            textBoxX4.Text = "";
        }
        
        private void ProductForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Products Part", 2))
            {
                Product p = new Product();
                p.Name = textBoxX2.Text;
                p.Price = Convert.ToDouble(textBoxX1.Text);
                p.Stock = Convert.ToInt32(textBoxX4.Text);
                p.RegDate = DateTime.Now;
                if (label1.Text == "Add Product")
                {
                    ms.MsShow("", pbll.Create(p), "", false, false);
                }
                else
                {
                    ms.MsShow("", pbll.Update(p, id), "", false, false);

                    label1.Text = "Add Product";
                }
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
            }

            FillDataGrid();
            ClearTxtBoxes();
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32((dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[0]).Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser,"Products Part", 3))
            {
                Product p = pbll.Read(id);
                textBoxX2.Text = p.Name;
                textBoxX1.Text = Convert.ToString(p.Price);
                textBoxX4.Text = Convert.ToString(p.Stock);
                label1.Text = "Edit Data";
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
            }

            FillDataGrid();

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Products Part", 4))
            {
                DialogResult dr = ms.MsShow("Warning", "Do you wish to delete product?", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    pbll.Delete(id);
                }
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
            }
            
            FillDataGrid();
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = pbll.Search(textBoxX3.Text);
            dataGridViewX1.Columns["ID"].Visible = false;
        }

    }
}
