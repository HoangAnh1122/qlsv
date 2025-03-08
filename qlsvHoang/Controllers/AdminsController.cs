using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qlsvHoang.Data;
using qlsvHoang.Models;
using qlsvHoang.Service.FacadePattern;
using qlsvHoang.Service.IService;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Controllers
{
    public class AdminsController : Controller
    {

        private readonly StudentFacade facade;
        private readonly qlsvHoangContext _context;
        private readonly ITeacherService service;
        private readonly IAdminService adminService;

        public AdminsController(qlsvHoangContext context, ITeacherService service, IAdminService adminService)
        {
            _context = context;
            this.service = service;
            this.adminService = adminService;
            facade = new StudentFacade(context);

        }

        #region Admin

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateAdmin(AdminVM adminVM)
        {
            try
            {
                //mapper adminvm to admin

                var admindo = Adapter.AdminAdapter.toAdminDO(adminVM);
                admindo.Password = Common.Security.Hash(admindo.Password);
                var res = await adminService.CreateAdmin(admindo);
                if (res != null)
                {
                    TempData["ok"] = "Create Admin Successful!";
                    return RedirectToAction("ListAdmin", "Admins");
                }
                ViewBag.ErrorMessage = "Username already exists. Please choose a different username.";
                TempData["no"] = "Username already exists. Please choose a different username.";
                return View();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        public async Task<ActionResult> ListAdmin()
        {
            try
            {
                var res = await adminService.GetAllAdmins();
                return View(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult> EditAdmin(int id)
        {
            try
            {
                var res = await adminService.getAdminById(id);
                if (res != null)
                {
                    return View(res);

                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditAdmin(UpdateAdminVM admin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await adminService.UpdateAdmin(admin);
                    if (res != 1)
                    {
                        ViewBag.ErrorMessage = "ADmin Not Found ";
                        TempData["no"] = "ADmin Not Found";
                        return View();
                    }
                    TempData["ok"] = "Update  ADmin Successful!";
                    return RedirectToAction("ListAdmin", "Admins");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> DeleteAdmin(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res=await adminService.DeleteAdmin(id);
                    if(res != 1)
                    {
                        TempData["no"] = "Delete Faild";
                        ViewBag.ErrorMessage = "ADmin Not Found ";
                        return RedirectToAction("ListAdmin", "Admins");
                    }
                    TempData["ok"] = "Delete  ADmin Successful!";
                    return RedirectToAction("ListAdmin", "Admins");
                }
                TempData["no"] = "Delete Faild";
                return View();
            }
            catch(Exception ex)
            {
                throw ex;

            }
        }
        //Admin Login

        #endregion



        #region Student
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View("~/Views/Admins/CreateStudent.cshtml");
        }
        [HttpPost]
        public async Task<ActionResult> CreateStudent(StudentVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingStudent = await facade.GetStudentByUserName(model.Username);
                    if (existingStudent != null)
                    {
                        ViewBag.ErrorMessage = "Username already exists. Please choose a different username.";
                        TempData["no"] = "Username already exists. Please choose a different username.";
                        return View();
                    }
                    //map to do
                    var student = Adapter.StudentAdapter.toStudentDO(model);

                    student.Password = Common.Security.Hash(model.Password);
                    student.RoleId = model.RoleId;
                    facade.AddStudent(student);
                    TempData["ok"] = "Create Student Successful!";
                    return RedirectToAction("ListStudent", "Admins");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                    // Log the error (uncomment ex variable name and write a log.)
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ListStudent()
        {
            try
            {
                var res = facade.GetAllStudents();
                return View(res);

            }
            catch (Exception ex)
            {
                var mess = $"{ex.Message} ";

            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> EditStudent(int id)
        {
            try
            {
                var res = await facade.GetStudentById(id);
                if (res == null)
                {
                    ViewBag.ErrorMessage = "No Data ";
                    return View();
                }
                return View(res);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditStudent(EditStudentVM student)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    //check exit student
                    var exitStudent = await facade.GetStudentById(student.StudentId);
                    if (exitStudent == null)
                    {
                        ViewBag.ErrorMessage = "Student ID not Found";
                        TempData["no"] = "Student ID not Found.";
                        return View();
                    }
                    var mapdata = Adapter.StudentAdapter.toUpdateStudentoDO(student);
                    mapdata.Password = exitStudent.Password;

                    var res = await facade.UpdateStudent(mapdata);
                    if (res == 0)
                    {
                        ViewBag.ErrorMessage = "Username already exists. Please choose a different username.";
                        TempData["no"] = "Username already exists. Please choose a different username.";
                        return View();
                    }

                    TempData["ok"] = "Update Student Successful!";
                    return RedirectToAction("ListStudent", "Admins");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                var res = await facade.DeleteStudent(id);
                if (res == -1)
                {

                    ViewBag.ErrorMessage = "Student not found .";
                    TempData["no"] = "Student not found. Please choose a different Teacher.";
                    return RedirectToAction("ListTeacher", "Admins");
                }

                TempData["ok"] = "Delete Successful !";
                return RedirectToAction("ListStudent", "Admins");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion




        #region Teacher

        [HttpGet]
        public async Task<ActionResult> ListTeacher()
        {
            try
            {
                var res = await service.getListTeachers();

                return View(res);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult CreateTeacher()
        {

            return View();

        }
        [HttpPost]
        public async Task<ActionResult> CreateTeacher(TeacherVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    model.Password = Common.Security.Hash(model.Password);
                    var res = await service.createTeacher(model);
                    if (res == 0)
                    {

                        ViewBag.ErrorMessage = "Username already exists. Please choose a different username.";
                        TempData["no"] = "Username already exists. Please choose a different username.";
                        return View();
                    }
                    TempData["ok"] = "Create Teacher Successful!";
                    return RedirectToAction("ListTeacher", "Admins");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                    // Log the error (uncomment ex variable name and write a log.)
                }
            }
            return View();

        }

        [HttpGet]
        public async Task<ActionResult> EditTeacher(int id)
        {
            var res = await service.findTeacherById(id);
            if (res == null)
            {
                ViewBag.ErrorMessage = "Username already exists. Please choose a different username.";
                TempData["no"] = "Username already exists. Please choose a different username.";
                return View();
            }
            return View(res);

        }
        [HttpPost]
        public async Task<ActionResult> EditTeacher(UpdateTeacherVM teacher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await service.updateTeacher(teacher);
                    if (res == 0)
                    {
                        ViewBag.ErrorMessage = "Teacher not found .";
                        TempData["no"] = "Teacher not found. Please choose a different Teacher.";
                        return View();

                    }
                    TempData["ok"] = "Update Teacher Successful!";
                    return RedirectToAction("ListTeacher", "Admins");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            try
            {
                var res = await service.deleteTeacher(id);
                if (res == -1)
                {

                    ViewBag.ErrorMessage = "Teacher not found .";
                    TempData["no"] = "Teacher not found. Please choose a different Teacher.";
                    return RedirectToAction("ListTeacher", "Admins");
                }

                TempData["ok"] = "Delete Successful !";
                return RedirectToAction("ListTeacher", "Admins");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion




    }
}
