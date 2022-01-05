using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var student = new List<DataAccess.Student.StudentDTO>();
            for (int i = 0; i < 10; i++)
            {
                student.Add(new DataAccess.Student.StudentDTO { ID = i + 1, Name = "Name" + i });
            }
            return View(student);
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DemoPartialView()
        {
            return PartialView();
        }

        public ActionResult Detail(int? id, string Name)
        {
            //ViewBag.ID = id;
            //ViewBag.Name = Name;
            var student = new DataAccess.Student.StudentDTO();
            var student_impl = new DataAccess.Student.DAOImpl.StudentImpl();

            student_impl.Student_Insert(student);
            try
            {
                student.ID = Convert.ToInt32(id);
                student.Name = Name;
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(student);
        }

        public JsonResult Login(string UserName, string Password)
        {
            var returnData = new ReturnData();
            try
            {
                var acc_impl = new DataAccess.Student.DAOImpl.AccountImpl();

                var result = acc_impl.Account_Login(UserName, Password);
                returnData.ResponseCode = result;
                if (result > 0)
                {
                    returnData.Description = "Login Thành công!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.Description = "Login thất bại";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                returnData.ResponseCode = -999;
                returnData.Description = "Hệ thống đang bận.Vui lòng quay lại sau!";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }


        }
    }
}