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
    public partial class CustomerForm : Form
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

        public CustomerForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        CustomerBLL bll = new CustomerBLL();
        MsBoxClass MsBoxClass = new MsBoxClass();
        UserBLL ubll = new UserBLL();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();

        int id;
        int index;


        void FillDataGrid()
        {
            
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["ID"].Visible = false;
            label12.Text = bll.CustomerCount();
            
        }

        void ClearTxtBoxes()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX3.Text = "";
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            c.Name = textBoxX1.Text;
            c.Phone = textBoxX2.Text;
            c.RegDate = DateTime.Now;
            if (label1.Text == "Add Data")
            {
                if (ubll.Access(w.LoggedInUser, "Customers Part", 2))
                {
                           
                    MsBoxClass.MsShow("Info", bll.Create(c), "", false, false);
                }
                else
                {
                    MsBoxClass.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
                    
                }         
            }
            else
            {
                MsBoxClass.MsShow("Info", bll.Update(c,id), "", false, false);

                label1.Text = "Add Data";
            }
            FillDataGrid();
            ClearTxtBoxes();
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked && checkBox2.Checked || (!checkBox1.Checked && !checkBox2.Checked) )
            {
                index = 0;
            }
            else if (checkBox1.Checked && !checkBox2.Checked)
            {
                index = 1;
            }
            else if (checkBox2.Checked && !checkBox1.Checked)
            {
                index = 2;
            }
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Search(textBoxX3.Text, index);

        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32((dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[0]).Value);
        }
        private void ویرایشToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Customers Part", 3))
            {

                Customer c = bll.Read(id);
                textBoxX1.Text = c.Name;
                textBoxX2.Text = c.Phone;
                label1.Text = "Edit Data";
            }
            else
            {
                MsBoxClass.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);

            }
            
        }

        private void حذفToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Customers Part", 4))
            {

                DialogResult dr = MsBoxClass.MsShow("Warning", "Are you sure you want to delete selected data?", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    bll.Delete(id);
                }
                FillDataGrid();
            }
            else
            {
                MsBoxClass.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);

            }
            
        }

    }
}
