using Microsoft.AspNetCore.Mvc;
using StudentRecordMS.Services;

namespace StudentRecordMS.Controllers
{
    public class StudentsController : Controller
    {
        // Field
        private readonly IStudentServices _studentServices;

        // DI
        public StudentsController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        #region Student Dashboard
        public IActionResult Index()
        {
            if (Request.Cookies["RoleId"] != "2")
                return RedirectToAction("Login", "Logins");

            int userId = Convert.ToInt32(Request.Cookies["UserId"]);
            var student = _studentServices.GetStudentByUserId(userId);

            return View(student);
        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("RoleId");
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Login", "Logins");
        }
        #endregion
    }
}
