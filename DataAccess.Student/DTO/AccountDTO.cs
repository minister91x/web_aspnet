using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Student.DTO
{
    public class AccountDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public bool IsAdmin { get; set; }
    }
}
