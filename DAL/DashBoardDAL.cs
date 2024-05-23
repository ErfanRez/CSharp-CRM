using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DashBoardDAL
    {
        DB db = new DB();

        public string UserReminderCount(User u)
        {
            var q = db.Users.Include("Reminders").Where(i => i.Id == u.Id && i.DeleteStatus == false).FirstOrDefault();
            if (q != null) {

                return q.Reminders.Count().ToString();
            }
            else
            {
                string s = q.Reminders.Count().ToString();
                s = "0";
                return s;
            }


        }

        public string CustomerCount()
        {
            return db.Customers.Count().ToString();
        }

        public string SellsCount()
        {
            int sum = 0;
            foreach (var item in db.Invoices)
            {
                if (item.RegDate.Date == DateTime.Today)
                {
                    sum += 1;
                }
            }
            return sum.ToString();
        }

        public List<Reminder> GetReminders(User u)
        {
            return db.Reminders.Include("Users").Where(i => i.Users.Id == u.Id).ToList();
        }
    }
}
