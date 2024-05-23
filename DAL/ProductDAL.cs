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
    public class ProductDAL
    {
        DB db = new DB();

        public string Create(Product p)
        {
            try
            {
                db.Products.Add(p);
                db.SaveChanges();

                return "Adding Product is successfully done!";
            }
            catch (Exception e)
            {
                return "Error occured:\n" + e.Message;
            }
        }

        public bool Read(Product p)

        {
            var q = db.Products.Where(i => i.Name == p.Name);

            if (q.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable Read()   // retun as DataTabele NOT List  

        //important new sql data code (better form)

        {
            string cmd = "SELECT Id AS ID, Name AS [Product Name], Price AS [Price], Stock AS [Stock], RegDate AS [Register Date] FROM dbo.Products WHERE(DeleteStatus = 0)";

            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMDb; Integrated Security = true");

            var sqladpater = new SqlDataAdapter(cmd, con);

            var commandbulider = new SqlCommandBuilder(sqladpater);

            var ds = new DataSet();

            sqladpater.Fill(ds);


            return ds.Tables[0];

        }

        public Product Read(int id)
        {
            return db.Products.Where(i => i.Id == id).FirstOrDefault();
        }

        public DataTable Search(string s)   //important search codes combined with stored procedure sql commands

        //This func will search for entered string by user into the database character by character

        //If(s) are for checkbox conditions to search according to name or phone number

        {

            SqlCommand cmd = new SqlCommand("dbo.SearchProduct");

            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMDb; Integrated Security = true");

            cmd.Parameters.AddWithValue("@search", s);

            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;

            var sqladpater = new SqlDataAdapter();

            sqladpater.SelectCommand = cmd;

            var commandbulider = new SqlCommandBuilder(sqladpater);

            var ds = new DataSet();

            sqladpater.Fill(ds);


            return ds.Tables[0];

        }

        public string Update(Product p, int Id)
        {
            var q = db.Products.Where(i => i.Id == Id).FirstOrDefault();
            try
            {

                if (q != null)
                {


                    q.Name = p.Name;
                    q.Stock = p.Stock;
                    q.Price = p.Price;
                    db.SaveChanges();
                    return "Editing data is successfully done";
                }
                else
                {

                    return "Product is not found!";
                }
            }

            catch (Exception e)
            {
                return "Error occured:\n" + e.Message;
            }


        }

        public string Delete(int Id)
        {
            var q = db.Products.Where(i => i.Id == Id).FirstOrDefault();
            try
            {

                if (q != null)
                {


                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "Deleteing data is successfully done";
                }
                else
                {

                    return "Product is not found!";
                }
            }

            catch (Exception e)
            {
                return "Error Occured:\n" + e.Message;
            }
        }

        public List<string> ReadProductNames() 
        {
            return db.Products.Where(i => i.DeleteStatus == false).Select(i => i.Name).ToList();
        }

        public Product SearchProduct(string s)
        {
            return db.Products.Where(i => i.Name == s).FirstOrDefault();
        }

        public string ProductCount()
        {
            return db.Products.Where(i => i.DeleteStatus == false).Count().ToString();
        }
    }
}
