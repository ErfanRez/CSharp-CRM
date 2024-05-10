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
    public class UserBLL
    {

        private string Encode(string Pass)
        {
            byte[] encData_byte = new byte[Pass.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(Pass);
            string encodeData = Convert.ToBase64String(encData_byte);
            return encodeData;
        }

        private string Decode(string EncodedPass)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(EncodedPass);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decode_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decode_char, 0);
            string result = new string(decode_char);
            return result;
        }


        UserDAL udal = new UserDAL();

        public string Create(User u, UserType ut)
        {
            u.Password = Encode(u.Password);
            return udal.Create(u, ut);
        }

        public bool IsRegistered()
        {
            return udal.IsRegistered();
        }

        public DataTable Read()
        {
            return udal.Read();
        }

        public User Read(int id)
        {
            return udal.Read(id);
        }

        public string Update(User u, int id)
        {
            u.Password = Encode(u.Password);
            return udal.Update(u, id);
        }

        public string Delete(int id)
        {
            return udal.Delete(id);
        }

        public User SearchU(string s)
        {
            return udal.SearchU(s);
        }

        public List<string> ReadUserNames()
        {
            return udal.ReadUserNames();
        }

        public User Login(string u, string p)
        {
            return udal.Login(u, Encode(p));
        }

        public bool Access(User u, String s, int a)
        {
            return udal.Access(u, s, a);
        }

        public List <User> Readnvoices()
        {
            return udal.ReadInvoices();
        }

        public DataTable SearchUser(string s)
        {
            return udal.SearchUser(s);
        }

        public string UsersCount()
        {
             return udal.UsersCount();
        }
    }
}
