using Microsoft.AspNetCore.Mvc;
using StudentRecordMS.Models;
using StudentRecordMS.Services;

namespace StudentRecordMS.Controllers
{
    public class InvigilatorsController : Controller
    {
        // Field
        private readonly IStudentServices _studentServices;

        // DI
        public InvigilatorsController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        #region Dashboard
        public IActionResult Index()
        {
            if (Request.Cookies["RoleId"] != "1")
                return RedirectToAction("Login", "Logins");

            return View();
        }
        #endregion

        #region List All Students
        [HttpGet]
        public IActionResult List(string searchTerm)
        {
            if (Request.Cookies["RoleId"] != "1")
                return RedirectToAction("Login", "Logins");

            var students = _studentServices.GetAllStudents();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                students = students.Where(s =>
                    s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    s.RollNumber.Contains(searchTerm)).ToList();
            }

            ViewBag.SearchTerm = searchTerm;
            return View(students);
        }
        #endregion

        #region Create Student -- HttpGet
        [HttpGet]
        public IActionResult Create()
        {
            if (Request.Cookies["RoleId"] != "1")
                return RedirectToAction("Login", "Logins");

            ViewBag.NextRollNumber = _studentServices.GetNextRollNumber();
            return View();
        }
        #endregion

        #region Create Student -- HttpPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    student.RollNumber = _studentServices.GetNextRollNumber();
                    _studentServices.AddStudent(student);
                    TempData["SuccessMessage"] = "Student record created successfully!";
                    return RedirectToAction("List");
                }
                ViewBag.NextRollNumber = _studentServices.GetNextRollNumber();
                return View(student);
            }
            catch (Exception)
            {
                return View(student);
            }
        }
        #endregion

        #region Edit Marks -- HttpGet
        [HttpGet]
        public IActionResult Edit(string rollNumber)
        {
            if (Request.Cookies["RoleId"] != "1")
                return RedirectToAction("Login", "Logins");

            Student student = _studentServices.GetStudentByRollNumber(rollNumber);
            if (student == null) return NotFound();

            return View(student);
        }
        #endregion

        #region Edit Marks -- HttpPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _studentServices.UpdateStudentMarks(student);
            TempData["SuccessMessage"] = "Marks updated successfully!";
            return RedirectToAction("List");
        }
        #endregion

        #region Delete Student -- HttpPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string rollNumber)
        {
            _studentServices.DeleteStudent(rollNumber);
            TempData["SuccessMessage"] = "Student record deleted.";
            return RedirectToAction("List");
        }
        #endregion

        #region View Single Student
        [HttpGet]
        public IActionResult Details(string rollNumber)
        {
            if (Request.Cookies["RoleId"] != "1")
                return RedirectToAction("Login", "Logins");

            Student student = _studentServices.GetStudentByRollNumber(rollNumber);
            if (student == null) return NotFound();

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
