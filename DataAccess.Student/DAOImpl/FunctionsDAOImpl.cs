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



        public int UserFunction_InSert(UserFunction userFunction)
        {
            var result = 0;
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("SP_UserFunction_INSERT", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // thêm giá trị vào cho các tham số 
                cmd.Parameters.AddWithValue("@_UserID", userFunction.UserID);
                cmd.Parameters.AddWithValue("@_FunctionID", userFunction.FunctionID);
                cmd.Parameters.AddWithValue("@_IsView", userFunction.IsView);
                cmd.Parameters.AddWithValue("@_IsInsert", userFunction.IsInsert);
                cmd.Parameters.AddWithValue("@_IsUpdate", userFunction.IsUpdate);
                cmd.Parameters.AddWithValue("@_IsDelete", userFunction.IsDelete);
                cmd.Parameters.Add("@_ResponseStatus", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

                // khi insert update thì dùng lệnh ExecuteNonQuery
                cmd.ExecuteNonQuery();

                result = cmd.Parameters["@_ResponseStatus"].Value != null ? Convert.ToInt32(cmd.Parameters["@_ResponseStatus"].Value) : 0;

                return result;
            }
            catch (Exception ex)
            {
                result = -969;
            }

            return result;
        }

        public List<UserFunctionByUserID> ListFunctionByUserID(int UserID)
        {
            var model = new List<UserFunctionByUserID>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("SP_UserFunction_Getlist", sqlconn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter("@_UserID", UserID);
                cmd.Parameters.Add(parameter);

                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    model.Add(new UserFunctionByUserID
                    {
                        FunctionName = read["FunctionName"].ToString(),
                        ActionName = read["ActionName"].ToString(),
                        Url = read["Url"].ToString(),
                    });
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return model;
        }

        public List<UserFunctionByUserID> ListUserFunctionByUserID(int UserID)
        {
            throw new NotImplementedException();
        }

        public List<Function_ByUserID> GrantUser_ListFunctionByUserID(int UserID)
        {
            var model = new List<Function_ByUserID>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("SP_GrantUser_GetlistFunction_ByUserID", sqlconn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter("@_UserID", UserID);
                cmd.Parameters.Add(parameter);

                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    model.Add(new Function_ByUserID
                    {
                        FunctionName = read["FunctionName"].ToString(),
                        FunctionID = Convert.ToInt32(read["FunctionID"].ToString()),
                        IsDelete = Convert.ToBoolean(read["IsDelete"].ToString()) ? 1 : 0,
                        IsInsert = Convert.ToBoolean(read["IsInsert"].ToString()) ? 1 : 0,
                        IsUpdate = Convert.ToBoolean(read["IsUpdate"].ToString()) ? 1 : 0,
                        IsView = Convert.ToBoolean(read["IsView"].ToString()) ? 1 : 0
                    });
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return model;
        }


    }
}
