using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    public class ReminderBLL
    {
        ReminderDAL rdal = new ReminderDAL();
        public string Create(Reminder r,User u)
        {
            return rdal.Create(r,u);
        }
       
        public DataTable Read()
        {
            return rdal.Read();
        }

        public DataTable Search(String s)
        {
            return rdal.Search(s);
        }

        public string Update(Reminder r, int id)
        {
            return rdal.Update(r,id);
        }

        public string Delete(int id)
        {
            return rdal.Delete(id);
        }

        public string Done(int id)
        {
            return rdal.Done(id);
        }

        public Reminder Read (int id)
        {
            return rdal.Read(id);
        }

        public string ReminderCount()
        {
            return rdal.ReminderCount();
        }

        public Reminder ReminderInfo(int id)
        {
            return rdal.ReminderInfo(id);
        }
    }
}
