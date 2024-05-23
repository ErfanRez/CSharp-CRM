using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DAL
{
    public class DB : DbContext
 
    {
            public DB() : base("Constr")
            {

            }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Invoice> Invoices { get; set; }
            public DbSet<Reminder> Reminders { get; set; }
            public DbSet<Activity> Activities { get; set; }
            public DbSet<ActivityCategory> ActivityCategories { get; set; }
            public DbSet<User> Users { get; set; } 
            public DbSet<UserType> UserTypes { get; set; }
            public DbSet<UserAccessRole> UserAccessRoles { get; set; }


    }

    
}
