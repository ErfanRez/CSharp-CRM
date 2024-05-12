using CRMPracticeProject.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMPracticeProject
{
    public class MsBoxClass
    {

        public DialogResult MsShow (string title,string FaInfo,string EngInfo,bool buttons,bool type)
        { 
            MyMsBox m = new MyMsBox();
            m.label1.Text= title;
            m.label2.Text = FaInfo;
            m.label3.Text = EngInfo;


            if (buttons)
            {
                m.buttonX2.Text = "No";
            }
            else
            {
                m.buttonX1.Visible = false;
            }

            if (type)
            {
                m.BackColor = Color.FromArgb(252, 163, 17);
                m.pictureBox1.Image = Properties.Resources.Warning;
            }



            m.ShowDialog();
            return m.DialogResult;
            
        }
    }
}
