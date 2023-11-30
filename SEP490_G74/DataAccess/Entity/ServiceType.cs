using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class ServiceType
{
    public int ServiceTypeId { get; set; }

    public string ServiceTypeName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
