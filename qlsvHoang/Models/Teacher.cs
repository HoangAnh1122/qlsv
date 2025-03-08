using System;
using System.Collections.Generic;

namespace qlsvHoang.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
