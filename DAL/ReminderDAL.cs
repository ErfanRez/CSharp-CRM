using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class ReminderDAL
    {
        DB db = new DB();

        public string Create (Reminder r,User u)
        {
            try
            {
                r.Users = db.Users.Find(u.Id);
                db.Reminders.Add(r);
                db.SaveChanges();
                return "Adding data was successfuly done";
            }
            catch (Exception e)
            {
                return "Error occured!\n" + e.InnerException;
            }
        }

        public DataTable Read()
        {
            string cmd = "SELECT dbo.Reminders.Id AS ID, dbo.Reminders.Title, dbo.Reminders.RegDate AS [Register Date], dbo.Reminders.RemindDate AS [Remind Date], dbo.Reminders.IsDone AS [Done/NotDone], dbo.Users.Username\r\nFROM     dbo.Reminders INNER JOIN\r\n                  dbo.Users ON dbo.Reminders.Users_Id = dbo.Users.Id\r\nWHERE  (dbo.Reminders.IsDeleted = 0)";

            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMDb; Integrated Security = true");

            var sqladpater = new SqlDataAdapter(cmd, con);

            var commandbulider = new SqlCommandBuilder(sqladpater);

            var ds = new DataSet();

            sqladpater.Fill(ds);


            return ds.Tables[0];
        }

        public Reminder Read(int id)
        {
            return db.Reminders.Where(i => i.Id == id).FirstOrDefault();
        }

        public DataTable Search(String s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchReminders");

            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMDb; Integrated Security = true");

            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;

            var sqladpater = new SqlDataAdapter();

            sqladpater.SelectCommand = cmd;

            var commandbulider = new SqlCommandBuilder(sqladpater);

            var ds = new DataSet();

            sqladpater.Fill(ds);


            return ds.Tables[0];
        }

        public string Update (Reminder r, int id)
        {
            var q = db.Reminders.Where(i => i.Id == id ).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.RemindDate = r.RemindDate;
                    q.ReminderInfo = r.ReminderInfo;
                    q.Title = r.Title;
                    db.SaveChanges();
                    return "Editing data is successfully done";
                }
                else
                {
                    return "Reminder is not found!";
                }
            }
            catch (Exception e)
            {
                return "Error Occured!\n" + e.Message;
            }
        }

        public string Delete (int id)
        {
            var q = db.Reminders.Where(i => i.Id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsDeleted = true;
                    db.SaveChanges();
                    return "Deleting data is Successfully done";
                }
                else
                {
                    return "Reminder is not found!";
                }
            }
            catch (Exception e)
            {
                return "Error Occured!\n" + e.Message;
            }
        }

        public string Done(int id)
        {
            var q = db.Reminders.Where(i => i.Id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsDone = true;
                    db.SaveChanges();
                    return "Reminder's status is done";
                }
                else
                {
                    return "Reminder is not found!";
                }
            }
            catch (Exception e)
            {
                return "Error Occured!\n" + e.Message;
            }
        }

        public string ReminderCount()
        {
            return db.Reminders.Where(i => i.IsDeleted == false && i.IsDone == true).Count().ToString();
        }

        public Reminder ReminderInfo(int id)
        {
            return db.Reminders.Where (i => i.Id == id).FirstOrDefault();
        }
    }
}
