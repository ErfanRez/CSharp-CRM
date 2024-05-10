using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class DashBoardBLL
    {
        DashBoardDAL Ddal = new DashBoardDAL();
        public string UserReminderCount(User u)
        {
            return Ddal.UserReminderCount(u);
        }

        public string CustomerCount()
        {
            return Ddal.CustomerCount();
        }

        public string SellsCount()
        {
            return Ddal.SellsCount();
        }

        public List<Reminder> GetReminders(User u)
        {
            return Ddal.GetReminders(u);
        }
    }
}
