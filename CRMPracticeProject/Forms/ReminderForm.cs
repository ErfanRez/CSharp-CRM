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
    public partial class ReminderForm : Form
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

            public ReminderForm()
            {
                InitializeComponent();
                this.FormBorderStyle = FormBorderStyle.None;
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }
        
        
        int id;
        MsBoxClass ms = new MsBoxClass();
        ReminderBLL rbll = new ReminderBLL();
        UserBLL ubll = new UserBLL();
        User u = new User();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();

        void FillDataGrid()
        {

            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = rbll.Read();
            dataGridViewX1.Columns["ID"].Visible = false;
            label5.Text = rbll.ReminderCount();
            

        }

        void ClearTxtBoxes()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            richTextBoxEx1.Text = "";
            textBoxX2.Enabled = true;
        }

        private void ReminderForm_Load(object sender, EventArgs e)
        {
            ubll.ReadUserNames();
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach(var item in ubll.ReadUserNames())
            {
                names.Add(item);
                textBoxX2.AutoCompleteCustomSource = names;
            }
            
            FillDataGrid();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBoxX2.Enabled = false;
            u = ubll.SearchU(textBoxX2.Text);
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Reminders Part", 3))
            {
                if (textBoxX2.Enabled == false)
                {
                    Reminder r = new Reminder();
                    r.Title = textBoxX1.Text;
                    r.ReminderInfo = richTextBoxEx1.Text;
                    r.RegDate = DateTime.Now;
                    r.RemindDate = dateTimeInput1.Value;
                    ms.MsShow("", rbll.Create(r, u), "", false, false);
                }
                else
                {
                    ms.MsShow("Error", "User is Not Chosen!", "", false, true);
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
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[0].Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser,"Reminders Part", 3))
            {
                Reminder r = rbll.Read(id);
                textBoxX1.Text= r.Title;
                richTextBoxEx1.Text= r.ReminderInfo;
                dateTimeInput1.Value= r.RegDate;
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
            if (ubll.Access(w.LoggedInUser, "Reminders Part", 4))
            {
                DialogResult dr = ms.MsShow("","Do you wish to delete the data?","",true,false);
                if (dr == DialogResult.Yes)
                {
                    ms.MsShow("",rbll.Delete(id),"",false,false);
                }
                
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
            }
            
            FillDataGrid();
        }

        private void انجامشدهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.LoggedInUser, "Reminders Part", 3))
            {
                DialogResult dr = ms.MsShow("", "Do you wish to change reminder's status to Done?", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    ms.MsShow("", rbll.Done(id), "", false, false);
                }
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to use this part", "", false, true);
            }
            
            FillDataGrid();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reminder rs = rbll.ReminderInfo(id);
            ms.MsShow("Details", rs.ReminderInfo, "", false, false);
        }
    }
}
