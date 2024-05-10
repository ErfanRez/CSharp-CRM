using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public class ActivityCategoryBLL
    {
        ActivityCategoryDAL acdal = new ActivityCategoryDAL();

        public string Create (ActivityCategory ac)
        {
            return acdal.Create (ac);
        }

        public DataTable Read()
        {
            return acdal.Read();
        }
        public string Delete (int id)
        {
            return acdal.Delete (id);
        }

        public string Update (ActivityCategory ac,int id)
        {
            return acdal.Update (ac,id);
        }
        
        public ActivityCategory SearchCategory(string s)
        {
            return acdal.SearchCategory(s);
        }

        public List<string> ReadCategory()
        {
            return acdal.ReadCategory();
        }

        public ActivityCategory Read(int id)
        {
            return acdal.Read (id);
        }
    }
}
