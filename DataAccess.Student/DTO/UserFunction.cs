using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Student.DTO
{
    public class UserFunction
    {
        public int UserID { get; set; }
        public int FunctionID { get; set; }
        public int IsView { get; set; }
        public int IsInsert { get; set; }
        public int IsUpdate { get; set; }
        public int IsDelete { get; set; }
    }

    public class Function_ByUserID
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public int IsView { get; set; }
        public int IsInsert { get; set; }
        public int IsUpdate { get; set; }
        public int IsDelete { get; set; }
    }
}
