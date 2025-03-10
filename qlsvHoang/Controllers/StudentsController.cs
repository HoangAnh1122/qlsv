using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qlsvHoang.Data;
using qlsvHoang.Models;
using qlsvHoang.Service.FacadePattern;
using qlsvHoang.ViewModel;

namespace qlsvHoang.Controllers
{
	public class StudentsController : Controller
	{
		private readonly qlsvHoangContext _context;
		private readonly StudentFacade facade;

		public StudentsController(qlsvHoangContext context)
		{
			_context = context;
			facade = new StudentFacade(context);
		}

		[Authorize(Roles = "Student")]
		public IActionResult DashBoard()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Student")]
		public async Task<IActionResult> ProfileStudent()
		{
			try
			{

				var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				var res = await facade.GetStudentById(Int32.Parse(studentId));
				if (res == null)
				{
					TempData["no"] = "Error Student id not found";
					return RedirectToAction("/");
				}

				return View(res);

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		[Authorize(Roles = "Student")]
		[HttpPost]
		public async Task<ActionResult> ProfileStudent(EditStudentVM student)
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
					return View();
				}
				catch (Exception ex)
				{
					TempData["no"] = ex.Message;
					throw ex;
				}
			}
			TempData["no"] = "Update Failed Because validate data";

			return View();
		}



		[HttpGet]
		[Authorize(Roles = "Student")]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Student")]
		public async Task<ActionResult> ChangePassword(ChangePasswordVM changePassword)
		{
			if (ModelState.IsValid)
			{

				var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				var res = await facade.GetStudentById(Int32.Parse(studentId));

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
				res.Password=Common.Security.Hash(changePassword.newPassword);
				await facade.UpdateStudent(res);
				TempData["ok"] = "Change Password Sucessful!";

			}

			TempData["no"] = "Data no valid";
			return View();

		}

	}
}
