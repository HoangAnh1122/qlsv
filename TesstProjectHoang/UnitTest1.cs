using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlsvHoang.Controllers;
using qlsvHoang.Data;
using qlsvHoang.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using qlsvHoang.Service.IService;
using qlsvHoang.ViewModel;
using Microsoft.AspNetCore.Mvc.Routing;
using qlsvHoang.Service.FacadePattern;

namespace TesstProjectHoang
{
    public class AuthenControllerTests
    {
        private readonly AuthenController _controller;
        private readonly qlsvHoangContext _context;
        private readonly Mock<IAdminService> _mockAdminService;
        private readonly Mock<ITeacherService> _mockTeacherService;
        private readonly Mock<StudentFacade> _mockStudentService;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<IUrlHelperFactory> _mockUrlHelperFactory;

        public AuthenControllerTests()
        {
            
            var options = new DbContextOptionsBuilder<qlsvHoangContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .EnableSensitiveDataLogging()
                .Options;

            _context = new qlsvHoangContext(options);

            _mockAdminService = new Mock<IAdminService>();
            _mockTeacherService = new Mock<ITeacherService>();
            _mockStudentService= new Mock<StudentFacade>();
            _mockHttpContext = new Mock<HttpContext>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockUrlHelperFactory = new Mock<IUrlHelperFactory>();

            _controller = new AuthenController(_mockTeacherService.Object, _mockAdminService.Object, _context);

       
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            _mockHttpContext
                .Setup(context => context.User)
                .Returns(userPrincipal);

            _mockHttpContext
                .Setup(context => context.RequestServices.GetService(typeof(IAuthenticationService)))
                .Returns(_mockAuthenticationService.Object);

            _mockHttpContext
                .Setup(context => context.RequestServices.GetService(typeof(IUrlHelperFactory)))
                .Returns(_mockUrlHelperFactory.Object);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _mockHttpContext.Object
            };

            var tempDataProvider = Mock.Of<ITempDataProvider>();
            _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), tempDataProvider);

            SeedDatabase(); 
        }

        private void SeedDatabase()
        {
            _context.Admins.RemoveRange(_context.Admins);
            _context.Students.RemoveRange(_context.Students);
            _context.Teachers.RemoveRange(_context.Teachers);
            _context.Roles.RemoveRange(_context.Roles);
            _context.SaveChanges();

            var roleAdmin = new Role { RoleId = 1, RoleName = "Admin" };
            var roleStudent = new Role { RoleId = 2, RoleName = "Student" };
            var roleTeacher = new Role { RoleId = 3, RoleName = "Teacher" };

            var admin = new Admin
            {
                AdminId = 1,
                Name = "Admin User",
                RoleId = 1,
                Username = "admin",
                Password = "password"
            };

            var student = new Student
            {
                StudentId = 1,
                Name = "Student User",
                RoleId = 2,
                Username = "student",
                Password = "password",
                Address = "123 Test Street",
                ClassName = "Class A",
                PhoneNumber = "0123456789"
            };

            var teacher = new Teacher
            {
                TeacherId = 1,
                Name = "Teacher User",
                RoleId = 3,
                Username = "teacher",
                Password = "password"
            };

            _context.Roles.AddRange(roleAdmin, roleStudent, roleTeacher);
            _context.Admins.Add(admin);
            _context.Students.Add(student);
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }

        [Fact]
        public async Task LoginAdmin_ValidCredentials_ShouldRedirectToDashboard()
        {
            var adminLogin = new AdminLoginVM { Username = "admin", Password = "password" };
            var admin = new Admin { AdminId = 1, Name = "Admin User", RoleId = 1, Username = "admin", Password = "password" };
            _mockAdminService.Setup(s => s.LoginAdmin(adminLogin)).ReturnsAsync(admin);
            var result = await _controller.LoginAdmin(adminLogin);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("DashBoard", redirectResult.ActionName);
            Assert.Equal("Admins", redirectResult.ControllerName);
        }

        [Fact]
        public async Task LoginAdmin_InvalidCredentials_ShouldReturnViewWithError()
        {
            var adminLogin = new AdminLoginVM { Username = "admin", Password = "wrongpass" };
            var result = await _controller.LoginAdmin(adminLogin);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Login Failed !", _controller.TempData["no"]);
        }


        [Fact]
        public void LoginStudent_GetRequest_ShouldReturnViewResult()
        {
            // Act
            var result = _controller.LoginStudent();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task LoginStudent_InvalidCredentials_ShouldReturnViewWithError()
        {
            var studentLogin = new LoginStudentVM { Username = "student", Password = "wrongpass" };
            var result = await _controller.LoginStudent(studentLogin);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Login Failed !", _controller.TempData["no"]);
        }

        [Fact]
        public async Task LoginTeacher_ValidCredentials_ShouldRedirectToDashboard()
        {
            var teacherLogin = new LoginTeacherVM { Username = "teacher", Password = "password" };
            var teacher = new Teacher { TeacherId = 1, Name = "Teacher", RoleId = 3, Username = "teacher", Password = "password" };
            _mockTeacherService.Setup(s => s.loginTeacher(teacherLogin)).ReturnsAsync(teacher);
            var result = await _controller.LoginTeacher(teacherLogin);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("DashBoard", redirectResult.ActionName);
            Assert.Equal("Teachers", redirectResult.ControllerName);
        }

        [Fact]
        public async Task LoginTeacher_InvalidCredentials_ShouldReturnViewWithError()
        {
            var teacherLogin = new LoginTeacherVM { Username = "wrongteacher", Password = "password" };
            var result = await _controller.LoginTeacher(teacherLogin);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Login Failed !", _controller.TempData["no"]);
        }

        [Fact]
        public async Task LoginAdmin_EmptyCredentials_ShouldReturnViewWithError()
        {
            var adminLogin = new AdminLoginVM { Username = "", Password = "" };
            var result = await _controller.LoginAdmin(adminLogin);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Login Failed !", _controller.TempData["no"]);
        }

        [Fact]
        public async Task Logout_ShouldRedirectToHomePage()
        {
            var result = await _controller.Logout();
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.Equal("See you again!", _controller.TempData["ok"]);
        }

        [Fact]
        public void AccessDenied_ShouldReturnViewResult()
        {
            var result = _controller.AccessDenied();
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task LoginAdmin_ExceptionThrown_ShouldThrowException()
        {
            var adminLogin = new AdminLoginVM { Username = "admin", Password = "password" };
            _mockAdminService.Setup(s => s.LoginAdmin(adminLogin)).ThrowsAsync(new System.Exception("Database error"));
            await Assert.ThrowsAsync<System.Exception>(() => _controller.LoginAdmin(adminLogin));
        }
    }
}
