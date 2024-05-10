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
    public class InvoiceBLL
    {
        InvoiceDAL idal = new InvoiceDAL();
        public string Create (Invoice i,Customer c,List<Product> p)
        {
            return idal.Create (i,c,p);
        }

        public string ReadInvoiceNum()
        {
            return idal.ReadInvoiceNum();
        }

        public DataTable Read()
        {
            return idal.Read();
        }

        public string Count()
        {
            return idal.Count();
        }

        public string Delete(int id)
        {
            return idal.Delete(id);
        }
    }
}
