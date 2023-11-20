using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class Contact
{
    public int CId { get; set; }

    public string Name { get; set; } = null!;

    public bool Gender { get; set; }

    public string Phone { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Address { get; set; } = null!;

    public string? Img { get; set; }

    public int? DoctorId { get; set; }

    public int? PatientId { get; set; }
    [JsonIgnore]
    public virtual Doctor? Doctor { get; set; }
    [JsonIgnore]
    public virtual Patient? Patient { get; set; }
}
