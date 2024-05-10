using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Reminder
    {

        public Reminder()
        {
            IsDone = false;
            IsDeleted = false;
        }

        public int Id { get; set; }

        public  string Title { get; set; }

        public string ReminderInfo { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime RemindDate { get; set; }

        public bool IsDone { get; set; }

        public User Users { get; set; }
    }
}
