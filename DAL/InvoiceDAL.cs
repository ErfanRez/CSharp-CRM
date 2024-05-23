using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class InvoiceDAL
    {
        DB db = new DB();

        public string Create(Invoice i, Customer c,List<Product> p)
        {
            try
            {
                i.Customer = db.Customers.Find(c.Id);
                foreach (var item in p)
                {
                    i.Products.Add(db.Products.Find(item.Id));
                }
                Random rnd = new Random(); //for using random number creation methods
                string s = rnd.Next(1000000).ToString();
                var q = db.Invoices.Where(z => z.InvoiceNumber == s);
                while (q.Count() > 0) //for not making same invoice numbers for two different product
                {
                    s = rnd.Next(1000000).ToString();
                }
                i.InvoiceNumber = s;
                db.Invoices.Add(i);
                db.SaveChanges();
                return "Invoice is successfully registered";

            }
            catch (Exception e)
            {

                return "Something Went Wrong!\n" + e.Message;
            }
        }

        public string ReadInvoiceNum()
        {
            var q = db.Invoices.OrderByDescending(z => z.InvoiceNumber).FirstOrDefault();
            return q.InvoiceNumber;
        }

        public DataTable Read()   // retun as DataTabele NOT List  

        //important new sql data code (better form)

        {
            string cmd = "SELECT dbo.Invoices.Id AS ID, dbo.Invoices.InvoiceNumber AS [Invoice Number], dbo.Invoices.RegDate AS [Register Date], dbo.Customers.Name AS [Customer's Name] FROM dbo.Invoices INNER JOIN dbo.Customers ON dbo.Invoices.Customer_Id = dbo.Customers.Id WHERE  (dbo.Invoices.DeleteStatus = 0)";

            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMDb; Integrated Security = true");

            var sqladpater = new SqlDataAdapter(cmd, con);

            var commandbulider = new SqlCommandBuilder(sqladpater);

            var ds = new DataSet();

            sqladpater.Fill(ds);


            return ds.Tables[0];

        }

        public string Count()
        {
            return db.Invoices.Where(i => i.DeleteStatus == false).Count().ToString();
        }

        public string Delete (int id)
        {
            var q = db.Invoices.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    q.DeleteStatus = false;
                    db.SaveChanges();
                    return "Deleting Data Is Successfuly Done";
                }
                else
                {
                    return "Invoice Is Not Found!";
                }
            }
            catch (Exception e)
            {
                return "Deleting Data Faced Problem!\n" + e.Message;
            }
        }

    }
}
