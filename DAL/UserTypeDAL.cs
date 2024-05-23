using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class UserTypeDAL
    {
        DB db = new DB();

        public string Create (UserType ut)
        {
            try
            {
                db.UserTypes.Add(ut);
                db.SaveChanges();
                return "User Type is successfuly done.";
            }
            catch (Exception e)
            {

                return "Something Went Wrong!" + e.Message;
            }
        }

        public bool Read(string Name)
        {
            return !db.UserTypes.Any(i => i.Title == Name);
        }

        public List<string> ReadUTNames()
        {
            return db.UserTypes.Select(i => i.Title).ToList();
        }

        public UserType ReadN(string n)
        {
            return db.UserTypes.Where(i => i.Title == n).FirstOrDefault();
        }
    }
}
