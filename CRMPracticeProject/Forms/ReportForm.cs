using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Stimulsoft.Report;

namespace CRMPracticeProject.Forms
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        UserBLL ubll = new UserBLL();

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                StiReport sti = new StiReport();
                sti.Load(@"C:\Users\ERFAN\Desktop\CRM-Source Files\Report.mrt"); //load the stimulsoft report file
                sti.Render();
                sti.Show();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            if (radioButton8.Checked)
            {
                foreach (var item in ubll.Readnvoices())
                {
                    int x = 0;
                    foreach (var q in item.Invoices)
                    {
                        if (q.RegDate.Date > dateTimeInput1.Value.Date && q.RegDate.Date < dateTimeInput2.Value.Date)
                        {
                            x++;
                        }
                    }
                    chart1.Series["Series1"].Points.AddXY(item.Name,x);
                }
            }
        }
    }
}
