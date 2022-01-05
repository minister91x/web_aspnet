using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Student
{
    public static class ConnectDB
    {
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection sqlconn = null;
            try
            {
                var connectName = "Server=AD-PC\\SQLEXPRESS;Database=QLHS;User Id=sa;Password=12345;";
                sqlconn = new SqlConnection(connectName);
                if (sqlconn.State == System.Data.ConnectionState.Open)
                {
                    sqlconn.Close();
                }

                sqlconn.Open();
            }
            catch (Exception ex)
            {

                throw;
            }

            return sqlconn;

        }
    }

}
