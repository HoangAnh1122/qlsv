using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using qlsvHoang.Data;
using qlsvHoang.Service.IService;
using qlsvHoang.Service.Service;
namespace qlsvHoang
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<qlsvHoangContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("qlsvHoangContext") ?? throw new InvalidOperationException("Connection string 'qlsvHoangContext' not found.")));


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Authen/LoginAdmin";
                    options.LoginPath = "/Authen/LoginStudent";
                    options.LoginPath = "/Authen/LoginTeacher";
                    options.ExpireTimeSpan = TimeSpan.FromDays(15);
                });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //DI
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            var app = builder.Build();


            //Authorize 


            builder.Services.AddAuthorization();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Authen}/{action=LoginAdmin}/{id?}");

            app.Run();
        }
    }
}
