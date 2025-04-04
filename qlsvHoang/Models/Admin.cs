﻿using System;
using System.Collections.Generic;

namespace qlsvHoang.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Username { get; set; } = null!;

    public string Name { get; set; } 
    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;
}
