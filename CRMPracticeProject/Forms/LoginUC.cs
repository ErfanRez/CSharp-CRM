using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BE;
using BLL;

namespace CRMPracticeProject.Forms
{
    public partial class LoginUC : UserControl
    {
        public LoginUC()
        {
            InitializeComponent();
        }

        UserBLL ubll = new UserBLL();
        MsBoxClass ms = new MsBoxClass();
        User u =  new User();
        DashBoardBLL Dbll = new DashBoardBLL();

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            u = ubll.Login(textBoxX3.Text, textBoxX5.Text);
            if (u != null)
            {
                ms.MsShow("Welcome", "To Enter Application press Ok", "", false, false);
                MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
                w.LoggedInUser = u;
                w.RefreshPage();
                ((LoginForm)System.Windows.Forms.Application.OpenForms["LoginForm"]).Close();
            }
            else
            {
                ms.MsShow("Error", "Username or Password is wrong", "", false, true);
            }
        }
    }
}
