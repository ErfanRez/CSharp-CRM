using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ActivityDAL
    {
        DB db = new DB();

        public string Create(Activity a, User u, Customer c,ActivityCategory ac)
        {
            try
            {
                a.User = db.Users.Find(u.Id);
                a.Customer = db.Customers.Find(c.Id);
                a.Category = db.ActivityCategories.Find(ac.Id);
                db.Activities.Add(a);
                db.SaveChanges();
                return "Adding Data is Successfuly Done";
            }
            catch (Exception e)
            {

                return "Adding Data Faced Problem\n" + e.Message;
            }
        }

        public DataTable Read()
        {
            string cmd = "SELECT dbo.Activities.Id AS ID, dbo.Activities.Title, dbo.ActivityCategories.CategoryName AS Category, dbo.Customers.Name AS [Customer's Name], dbo.Users.Name AS [Seller's Name], dbo.Activities.RegDate AS [Register Date] FROM dbo.ActivityCategories INNER JOIN dbo.Activities ON dbo.ActivityCategories.Id = dbo.Activities.Category_Id INNER JOIN dbo.Customers ON dbo.Activities.Customer_Id = dbo.Customers.Id INNER JOIN dbo.Users ON dbo.Activities.User_Id = dbo.Users.Id WHERE  (dbo.Activities.DeleteStatus = 0)";

            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMDb; Integrated Security = true");

            var sqladpater = new SqlDataAdapter(cmd, con);

            var commandbulider = new SqlCommandBuilder(sqladpater);

            var ds = new DataSet();

            sqladpater.Fill(ds);


            return ds.Tables[0];
        }

        public Activity Read (int id)
        {
            return db.Activities.Where(i => i.Id == id).FirstOrDefault();
        }

        public string Delete (int id)
        {
            var q = db.Activities.Where (i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "Selected Activity Is Successfuly Deleted";
                }
                else
                {
                    return "Selected Activity Is Not Found";
                }
            }
            catch (Exception e)
            {
                return "Deleting Data Faced Problem\n" + e.Message;

            }
        }

        public string Done (int id)
        {
            var q = db.Activities.Where(i => i.Id == id).FirstOrDefault();
          
            try
            {
                if (q != null)
                {
                    q.IsDone = true;
                    db.SaveChanges ();
                    return "Congratulations! You have done the activity";
                }
                else
                {
                    return "Selected Activity Is Not Found";
                }
            }
                
            catch (Exception e)
            {

                return "Something went wrong!\n" + e.Message;
            }
        }

        public DataTable SearchActivity(string s)   //important search codes combined with stored procedure sql commands

        //This func will search for entered string by user into the database character by character

        //If(s) are for checkbox conditions to search according to name or phone number

        {

            SqlCommand cmd = new SqlCommand("dbo.SearchActivity");


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

        public DataTable SearchTitle(string s)   //important search codes combined with stored procedure sql commands

        //This func will search for entered string by user into the database character by character

        //If(s) are for checkbox conditions to search according to name or phone number

        {

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "dbo.SearchActivityTitle";

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

        public string DoneCount()
        {
            return db.Activities.Where(i => i.DeleteStatus == false && i.IsDone == true).Count().ToString();
        }

    }
}
