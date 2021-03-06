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
        int UserFunction_InSert(UserFunction userFunction);

        List<UserFunctionByUserID> ListUserFunctionByUserID(int UserID);

        List<Function_ByUserID> GrantUser_ListFunctionByUserID(int UserID);
    }
}
