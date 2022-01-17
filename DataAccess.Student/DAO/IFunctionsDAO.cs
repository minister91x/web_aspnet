using DataAccess.Student.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Student.DAO
{
   public interface IFunctionsDAO
    {
        List<Functions> FunctionsGetList();
    }
}
