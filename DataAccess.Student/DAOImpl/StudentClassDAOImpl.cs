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
    public class StudentClassDAOImpl : IStudentClassDAO
    {
        public StudentClassDTO StudentClass_GetDetail(string MaLopInput)
        {
            var model = new StudentClassDTO();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("SP_ClassGetDetail", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_MaLopInput", MaLopInput);
                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    model = new StudentClassDTO
                    {
                        MaLop = read["MaLop"].ToString(),
                        TenLOP = read["TenLOP"].ToString(),
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }

        public ListStudentClassResponse StudentClass_GetList(string MaLopInput, int CurrPage, int RecordPerPage)
        {
            var model = new ListStudentClassResponse();
            var list = new List<StudentClassDTO>();
            var totalRecord = 0;
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("SP_ClassGetList", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandTimeout = 120;

                cmd.Parameters.AddWithValue("@_MaLop", MaLopInput);
                cmd.Parameters.AddWithValue("@_CurrPage", CurrPage);
                cmd.Parameters.AddWithValue("@_RecordPerPage", RecordPerPage);
                cmd.Parameters.Add("@_TotalRecord", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    list.Add(new StudentClassDTO
                    {
                        MaLop = read["MaLop"].ToString(),
                        TenLOP = read["TenLOP"].ToString(),
                    });
                }

                if (!read.IsClosed)
                {
                    read.Close();
                }

                totalRecord = cmd.Parameters["@_TotalRecord"].Value != null ? Convert.ToInt32(cmd.Parameters["@_TotalRecord"].Value) : 0;

                model.listClass = list;
                model.TotalRecord = totalRecord;
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }

        public int StudentClass_InsertUpdate(int IsUdpate, string MaLopInput, string stTenLOPInput)
        {
            var result = 0;
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("SP_ClassInsertUpdate", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // thêm giá trị vào cho các tham số 
                cmd.Parameters.AddWithValue("@_IsUdpate", IsUdpate);
                cmd.Parameters.AddWithValue("@_MaLopInput", MaLopInput);
                cmd.Parameters.AddWithValue("@_TenLOPInput", stTenLOPInput);

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

        public int StudentClas_Delete(string MaLopInput)
        {
            var result = 0;
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                //ALT +F10
                SqlCommand cmd = new SqlCommand("SP_ClassDelete", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // thêm giá trị vào cho các tham số 
                cmd.Parameters.AddWithValue("@_MaLopInput", MaLopInput);
                cmd.Parameters.Add("@_ResponseStatus", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

                // khi insert update thì dùng lệnh ExecuteNonQuery
                cmd.ExecuteNonQuery();

                result = cmd.Parameters["@_ResponseStatus"].Value != null ? Convert.ToInt32(cmd.Parameters["@_ResponseStatus"].Value) : 0;

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
