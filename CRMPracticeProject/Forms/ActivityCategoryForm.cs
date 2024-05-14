using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;
using System.Windows;

namespace CRMPracticeProject.Forms
{
    public partial class ActivityCategoryForm : Form
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


        public ActivityCategoryForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        int id;
        ActivityCategoryBLL bll = new ActivityCategoryBLL();
        MsBoxClass ms = new MsBoxClass();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        UserBLL ubll = new UserBLL();

        void FillDataGrid()
        {

            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["Id"].Visible = false;

        }

        void ClearTxtBoxes()
        {
            textBoxX2.Text = "";
        }
        private void ActivityCategoryForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X,Cursor.Position.Y);
            id = Convert.ToInt32((dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[0]).Value);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser,"Settings", 2))
            {
                ActivityCategory ac = new ActivityCategory();
                ac.CategoryName = textBoxX2.Text;

                if (label1.Text == "Add New Category")
                {
                    ms.MsShow("", bll.Create(ac), "", false, false);
                }
                else if (label1.Text == "Edit Category")
                {
                    ms.MsShow("", bll.Update(ac, id), "", false, false);

                    label1.Text = "Add New Category";
                }
            }
            else
            {
                ms.MsShow("Error", "You didn't approve customer or user or category yet", "", false, true);
            }


            FillDataGrid();
            ClearTxtBoxes();
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Settings", 3))
            {
                ActivityCategory ac = bll.Read(id);
                textBoxX2.Text = ac.CategoryName;
                label1.Text = "Edit Category";
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
            }

            FillDataGrid();
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Activities Part", 4))
            {
                DialogResult dr = ms.MsShow("Warning", "Do you wish to delete category?", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    bll.Delete(id);
                }
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
            }

            FillDataGrid();
            
        }
    }
}
