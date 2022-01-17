using DataAccess.Student.DAO;
using DataAccess.Student.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Student.DAOImpl
{
    public class AccountImpl : IAccount
    {
        public List<AccountDTO> AccountDTOGetList()
        {
            var model = new List<AccountDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("SP_GetListUser", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

               
                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    model.Add(new AccountDTO
                    {
                        UserId = Convert.ToInt32(read["UserId"].ToString()),
                        UserName = read["UserName"].ToString(),
                        FullName = read["FullName"].ToString(),
                        IsAdmin = Convert.ToBoolean(read["IsAdmin"].ToString()),
                    });
                }


            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }

        public int Account_Login(string UserName, string Password)
        {
            var result = 0;
            try
            {
                // xử lý gọi Database 
                //Bước 1 : Mở connectionstring
                var sqlconn = ConnectDB.GetSqlConnection();
                //Bước 2 : gọi Store procedure 
                // Bước 2.1 SqlCommand để gọi store 
                SqlCommand cmd = new SqlCommand("Sp_UserLogin", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_UserName", UserName);
                cmd.Parameters.AddWithValue("@_Password", Password);
                cmd.Parameters.Add("@_ResponseCode", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
                //Bước 3: Đọc dữ liệu từ store trả về 
                cmd.ExecuteNonQuery();

                result = cmd.Parameters["@_ResponseCode"].Value != null ? Convert.ToInt32(cmd.Parameters["@_ResponseCode"].Value) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public AccountDTO GetAccountByID(int UserID)
        {
            var model = new AccountDTO();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("Account_GetByID", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_UserID", UserID);
                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    model = new AccountDTO
                    {
                        UserId = Convert.ToInt32(read["UserId"].ToString()),
                        UserName = read["UserName"].ToString(),
                        FullName = read["FullName"].ToString(),
                        IsAdmin = Convert.ToBoolean(read["IsAdmin"].ToString()),
                    };
                }


            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }
    }
}
