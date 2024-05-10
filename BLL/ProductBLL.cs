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
    public class ProductBLL
    {
        ProductDAL pdal = new ProductDAL();

        public string Create(Product p)
        {
            return pdal.Create(p);
        }

        public DataTable Read()
        {
            return pdal.Read();
        }

        public Product Read (int id)
        {
            return pdal.Read(id);
        }

        public DataTable Search (string s)
        {
            return pdal.Search(s);
        }

        public string Delete (int id)
        {
            return pdal.Delete(id);
        }

        public string Update (Product p,int id)
        {
            return pdal.Update(p, id);
        }

        public List<string> ReadProductNames()
        {
            return pdal.ReadProductNames();
        }

        public Product SearchProduct(string s)
        {
            return pdal.SearchProduct(s);
        }

        public string ProductCount()
        {
            return pdal.ProductCount();
        }
    }
}
