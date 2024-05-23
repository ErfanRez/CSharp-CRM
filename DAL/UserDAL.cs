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
    public class UserDAL
    {

        DB db = new DB();

        public string Create(User u, UserType ut)
        {
            try
            {
                if (Read(u))
                {
                    u.UserType = db.UserTypes.Find(ut.Id);
                    db.Users.Add(u);
                    db.SaveChanges();
                    return "Adding data is successfuly done";
                }
                else
                {
                    return "username is taken before";
                }
            }
            catch (Exception e)
            {
                return "Adding data faced problem\n" + e.Message;

            }
        }

        public bool IsRegistered()
        {
            return db.Users.Count() > 0;
        }

        public bool Read(User u)
        {
            var q = db.Users.Where(i => i.Username == u.Username);
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
            string cmd = "SELECT  dbo.Users.Id AS ID, dbo.Users.Name AS [Name and Surname], dbo.Users.Username AS [Username], dbo.Users.RegDate AS [Register Date], dbo.UserTypes.Title AS [User Type] FROM dbo.Users INNER JOIN dbo.UserTypes ON dbo.Users.UserType_Id = dbo.UserTypes.Id WHERE(dbo.Users.DeleteStatus = 0)";

            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMDb; Integrated Security = true");

            var sqladpater = new SqlDataAdapter(cmd, con);

            var commandbulider = new SqlCommandBuilder(sqladpater);

            var ds = new DataSet();

            sqladpater.Fill(ds);


            return ds.Tables[0];

        }

        public User Read(int id)
        {
            return db.Users.Where(i => i.Id == id).FirstOrDefault();
        }

        public string Update(User u, int id)
        {
            var q = db.Users.Where(i => i.Id == id).FirstOrDefault();
            try
            {

                if (q != null)
                {


                    q.Name = u.Name;
                    q.Username = u.Username;
                    q.Password = u.Password;
                    q.Pic = u.Pic;
                    db.SaveChanges();
                    return "Editing Data Is Successfuly Done";
                }
                else
                {

                    return "User Is Not Found";
                }
            }

            catch (Exception e)
            {
                return "Editing Data Faced Problem\n" + e.Message;
            }


        }

        public string Delete(int Id)
        {
            var q = db.Users.Where(i => i.Id == Id).FirstOrDefault();
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

                    return "User Is Not Found";
                }
            }

            catch (Exception e)
            {
                return "Deleting Data Faced Problem\n" + e.Message;
            }
        }

        public User SearchU(string s)
        {
            return db.Users.Where(i => i.Username == s && i.DeleteStatus == false).FirstOrDefault();
        }

        public List<string> ReadUserNames()
        {
            return db.Users.Where(i => i.DeleteStatus == false).Select(i => i.Username).ToList();
        }

        public User Login(string u, string p)
        {
            return db.Users.Include("UserType").Where(i => i.Username == u && i.Password == p).SingleOrDefault();
        }
       
        public bool Access(User u, String s,int a)
        {
            UserType ut = db.UserTypes.Include("UserAccessRoles").Where(i => i.Id == u.UserType.Id).FirstOrDefault();
            UserAccessRole uar = ut.UserAccessRoles.Where(z => z.Section == s).FirstOrDefault();
            if ( a == 1)
            {
                return uar.CanEnter;
            }
            else if (a == 2)
            {
                return uar.CanCreate;
            }
            else if (a ==3 )
            {
                return uar.CanUpdate;
            }
            else
            {
                return uar.CanDelete;
            }
        }

        public List <User> ReadInvoices()
        {
            return db.Users.Include("Invoices").Where(i => i.DeleteStatus == false).ToList();
        }

        public DataTable SearchUser(string s)   //important search codes combined with stored procedure sql commands

        //This func will search for entered string by user into the database character by character

        //If(s) are for checkbox conditions to search according to name or phone number

        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.SearchUser";

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

        public string UsersCount()
        {
            return db.Users.Where(i => i.DeleteStatus == false).Count().ToString();
        }
    }

    
}
