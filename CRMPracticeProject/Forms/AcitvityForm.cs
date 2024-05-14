using BE;
using BLL;
using DevComponents.DotNetBar.Controls;
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

namespace CRMPracticeProject.Forms
{
    public partial class ActivityForm : Form
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
       
        
        public ActivityForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }


        int id;
        Customer c = new Customer();
        User u = new User();
        MsBoxClass ms = new MsBoxClass();
        ActivityCategory ac = new ActivityCategory();
        ActivityCategoryBLL AcBll = new ActivityCategoryBLL();
        ReminderBLL rbll = new ReminderBLL();
        ActivityBLL abll = new ActivityBLL();
        CustomerBLL cbll = new CustomerBLL();
        UserBLL ubll = new UserBLL();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();


        void FillDataGrid()
        {

            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = abll.Read();
            dataGridViewX1.Columns["ID"].Visible = false;
            label5.Text = abll.DoneCount();
            textBoxX1.Enabled = true;
            textBoxX2.Enabled = true;
            textBoxX5.Enabled = true;

        }

        void ClearTxtBoxes()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX5.Text = "";
            textBoxX4.Text = "";
            richTextBoxEx1.Text = "Enter Activity Details";
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection phone = new AutoCompleteStringCollection();

            foreach (var item in cbll.ReadCustomerPhone())
            {
                phone.Add(item);
            }
            textBoxX2.AutoCompleteCustomSource = phone;

            AutoCompleteStringCollection username = new AutoCompleteStringCollection();

            foreach (var item in ubll.ReadUserNames())
            {
                username.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = username;

            AutoCompleteStringCollection ACNames = new AutoCompleteStringCollection();

            foreach (var item in AcBll.ReadCategory())
            {
                ACNames.Add(item);
            }
            textBoxX5.AutoCompleteCustomSource = ACNames;

            FillDataGrid();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            c = cbll.SearchPhone(textBoxX2.Text);
            textBoxX2.Enabled = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            u = ubll.SearchU(textBoxX1.Text);
            textBoxX1.Enabled = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ac = AcBll.SearchCategory(textBoxX5.Text);
            textBoxX5.Enabled = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                dateTimeInput1.Enabled = true;
            }
            else if (!checkBox3.Checked)
            {
                dateTimeInput1.Enabled = false;
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (ubll.Access(w.LoggedInUser, "Activities Part", 2))
            {
                if (textBoxX1.Enabled == false && textBoxX2.Enabled == false && textBoxX5.Enabled == false)
                {
                    Activity a = new Activity();
                    a.Title = textBoxX4.Text;
                    a.Info = richTextBoxEx1.Text;
                    a.RegDate = DateTime.Now;
                    ms.MsShow("", abll.Create(a, u, c, ac), "", false, false);

                    if (checkBox3.Checked)
                    {
                        Reminder r = new Reminder();
                        r.Title = textBoxX4.Text;
                        r.ReminderInfo = richTextBoxEx1.Text;
                        r.RegDate = DateTime.Now;
                        r.RemindDate = dateTimeInput1.Value;
                        ms.MsShow("", rbll.Create(r, u), "", false, false);
                    }
                }
                else
                {
                    textBoxX1.Enabled = true;
                    textBoxX2.Enabled = true;
                    textBoxX5.Enabled = true;
                    ms.MsShow("Error", "You Haven't approve customer or user or category yet!", "", false, true);
                }
            

                FillDataGrid();
                ClearTxtBoxes();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part!", "", false, true);   
            }
            

            
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[0].Value);
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Activities Part", 4))
            {
                DialogResult dr = ms.MsShow("Warning", "Do you wish to delete the chosen activity", "", true, false);
                if (dr == DialogResult.Yes)
                {
                   
                    ms.MsShow("Details", abll.Delete(id), "", false, false);

                }
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
            }

            FillDataGrid();
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activity a = abll.Read(id);
            ms.MsShow ("Details",a.Info,"",false,false);
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            if ( checkBox1.Checked && checkBox2.Checked || (!checkBox1.Checked && !checkBox2.Checked) )
            {
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = abll.SearchActivity(textBoxX3.Text);
                dataGridViewX1.Columns["ID"].Visible = false;
            }
            else if (checkBox1.Checked && !checkBox2.Checked)
            {
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = ubll.SearchUser(textBoxX3.Text);
                dataGridViewX1.Columns["ID"].Visible = false;
            }
            else if (checkBox2.Checked && !checkBox1.Checked)
            {
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = abll.SearchTitle(textBoxX3.Text);
                dataGridViewX1.Columns["ID"].Visible = false;
            }
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Activities Part", 3))
            {
                DialogResult dr = ms.MsShow("", "Do you wish to change activitie's status to Done?", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    ms.MsShow("", abll.Done(id), "", false, false);
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
