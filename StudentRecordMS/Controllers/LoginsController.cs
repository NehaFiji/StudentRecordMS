using Microsoft.AspNetCore.Mvc;
using StudentRecordMS.Services;
using StudentRecordMS.ViewModel;

namespace StudentRecordMS.Controllers
{
    public class LoginsController : Controller
    {
        // Field
        private readonly IUserServices _userServices;

        // DI
        public LoginsController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginVModel)
        {
            if (ModelState.IsValid)
            {
                var availableUser = _userServices.AuthenticateUserNameAndPassword(loginVModel.UserName, loginVModel.UserPassword);

                if (availableUser != null)
                {
                    Response.Cookies.Append("UserId", availableUser.UserId.ToString(),
                        new CookieOptions { Expires = DateTime.Now.AddHours(1) });
                    Response.Cookies.Append("UserName", availableUser.UserName.ToString(),
                        new CookieOptions { Expires = DateTime.Now.AddHours(1) });
                    Response.Cookies.Append("RoleId", availableUser.RoleId.ToString(),
                        new CookieOptions { Expires = DateTime.Now.AddHours(1) });

                    TempData["SuccessMessage"] = $"Welcome, {availableUser.UserName}!";

                    return RedirectToRoleBasedDashboard(availableUser.RoleId.Value);
                }
                TempData["ErrorMessage"] = "Invalid username or password.";
            }
            return View(new LoginViewModel());
        }

        private ActionResult RedirectToRoleBasedDashboard(int roleId)
        {
            switch (roleId)
            {
                case 1: return RedirectToAction("Index", "Invigilators");
                case 2: return RedirectToAction("Index", "Students");
                default: return RedirectToAction("Login", "Logins");
            }
        }
    }
}
