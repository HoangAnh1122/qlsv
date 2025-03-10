using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsvHoang.Models;
using qlsvHoang.Service.IService;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListCourse()
        {
            try
            {

                var res = await courseService.getListCourse();
                return View(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse(CourseVM courseVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await courseService.createCourse(courseVM);
                    TempData["ok"] = "Create Course Sucessful !";
                    return RedirectToAction("ListCourse");
                }
                TempData["no"] = "InValid Data ";
                return View();
            }
            catch (Exception ex)
            {
                TempData["no"] = ex.Message;
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCourse(int id)
        {
            // check exit
            try
            {
                var checkexit = await courseService.findCourseById(id);
                if (checkexit != null)
                {
                    return View(checkexit);
                }
                TempData["no"] = "Error id not found";
                return RedirectToAction("ListCourse");
            }
            catch (Exception e)
            {
                TempData["no"] = e.Message;
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCourse(Course course)
        {
            try

            {
                if (ModelState.IsValid)
                {
                    var res = await courseService.updateCourse(course);
                    if (res != 1)
                    {
                        TempData["no"] = "Error";
                        return View();
                    }
                    TempData["ok"] = "Update Successful !";
                    return RedirectToAction("ListCourse");

                }
                TempData["no"] = "Data inValid";
                return RedirectToAction("ListCourse");
            }
            catch (Exception e)
            {
                TempData["no"] = e.Message;
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                var res = await courseService.deleteCourse(id);
                if (res != 1)
                {
                    TempData["no"] = "Error id course not found";
                    return RedirectToAction("ListCourse");
                }
                TempData["ok"] = "Dlete Sucessful !";
                return RedirectToAction("ListCourse");

            }
            catch (Exception e)
            {
                TempData["no"] = e.Message;
                throw new Exception(e.Message);
            }
        }
    }
}
