using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qlsvHoang.Data;
using qlsvHoang.Models;

namespace qlsvHoang.Controllers
{
    public class StudentsController : Controller
    {
        private readonly qlsvHoangContext _context;

        public StudentsController(qlsvHoangContext context)
        {
            _context = context;
        }

        public IActionResult DashBoard()
        {
            return View();
        }
   
    }
}
