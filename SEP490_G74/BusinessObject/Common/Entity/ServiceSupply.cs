using System;
using System.Collections.Generic;

namespace API.Common.Entity;

public partial class ServiceSupply
{
    public int Sid { get; set; }

    public int ServiceId { get; set; }

    public byte Quantity { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual Supply SidNavigation { get; set; } = null!;
}
