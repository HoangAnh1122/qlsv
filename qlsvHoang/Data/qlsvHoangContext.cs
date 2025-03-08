using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qlsvHoang.Models;

namespace qlsvHoang.Data
{
    public class qlsvHoangContext : DbContext
    {
        public qlsvHoangContext (DbContextOptions<qlsvHoangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Enrollment> Enrollments { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Teacher> Teachers { get; set; }

    }
}
