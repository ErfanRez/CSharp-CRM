using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DAL
{
    public class CustomerDAL
    {
        DB db = new DB();

        public string Create(Customer c)
        {
            try
            {
                if (Read(c))
                {
                    db.Customers.Add(c);
                    db.SaveChanges();

                    return "Adding Data Is Successfuly Done";
                }
                else
                {
                    return "Customer's Phone Number Is Already Registered";
                }
               
            }
            catch (Exception e)
            {
                return "Adding Data Faced Probelm:\n" + e.Message;
            }
        }

        public bool Read (Customer c)
       
        {
            var q = db.Customers.Where(i => c.Phone == i.Phone);

            if (q.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable Read ()   // retun as DataTabele NOT List  

        //important new sql data code (better form)
        
        {
            string cmd = "SELECT Id AS ID, Name AS [Customer's Name], Phone AS [Phone Number], RegDate AS [Register Date]FROM dbo.Customers WHERE(DeleteStatus = 0)";
            
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMDb; Integrated Security = true");
            
            var sqladpater = new SqlDataAdapter(cmd, con);

            var commandbulider = new SqlCommandBuilder(sqladpater);

            var ds = new DataSet();

            sqladpater.Fill(ds);
           
            
            return  ds.Tables[0];

        }

        public Customer Read (int id)
        {
            return db.Customers.Where (i => i.Id == id).FirstOrDefault();
        }

        public DataTable Search (string s,int index)   //important search codes combined with stored procedure sql commands

            //This func will search for entered string by user into the database character by character
            
            //If(s) are for checkbox conditions to search according to name or phone number
          
        {

            SqlCommand cmd = new SqlCommand();

            if (index == 0)
            {
                cmd.CommandText = "dbo.SearchCustomer";
            
            }else if (index == 1)
            {
                cmd.CommandText = "dbo.SearchCustomerName";
            }
            else if (index == 2)
            {
                cmd.CommandText = "dbo.SearchCustomerPhone";
            }
           
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

        public string Update(Customer c, int id)
        {
            var q = db.Customers.Where(i => i.Id == id).FirstOrDefault();
            try
            {

                if (q != null)
                {


                    q.Name = c.Name;
                    q.Phone = c.Phone;
                    db.SaveChanges();
                    return "Editing Data Is Successfuly Done";
                }
                else
                {

                    return "Customer Is Not Found";
                }
            }

            catch (Exception e)
            {
                return "Editing Data Faced Problem:\n" + e.Message;
            }


        }

        public string Delete (int Id)
        {
            var q = db.Customers.Where(i => i.Id == Id).FirstOrDefault();
            try
            {

                if (q != null)
                {


                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "Deleting Data Is Successfuly Done";
                }
                else
                {

                    return "Cutomer Is Not Found";
                }
            }

            catch (Exception e)
            {
                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }

        public Customer SearchPhone (string s)
        {
            return db.Customers.Where(i => i.Phone == s && i.DeleteStatus == false).FirstOrDefault();
        }

        public List<string> ReadCustomerPhone()
        {
            return db.Customers.Where(i => i.DeleteStatus == false).Select (i => i.Phone).ToList();
        }

        public string CustomerCount()
        {
            return db.Customers.Where(i => i.DeleteStatus == false).Count().ToString();
        }
    }
    
}
