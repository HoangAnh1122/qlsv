using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsvHoang.Models;
using qlsvHoang.Service.IService;
using qlsvHoang.ViewModel;
using System.Security.Claims;

namespace qlsvHoang.Controllers
{
	public class TeachersController : Controller
	{
		private readonly ITeacherService teacherService;

		public TeachersController(ITeacherService teacherService)
		{
			this.teacherService = teacherService;
		}
		[Authorize(Roles = "Teacher")]
		public IActionResult DashBoard()
		{
			return View();
		}


		[HttpGet]
		[Authorize(Roles = "Teacher")]
		public async Task<IActionResult> ProfileTeacher()
		{
			try
			{

				var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				var res = await teacherService.findTeacherById(Int32.Parse(teacherId));
				if (res == null)
				{
					TempData["no"] = "Error Teacher id not found";
					return RedirectToAction("/");
				}

				return View(res);

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		[Authorize(Roles = "Teacher")]
		[HttpPost]
		public async Task<ActionResult> ProfileTeacher(UpdateTeacherVM teacher)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var res = await teacherService.updateTeacher(teacher);
					if (res == 0)
					{
						ViewBag.ErrorMessage = "Teacher not found .";
						TempData["no"] = "Teacher not found. Please choose a different Teacher.";
						return View();

					}
					TempData["ok"] = "Update Teacher Successful!";
					return RedirectToAction("DashBoard", "Teachers");
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			TempData["no"] = "InValid Data";
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Teacher")]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Teacher")]
		public async Task<ActionResult> ChangePassword(ChangePasswordVM changePassword)
		{
			try
			{
				if (ModelState.IsValid)
				{

					var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);

					var res = await teacherService.findTeacherById(Int32.Parse(teacherId));

					if (Common.Security.Hash(changePassword.oldPassword) != res.Password)
					{
						TempData["no"] = "Old Password Not correct !";
						return View();
					}
					if (changePassword.newPassword != changePassword.confirmNewPassword)
					{
						TempData["no"] = "New Password and Confirm New Password not correct !";
						return View();
					}
					res.Password = Common.Security.Hash(changePassword.newPassword);

					await teacherService.updatePassword(res);
					TempData["ok"] = "Change Password Sucessful!";

				}

				TempData["no"] = "Data no valid";
				return View();

			}
			catch (Exception ex) { throw ex; }


		}
	}
}
