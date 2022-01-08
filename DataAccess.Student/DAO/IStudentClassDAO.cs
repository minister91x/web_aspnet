using DataAccess.Student.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Student.DAO
{
    public interface IStudentClassDAO
    {
        int StudentClass_InsertUpdate(int IsUdpate, string MaLopInput, string stTenLOPInput);
        ListStudentClassResponse StudentClass_GetList(string MaLopInput, int CurrPage, int RecordPerPage);
        
       
        StudentClassDTO StudentClass_GetDetail(string MaLopInput);
        int StudentClas_Delete(string MaLopInput);
    }
}
