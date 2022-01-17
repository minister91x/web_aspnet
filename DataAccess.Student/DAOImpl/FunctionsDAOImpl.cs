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
    public class FunctionsDAOImpl : IFunctionsDAO
    {
        public List<Functions> FunctionsGetList()
        {

            var model = new List<Functions>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("SP_FUNCTION_GetList", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    model.Add(new Functions
                    {
                        FunctionID = Convert.ToInt32(read["FunctionID"].ToString()),
                        FunctionName = read["FunctionName"].ToString(),
                        ActionName = read["ActionName"].ToString(),
                        Url = read["Url"].ToString(),
                    });
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
