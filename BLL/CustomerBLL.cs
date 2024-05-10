using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;


namespace BLL
{
    public class CustomerBLL
    {
        CustomerDAL cdal = new CustomerDAL();
        public string Create (Customer c)
        {
            return cdal.Create (c);
        }


        public DataTable Read()
        {
            return cdal.Read();
        }


        public Customer Read(int id)
        {
            return cdal.Read(id);
        }

        public DataTable Search(string s, int index)
        {
            return cdal.Search(s, index);
        }


        public string Update(Customer c, int id)
        {
            return cdal.Update(c, id);
        }


        public string Delete (int id)
        {
            return cdal.Delete(id);
        }

        public Customer SearchPhone(string s)
        {
            return cdal.SearchPhone(s);
        }

        public List<string> ReadCustomerPhone()
        {
            return cdal.ReadCustomerPhone();
        }

        public string CustomerCount()
        {
            return cdal.CustomerCount();
        }
    }
}
