using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace qlsvHoang.Models;

public partial class Se06303Nhom9Context : DbContext
{
    public Se06303Nhom9Context()
    {
    }

    public Se06303Nhom9Context(DbContextOptions<Se06303Nhom9Context> options)
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



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__719FE4880E5902B7");

            entity.HasIndex(e => e.Username, "UQ__Admins__536C85E40256786D").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Admins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Admins_Roles");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A7AEEB9BE6");

            entity.HasIndex(e => e.CourseName, "UQ__Courses__9526E2775ED7456E").IsUnique();

            entity.Property(e => e.CourseName).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F68771B11D17C90");

            entity.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Session).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK__Enrollmen__Sessi__2334397B");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Enrollmen__Stude__22401542");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AB1ED6A10");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160A12B18C2").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(255);
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Sessions__C9F49290FCC2C4C8");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.SessionName).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Course).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Sessions__Course__1D7B6025");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Sessions__Teache__1E6F845E");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B99855EE200");

            entity.HasIndex(e => e.Username, "UQ__Students__536C85E4F3933C51").IsUnique();

            entity.Property(e => e.ClassName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Students)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Roles");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teachers__EDF25964FF7346C8");

            entity.HasIndex(e => e.Username, "UQ__Teachers__536C85E4CC70EEA4").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teachers_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
