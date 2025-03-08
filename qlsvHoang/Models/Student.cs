using System;
using System.Collections.Generic;

namespace qlsvHoang.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string Username { get; set; } = null!;
    public string Name { get; set; } 
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }


    public string Password { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Role Role { get; set; } = null!;
}
