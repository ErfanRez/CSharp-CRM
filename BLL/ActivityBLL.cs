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
    public class ActivityBLL
    {
        ActivityDAL adal = new ActivityDAL();
        public string Create (Activity a,User u,Customer c,ActivityCategory ac)
        {
            return adal.Create(a,u,c,ac);
        }

        public DataTable Read()
        {
             return adal.Read();
        }

        public Activity Read (int id)
        {
            return adal.Read(id);
        }

        public string Delete (int id) 
        { 
            return adal.Delete(id); 
        }

        public string Done (int id)
        {
            return adal.Done(id);
        }

        public DataTable SearchActivity(string s)
        {
            return adal.SearchActivity(s);
        }

        public DataTable SearchTitle(string s)
        {
            return adal.SearchTitle(s);
        }

        public string DoneCount()
        {
            return adal.DoneCount();
        }
    }
}
