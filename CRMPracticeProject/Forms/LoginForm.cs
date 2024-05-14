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

namespace CRMPracticeProject.Forms
{
    public partial class LoginForm : Form
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

        public LoginForm()
        {
            this.Controls.Add(r);
            this.Controls["RegisterAdmin"].Location = new Point(400, 830);
            this.Controls.Add(l);
            this.Controls["LoginUC"].Location = new Point(400,830);
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        Timer t1 = new Timer();
        Timer t2 = new Timer();
        Timer t3 = new Timer();
        UserBLL ubll = new UserBLL();
        List<string> usernames = new List<string>();
        RegisterAdmin r = new RegisterAdmin();
        LoginUC l = new LoginUC();
        bool IsRegistered;
        int y = 390;
        int y2 = 830;
        int y3 = 830;

        public void LoadLoginForm()
        {
            t3.Enabled = true;
            t3.Interval = 15;
            t3.Tick += Timer3_Tick;
            t3.Start();

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            label2.Visible = true;
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer_Tick;
            t1.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressBarX1.Value >= 100)
            {
                t1.Stop();
                progressBarX1.Visible = false;
                label2.Visible = false;
                label3.Visible = true;
                t2.Enabled = true;
                t2.Interval = 1;
                t2.Tick += Timer2_Tick;
                t2.Start();
            }
            else if (progressBarX1.Value == 45)
            {
                IsRegistered = ubll.IsRegistered();
                usernames = ubll.ReadUserNames();
                progressBarX1.Value++;
            }
            else
            {
                progressBarX1.Value++;
            }
        }
 
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (label3.Location.Y >= 45)
            {
                y -= 15;
                y2 -= 30;
                label3.Location = new Point(496, y);
                if (IsRegistered)
                {
                    this.Controls["LoginUC"].Location = new Point(400, y2);
                }
                else
                {
                    this.Controls["RegisterAdmin"].Location = new Point(400, y2);
                }

            }
            else
            {
                t2.Stop();
                panel1.Visible = true;
            }
        }

        private void Timer3_Tick(object sender,EventArgs e)
        {
            if (this.Controls["LoginUC"].Location.Y >= 100)
            {
                y3 -= 30;
                this.Controls["LoginUC"].Location = new Point(400, y2);
            }
            else
            {
                t3.Stop();
            }
        }
    }
}