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
            // Model để truyền dữ liệu ra ngoài vieww
            var model = new List<DataAccess.Student.DTO.StudentClassDTO>();
            try
            {
                model = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_GetList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView(model);
        }

        public ActionResult Detail(string MalopInput)
        {
            var model = new DataAccess.Student.DTO.StudentClassDTO();
            try
            {
                //var list = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_GetList();
                ////list.Where(s => s.MaLop == MalopInput) lọc trong list những object mà có MaLop == MalopInput 
                //model = list.Count > 0 ? list.Where(s => s.MaLop == MalopInput).FirstOrDefault() : new  DataAccess.Student.DTO.StudentClassDTO();
                if (!string.IsNullOrEmpty(MalopInput))
                {
                    model = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_GetDetail(MalopInput);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(model);
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

        public JsonResult StudentClassInsertUpdate(int IsUpdate, string MaLop, string TenLop)
        {
            var returnData = new ReturnData();
            try
            {
                if (string.IsNullOrEmpty(MaLop))
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Mã lớp không được trống!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(TenLop))
                {
                    returnData.ResponseCode = -601;
                    returnData.Description = "Tên lớp không được trống!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                var result = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_InsertUpdate(IsUpdate, MaLop, TenLop);
                if (result > 0)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = IsUpdate == 1 ? "Cập nhật thành công" : "Thêm thành công !";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    switch (result)
                    {
                        case -1:
                            returnData.ResponseCode = -1;
                            returnData.Description = "Không tồn tại mã lớp trên hệ thống";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                        default:
                            returnData.ResponseCode = -99;
                            returnData.Description = "Hệ thống đang bận";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public JsonResult StudentClassDelete(string MalopInput)
        {
            var returnData = new ReturnData();
            try
            {
                if (string.IsNullOrEmpty(MalopInput))
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Mã lớp cần xóa không được trống!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }


                var result = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClas_Delete(MalopInput.Trim());
                if (result > 0)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Xóa thành công !";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    switch (result)
                    {
                        case -1:
                            returnData.ResponseCode = -1;
                            returnData.Description = "Không tồn tại mã lớp trên hệ thống";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                        default:
                            returnData.ResponseCode = -99;
                            returnData.Description = "Hệ thống đang bận";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}