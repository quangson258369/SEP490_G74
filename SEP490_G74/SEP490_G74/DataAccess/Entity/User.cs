﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
