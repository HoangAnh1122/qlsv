using System;
using System.Collections.Generic;

namespace qlsvHoang.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}


