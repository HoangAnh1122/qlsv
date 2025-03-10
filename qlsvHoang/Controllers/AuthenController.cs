using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlsvHoang.Data;
using qlsvHoang.Models;
using qlsvHoang.Service.FacadePattern;
using qlsvHoang.Service.IService;
using qlsvHoang.ViewModel;
using System.Security.Claims;

namespace qlsvHoang.Controllers
{
    public class AuthenController : Controller
    {
        private readonly ITeacherService teacherService;
        private readonly IAdminService adminService;
        private readonly StudentFacade facade;
        private readonly qlsvHoangContext _context;

        public AuthenController(ITeacherService teacherService, IAdminService adminService, qlsvHoangContext context)
        {
            this.teacherService = teacherService;
            this.adminService = adminService;
            _context = context;
            facade = new StudentFacade(context);
        }
        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> LoginAdmin(AdminLoginVM adminLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var res = await adminService.LoginAdmin(adminLogin);
                    if (res != null)
                    {
                        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == res.RoleId);
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, res.AdminId.ToString()),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Name,res.Name),
                    };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        TempData["ok"] = "Login Successful !";
                        return RedirectToAction("DashBoard", "Admins");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid username or password.";
                        TempData["no"] = "Login Failed !";
                        return View();
                    }
                }
                return View();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public IActionResult LoginStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginStudent(LoginStudentVM loginStudentVM)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var login = await facade.loginStudent(loginStudentVM);
                    if (login != null)
                    {
                        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == login.RoleId);
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, login.StudentId.ToString()),
                        new Claim(ClaimTypes.Role, "Student"),
                        new Claim(ClaimTypes.Name,login.Name),
                    };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        TempData["ok"] = "Login Successful !";
                        return RedirectToAction("DashBoard", "Students");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid username or password.";
                        TempData["no"] = "Login Failed !";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpGet]
        public IActionResult LoginTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> LoginTeacher(LoginTeacherVM loginTeacherVM)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var login = await teacherService.loginTeacher(loginTeacherVM);
                    if (login != null)
                    {
                        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == login.RoleId);
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, login.TeacherId.ToString()),
                        new Claim(ClaimTypes.Role, "Teacher"),
                        new Claim(ClaimTypes.Name,login.Name),
                    };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        TempData["ok"] = "Login Successful !";
                        return RedirectToAction("DashBoard", "Teachers");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid username or password.";
                        TempData["no"] = "Login Failed !";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<IActionResult> Logout()

        {
            await HttpContext.SignOutAsync();
            TempData["ok"] = "See you again!";
            return Redirect("/");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
