using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Student.DTO
{
    public class StudentClassDTO
    {
        public string MaLop { get; set; }
        public string TenLOP { get; set; }
    }

    public class ListStudentClassResponse
    {
        public List<StudentClassDTO> listClass { get; set; }
        public int TotalRecord { get; set; }
        //public int TotalAmount { get; set; }
    }
}
