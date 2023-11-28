using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Status { get; set; }
    
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
