using System;
using System.Collections.Generic;

namespace qlsvHoang.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int StudentId { get; set; }

    public int SessionId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public virtual Session Session { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
