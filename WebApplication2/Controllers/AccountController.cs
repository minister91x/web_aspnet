using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult ManagerUser()
        {
            return View();
        }

        public ActionResult ListUserPartial()
        {
            var model = new List<DataAccess.Student.DTO.AccountDTO>();
            try

            {
                model = new DataAccess.Student.DAOImpl.AccountImpl().AccountDTOGetList();
            }
            catch (Exception ex)
            {

                throw;

            }

            return PartialView(model);
        }

        public ActionResult GrantUser(int UserIDInput)
        {
            var model = new List<DataAccess.Student.DTO.Functions>();
            try

            {
                model = new DataAccess.Student.DAOImpl.FunctionsDAOImpl().FunctionsGetList();
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return View(model);
        }
    }
}