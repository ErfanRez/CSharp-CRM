using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class UserTypeBLL
    {
        UserTypeDAL utdal = new UserTypeDAL();

        public string Create (UserType ut)
        {
            return utdal.Create (ut);
        }

        public bool Read(string Name)
        {
            return utdal.Read (Name);
        }

        public List<string> ReadUTNames()
        {
            return utdal.ReadUTNames();
        }

        public UserType ReadN(string n)
        {
            return utdal.ReadN(n);
        }
    }
}
