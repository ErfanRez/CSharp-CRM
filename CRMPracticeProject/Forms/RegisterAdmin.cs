using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoxLearn.License;
using BLL;
using BE;
using System.IO;

namespace CRMPracticeProject.Forms
{
    public partial class RegisterAdmin : UserControl
    {
        public RegisterAdmin()
        {
            InitializeComponent();
        }

        MsBoxClass ms = new MsBoxClass();
        Timer t1 = new Timer();
        OpenFileDialog ofd = new OpenFileDialog();
        Image img;
        UserBLL ubll = new UserBLL();
        UserTypeBLL utbll = new UserTypeBLL();
        void SwitchPanels()
        {
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer_Tick;
            t1.Start();
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
                MessageBox.Show("Picture Couldn't be saved " + e.Message);
            }
            return path + PicName;
        }

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
        
        void CreateAdminGroup()
        {
            UserType ut = new UserType();
            ut.Title = "Admin";
            ut.UserAccessRoles.Add(FillAccessRole("Customers Part",true,true,true,true));
            ut.UserAccessRoles.Add(FillAccessRole("Products Part", true, true, true, true));
            ut.UserAccessRoles.Add(FillAccessRole("Invoices Part", true, true, true, true));
            ut.UserAccessRoles.Add(FillAccessRole("Activities Part", true, true, true, true));
            ut.UserAccessRoles.Add(FillAccessRole("Reminders Part", true, true, true, true));
            ut.UserAccessRoles.Add(FillAccessRole("Users Part", true, true, true, true));
            ut.UserAccessRoles.Add(FillAccessRole("Sms Part", true, true, true, true));
            ut.UserAccessRoles.Add(FillAccessRole("Reports Part", true, true, true, true));
            ut.UserAccessRoles.Add(FillAccessRole("Settings", true, true, true, true));
            utbll.Create(ut);
        }
        
        int y = 326;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (panel1.Location.Y < 750)
            {
                y += 15;
                panel1.Location = new Point(3,y);
            }
            else
            {
                t1.Stop();
                panel3.Visible = true;
            }
        }
        private void RegisterAdmin_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = ComputerInfo.GetComputerId();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(textBoxX1.Text);
            string productkey = textBoxX2.Text;
            if (km.ValidKey(ref productkey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productkey,ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productkey;
                    lic.FullName = "Personal accounting";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }

                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lic);
                    ms.MsShow("Congratulations!", "Software is successfuly activated","", false, false);
                    SwitchPanels();

                }
            }
            else
            {
                ms.MsShow("Error", "Enterd license is Not Coreect!", "", false, true);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            User u = new User();
            CreateAdminGroup();
            u.Username = textBoxX3.Text;
            u.Name = textBoxX4.Text;
            if (textBoxX5.Text == textBoxX6.Text)
            {
                u.Password = textBoxX5.Text;
            }
            else
            {
                ms.MsShow("Error", "The password and the repeat don't match!","",false,true) ;
            }
            u.RegDate = DateTime.Now;
            u.Pic = SavePic(textBoxX3.Text);
            ms.MsShow("Registration Result", ubll.Create(u,utbll.ReadN("Admin")), "", false, false);
            this.Visible = false;
            ((LoginForm)(Application.OpenForms["LoginForm"])).LoadLoginForm();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = "Choose User's Picture";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                img = Image.FromFile(ofd.FileName);
            }
        }
    }
}
