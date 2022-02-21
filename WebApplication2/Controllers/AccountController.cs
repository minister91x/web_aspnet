using DataAccess.Student.DTO;
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

        public JsonResult SetGrantUser(int UserID, string lstFunctionID, string lstIsView, string lstIsInsert, string lstIsUpdate, string lstIsDelete)
        {
            var returnData = new WebApplication2.Models.ReturnData();
            try
            {
                if (string.IsNullOrEmpty(lstFunctionID)
                    || string.IsNullOrEmpty(lstIsView)
                    || string.IsNullOrEmpty(lstIsInsert)
                    || string.IsNullOrEmpty(lstIsUpdate)
                    || string.IsNullOrEmpty(lstIsDelete))
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Dữ liệu đầu vào không hợp lệ!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var result = 1;
                var res = string.Empty;

                var lengthItem = lstFunctionID.Split('_').Length;
                for (int i = 0; i < lengthItem; i++)
                {
                    var userFunction = new UserFunction();
                    userFunction.UserID = UserID;
                    userFunction.FunctionID = lstFunctionID.Split('_')[i] != null ? Convert.ToInt32(lstFunctionID.Split('_')[i]) : 0;
                    userFunction.IsView = lstIsView.Split('_')[i] != null ? Convert.ToInt32(lstIsView.Split('_')[i]) : 0;
                    userFunction.IsInsert = lstIsInsert.Split('_')[i] != null ? Convert.ToInt32(lstIsInsert.Split('_')[i]) : 0;
                    userFunction.IsUpdate = lstIsUpdate.Split('_')[i] != null ? Convert.ToInt32(lstIsUpdate.Split('_')[i]) : 0;
                    userFunction.IsDelete = lstIsDelete.Split('_')[i] != null ? Convert.ToInt32(lstIsDelete.Split('_')[i]) : 0;
                    result = new DataAccess.Student.DAOImpl.FunctionsDAOImpl().UserFunction_InSert(userFunction);
                    if (result < 0)
                    {
                        res = "lỗi ở vị trí :" + (i + 1);
                    }
                }

                if (string.IsNullOrEmpty(res))
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Phân quyền thành công!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = res;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}
