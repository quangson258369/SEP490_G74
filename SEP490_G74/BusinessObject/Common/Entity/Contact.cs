using System;
using System.Collections.Generic;

namespace API.Common.Entity;

public partial class Contact
{
    public int CId { get; set; }

    public string Name { get; set; } = null!;

    public bool Gender { get; set; }

    public string Phone { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Address { get; set; } = null!;

    public string? Image { get; set; }

    public int UserId { get; set; }

    public virtual Patient User { get; set; } = null!;

    public virtual User User1 { get; set; } = null!;

    public virtual Doctor UserNavigation { get; set; } = null!;
}
