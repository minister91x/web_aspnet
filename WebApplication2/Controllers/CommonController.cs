using DataAccess.Student.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopupPartial(string msg)
        {
            ViewBag.Mesenger = msg;

            return PartialView();
        }

        public ActionResult MenuPartial()
        {
            var lstFunction = new List<UserFunctionByUserID>();
            try
            {
                //lấy session
                var accountLogin = Session[Libs.Config.SessionAccount] != null
                    ? (AccountDTO)Session[Libs.Config.SessionAccount] : new AccountDTO();
                if (accountLogin.UserId <= 0)
                {
                    //Nếu mà object chưa có giá trị thì chứng tỏ là chưa có tài khoản nào đăng nhập
                    return RedirectToAction("FormLogin", "Home");
                }

                var UserId = accountLogin.UserId;
                lstFunction = new DataAccess.Student.DAOImpl.FunctionsDAOImpl().ListFunctionByUserID(UserId);

            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView(lstFunction);
        }
    }
}