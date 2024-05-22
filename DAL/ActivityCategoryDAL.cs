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
    public class ActivityCategoryDAL
    {
        DB db = new DB();

        public string Create(ActivityCategory ac)
        {

            try
            {
                if (Read(ac))
                {
                    db.ActivityCategories.Add(ac);
                    db.SaveChanges();
                    return "New category has been added.";
                }
                else
                {
                    return "The same catgory already exists!";
                }

            }
            catch (Exception e)
            {
                return "Something went wrong!:\n" + e.Message;

            }

        }

        public bool Read (ActivityCategory ac)
        {
            var q = db.ActivityCategories.Where (i => ac.Id == i.Id);
            if (q.Count() == 0)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public ActivityCategory Read (int id)
        {
            return db.ActivityCategories.Where(i => i.Id == id).FirstOrDefault();
        }

        public DataTable Read()
        {
            string cmd = "SELECT Id AS Id, CategoryName AS [Category Name] FROM dbo.ActivityCategories WHERE(DeleteStatus = 0)";

            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMDb; Integrated Security = true");

            var sqladpater = new SqlDataAdapter(cmd, con);

            var commandbulider = new SqlCommandBuilder(sqladpater);

            var ds = new DataSet();

            sqladpater.Fill(ds);


            return ds.Tables[0];
        }

        public string Delete(int id)
        {
            var q = db.ActivityCategories.Where (i => i.Id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "Category is successfully deleted.";
                }
                else
                {
                    return "Category was not found!";
                }
            }
            catch (Exception e)
            {
                return "Something went wrong!\n" + e.Message;
                
            }
        }

        public string Update (ActivityCategory ac,int id)
        {
            var q = db.ActivityCategories.Where(i => i.Id == id).FirstOrDefault();
      
            try
            {
                if (q != null)
                {
                    q.CategoryName = ac.CategoryName;
                    db.SaveChanges();
                    return "Editing category was successfully done.";
                }
                else
                {
                    return "Category was not found!";
                }
            }
            catch (Exception e)
            {
                return "Something went wrong!\n" + e.Message;

            }
        }

        public ActivityCategory SearchCategory(string s)
        {
            return db.ActivityCategories.Where(i => i.CategoryName == s && i.DeleteStatus == false).FirstOrDefault();
        }

        public List<string> ReadCategory()
        {
            return db.ActivityCategories.Where(i => i.DeleteStatus == false).Select(i => i.CategoryName).ToList();
        }
    }
}
