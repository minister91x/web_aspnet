using DataAccess.Student.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Student.DAO
{
   public interface IStudent
    {
        int Student_Insert(StudentDTO stu);
        int Student_Update(StudentDTO stu);
        int Student_Delele(int Id);
    }
}
