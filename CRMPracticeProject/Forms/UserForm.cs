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
using BE;
using BLL;
using System.IO;

namespace CRMPracticeProject.Forms
{
    public partial class UserForm : Form
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

        public UserForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }


        UserBLL bll = new UserBLL();
        OpenFileDialog ofd = new OpenFileDialog();
        Image img;
        int id;
        UserTypeBLL utbll = new UserTypeBLL();
        MsBoxClass ms = new MsBoxClass();
        UserAccessRole FillAccessRole(string Section, bool CanEnter, bool CanCreate, bool CanUpdate, bool CanDelete)
        {
            UserAccessRole uar = new UserAccessRole();
            uar.Section = Section;
            uar.CanEnter = CanEnter;
            uar.CanCreate = CanCreate;
            uar.CanUpdate = CanUpdate;
            uar.CanDelete = CanDelete;
            return uar;
        }

        string SavePic(string UserName)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\UserPics\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string PicName = UserName + ".JPG";
            try
            {
                string PicPath = ofd.FileName;
                File.Copy(PicPath, path + PicName, true);
            }
            catch (Exception e)
            {
                MessageBox.Show("System is not able to save the picture" + e.Message);
            }
            return path + PicName;
        }

        void FillDataGrid()
        {

            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["ID"].Visible = false;
            label6.Text = bll.UsersCount();

        }

        void ClearTxtBoxes()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX3.Text = "";
            textBoxX4.Text = "";

        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            comboBoxEx1.DataSource = utbll.ReadUTNames();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = "Choose User's Picture";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                img = Image.FromFile(ofd.FileName);
                pictureBox2.Image = img;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Name = textBoxX1.Text;
            u.Username = textBoxX2.Text;
            u.RegDate = DateTime.Now;
            UserType ut = utbll.ReadN(comboBoxEx1.Text);

            if (textBoxX3.Text == textBoxX4.Text)
            {
                u.Password = textBoxX4.Text;
            }
            else
            {
                MessageBox.Show("The Repeated Password Is Not Correct");
            }

            if (label1.Text == "Add User")
            {
                u.Pic = SavePic(textBoxX2.Text);
                MessageBox.Show(bll.Create(u,ut));
            }
            else
            {
                u.Pic = SavePic(textBoxX2.Text);
                MessageBox.Show(bll.Update(u, id));

                label1.Text = "Add User";
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
            User u = bll.Read(id);
            textBoxX1.Text = u.Name;
            textBoxX2.Text = u.Username;
            pictureBox2.Image = Image.FromFile(u.Pic);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Text = "Edit Data";
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete user?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                bll.Delete(id);
            }
            FillDataGrid();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox8.Checked = true;
                checkBox12.Checked = true;
                checkBox16.Checked = true;
                checkBox20.Checked = true;
                checkBox24.Checked = true;
                checkBox28.Checked = true;
                checkBox32.Checked = true;
                checkBox36.Checked = true;
                checkBox40.Checked = true;
            }
            else
            {
                checkBox8.Checked = false;
                checkBox12.Checked = false;
                checkBox16.Checked = false;
                checkBox20.Checked = false;
                checkBox24.Checked = false;
                checkBox28.Checked = false;
                checkBox32.Checked = false;
                checkBox36.Checked = false;
                checkBox40.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox7.Checked = true;
                checkBox11.Checked = true;
                checkBox15.Checked = true;
                checkBox19.Checked = true;
                checkBox23.Checked = true;
                checkBox27.Checked = true;
                checkBox31.Checked = true;
                checkBox35.Checked = true;
                checkBox39.Checked = true;
            }
            else
            {
                checkBox7.Checked = false;
                checkBox11.Checked = false;
                checkBox15.Checked = false;
                checkBox19.Checked = false;
                checkBox23.Checked = false;
                checkBox27.Checked = false;
                checkBox31.Checked = false;
                checkBox35.Checked = false;
                checkBox39.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox6.Checked = true;
                checkBox10.Checked = true;
                checkBox14.Checked = true;
                checkBox18.Checked = true;
                checkBox22.Checked = true;
                checkBox26.Checked = true;
                checkBox30.Checked = true;
                checkBox34.Checked = true;
                checkBox38.Checked = true;
            }
            else
            {
                checkBox6.Checked = false;
                checkBox10.Checked = false;
                checkBox14.Checked = false;
                checkBox18.Checked = false;
                checkBox22.Checked = false;
                checkBox26.Checked = false;
                checkBox30.Checked = false;
                checkBox34.Checked = false;
                checkBox38.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox5.Checked = true;
                checkBox9.Checked = true;
                checkBox13.Checked = true;
                checkBox17.Checked = true;
                checkBox21.Checked = true;
                checkBox25.Checked = true;
                checkBox29.Checked = true;
                checkBox33.Checked = true;
                checkBox37.Checked = true;
            }
            else
            {
                checkBox5.Checked = false;
                checkBox9.Checked = false;
                checkBox13.Checked = false;
                checkBox17.Checked = false;
                checkBox21.Checked = false;
                checkBox25.Checked = false;
                checkBox29.Checked = false;
                checkBox33.Checked = false;
                checkBox37.Checked = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            UserType ut = new UserType();
            ut.Title = textBoxX8.Text;
            ut.UserAccessRoles.Add(FillAccessRole(label3.Text, checkBox8.Checked, checkBox7.Checked, checkBox6.Checked, checkBox5.Checked));
            ut.UserAccessRoles.Add(FillAccessRole(label4.Text, checkBox12.Checked, checkBox11.Checked, checkBox10.Checked, checkBox9.Checked));
            ut.UserAccessRoles.Add(FillAccessRole(label5.Text, checkBox16.Checked, checkBox15.Checked, checkBox14.Checked, checkBox13.Checked));
            ut.UserAccessRoles.Add(FillAccessRole(label14.Text, checkBox20.Checked, checkBox19.Checked, checkBox18.Checked, checkBox17.Checked));
            ut.UserAccessRoles.Add(FillAccessRole(label15.Text, checkBox24.Checked, checkBox23.Checked, checkBox22.Checked, checkBox21.Checked));
            ut.UserAccessRoles.Add(FillAccessRole(label16.Text, checkBox28.Checked, checkBox27.Checked, checkBox26.Checked, checkBox25.Checked));
            ut.UserAccessRoles.Add(FillAccessRole(label17.Text, checkBox32.Checked, checkBox31.Checked, checkBox30.Checked, checkBox29.Checked));
            ut.UserAccessRoles.Add(FillAccessRole(label18.Text, checkBox36.Checked, checkBox35.Checked, checkBox34.Checked, checkBox33.Checked));
            ut.UserAccessRoles.Add(FillAccessRole(label19.Text, checkBox40.Checked, checkBox39.Checked, checkBox38.Checked, checkBox37.Checked));
            ms.MsShow("Result", utbll.Create(ut),"", false, false);
            comboBoxEx1.DataSource = utbll.ReadUTNames();
        }
    }
}
