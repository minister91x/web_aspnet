using DataAccess.Student.DTO;
using OfficeOpenXml;
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
            //try
            //{
            //    //lấy session
            //    var accountLogin = Session[Libs.Config.SessionAccount] != null
            //        ? (AccountDTO)Session[Libs.Config.SessionAccount] : new AccountDTO();
            //    if (accountLogin.UserId <= 0)
            //    {
            //        //Nếu mà object chưa có giá trị thì chứng tỏ là chưa có tài khoản nào đăng nhập
            //        return RedirectToAction("FormLogin", "Home");
            //    }

            //    ViewBag.IsAdmin = accountLogin.IsAdmin;
            //    ViewBag.FullName = accountLogin.FullName;

            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
            return View();
        }

        public ActionResult Index1(string UserName)
        {
            return View();
        }

        public ActionResult PartiaViewDemo()
        {
            var model = new List<DemoModels>();
            for (int i = 0; i < 10; i++)
            {
                model.Add(new DemoModels { Id = i, Name = "Số " + i });

            }

            var shipper = new Shipper();

            shipper.BoxOfQuan = 10000;

            ViewBag.Name = "QUÂN";
            return PartialView(shipper);
        }

        public ActionResult FormLogin()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("FormLogin", "Home");
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

        #region Class
        public ActionResult DemoPartialView(string MaLopInPut, int CurrentPage, int RecordPerPage)
        {
            // Model để truyền dữ liệu ra ngoài vieww
            var model = new List<DataAccess.Student.DTO.StudentClassDTO>();
            var TotalPage = 1;
            try
            {

                for (int i = 1; i <= 10; i++)
                {
                    model.Add(new StudentClassDTO { MaLop = "Class00" + i, TenLOP = "Lớp học ASPNET MVC " + i });
                }
                //var modelresponse = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_GetList(MaLopInPut, CurrentPage, RecordPerPage);
                //if (modelresponse.TotalRecord > 0)
                //{
                //    model = modelresponse.listClass;
                //    TotalPage = modelresponse.TotalRecord % RecordPerPage == 0 ? modelresponse.TotalRecord / RecordPerPage : (modelresponse.TotalRecord / RecordPerPage) + 1;
                //}

                ViewBag.CurrentPage = CurrentPage;
                ViewBag.PageSize = RecordPerPage;
                ViewBag.TotalPage = TotalPage;
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
                    //model = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_GetDetail(MalopInput);
                    model.MaLop = MalopInput;

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

                var pass_encrypt = Libs.Security.MD5(Password);

                var result = acc_impl.Account_Login(UserName, pass_encrypt);
                returnData.ResponseCode = result;
                if (result > 0)
                {
                    var currentUser = acc_impl.GetAccountByID(result);

                    Session.Add(Libs.Config.SessionAccount, currentUser);

                    //Session.Timeout = 120;

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

        #endregion


        #region ManagerStudent

        // Comment thì dùng phím CTRL+K+C 
        // Uncomment thì dùng CTRL+K+D 
        public ActionResult ManagerStudent()
        {
            return View();
        }


        public ActionResult StudentInsertUpdate(int? StudentID)
        {
            var model = new DataAccess.Student.DTO.StudentDTO();
            try
            {
                var lstClass = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_GetList(string.Empty, 1, 10000);
                ViewBag.lstClass = lstClass.listClass;
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(model);
        }

        public JsonResult LoadClassData()
        {
            var listClass = new List<StudentClassDTO>();
            try
            {
                var result = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_GetList(string.Empty, 1, 10000);
                listClass = result.listClass;


            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(listClass, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public void ExportExcel()
        {
            var model = new List<DataAccess.Student.DTO.StudentClassDTO>();
            var TotalPage = 1;
            try
            {
                //Bước 1: GetData
                var modelresponse = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_GetList(string.Empty, 1, 10000);
                model = modelresponse.listClass;
                //Bước 2: 
                ExcelPackage ep = new ExcelPackage();
                ExcelWorksheet Sheet = ep.Workbook.Worksheets.Add("Report");
                Sheet.Cells["A1"].Value = "Mã Lớp";
                Sheet.Cells["B1"].Value = "Tên Lớp";
                int row = 2;// dòng bắt đầu ghi dữ liệu
                var StatusName = "";

                var str = string.Format("TEXT {0}{1}", "MyClass", "6A1");
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.MaLop;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.TenLOP;
                        row++;
                    }
                }
                Sheet.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + "Report.xlsx");
                Response.BinaryWrite(ep.GetAsByteArray());
                Response.End();


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public JsonResult UpdateDiemHSByFile()
        {
            var index_fail = "";
            var returnData = new ReturnData();
            try
            {
                HttpPostedFileBase excelFile = Request.Files["UploadedFile"];

                if (excelFile == null)
                {
                    returnData.ResponseCode = -3;
                    returnData.Description = "File dữ không được trống!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                var package = new ExcelPackage(excelFile.InputStream);

                ExcelWorksheet ws = package.Workbook.Worksheets[1];

                var listVoucherInfo = string.Empty;

                for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                {
                    if (ws.Cells[rw, 1].Value != null
                        && ws.Cells[rw, 2].Value != null)
                    {
                        var malop = ws.Cells[rw, 1].Value != null ? ws.Cells[rw, 1].Value.ToString() : string.Empty;
                        if (malop == null)
                        {
                            returnData.ResponseCode = -31;
                            returnData.Description = "Mã lớp ở dòng thứ " + rw + "Không hợp lệ";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                        }
                        var tenlop = ws.Cells[rw, 2].Value != null ? ws.Cells[rw, 2].Value.ToString() : string.Empty;

                        if (tenlop == null)
                        {
                            returnData.ResponseCode = -31;
                            returnData.Description = "Tên lớp ở dòng thứ " + rw + "Không hợp lệ";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                        }


                        var result = new DataAccess.Student.DAOImpl.StudentClassDAOImpl().StudentClass_InsertUpdate(0, malop, tenlop);
                        if (result <= 0)
                        {
                            var err_des = "";
                            switch (result)
                            {
                                case -1:
                                    err_des = "Mã Lớp sinh đã tồn tại"; break;
                                default:
                                    err_des = "Lỗi không xác định"; break;
                            }

                            index_fail += "Dòng :" + rw + "_" + "Lỗi:" + err_des + ",";
                        }

                        // var result = new DataAcess.StudentAPI.DALImpl.DiemHSImpl().DiemHs_Update(obj);
                    }
                }

                if (string.IsNullOrEmpty(index_fail))
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Thêm mới thành công";
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Lỗi tại các dòng :" + index_fail;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }


                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}