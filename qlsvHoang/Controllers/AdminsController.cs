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

        public AdminsController(qlsvHoangContext context, ITeacherService service)
        {
            _context = context;
            this.service = service;
            facade = new StudentFacade(context);

        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            var qlsvHoangContext = _context.Admins.Include(a => a.Role);
            return View(await qlsvHoangContext.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }
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
                    var exitStudent=await facade.GetStudentById(student.StudentId);
                    if (exitStudent == null)
                    {
                        ViewBag.ErrorMessage = "Student ID not Found";
                        TempData["no"] = "Student ID not Found.";
                        return View();
                    }
                    var mapdata=Adapter.StudentAdapter.toUpdateStudentoDO(student);
                        mapdata.Password=exitStudent.Password;
                    
                    var res= await  facade.UpdateStudent(mapdata);
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,RoleId")] AdminVM admin)
        {
            if (ModelState.IsValid)
            {
                var res = new Admin
                {
                    Username = admin.Password,
                    Password = admin.Password,
                    RoleId = admin.RoleId,

                };


                _context.Admins.Add(res);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", admin.RoleId);
            return View(admin);
        }


        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", admin.RoleId);
            return View(admin);
        }

        // POST: Admins/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,Username,Password,RoleId")] Admin admin)
        {
            if (id != admin.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.AdminId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", admin.RoleId);
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }


        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }
    }
}
