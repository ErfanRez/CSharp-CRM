using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class User
    {
        public User()
        {
            DeleteStatus = false;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Pic { get; set; } //For User Picture

        public bool DeleteStatus { get; set; }

        public DateTime RegDate { get; set; }

        public List<Activity> Activites { get; set; } = new List<Activity>();
        
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();

        public List<Reminder> Reminders { get; set; } = new List<Reminder>();

        public UserType UserType { get; set; }
    }
}
