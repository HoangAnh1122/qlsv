using System;
using System.Collections.Generic;

namespace qlsvHoang.Models;

public partial class Session
{
    public int SessionId { get; set; }

    public string SessionName { get; set; } = null!;

    public int CourseId { get; set; }

    public int TeacherId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Teacher Teacher { get; set; } = null!;
}
