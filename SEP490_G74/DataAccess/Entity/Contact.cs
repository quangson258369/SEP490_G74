using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("Contact")]
public partial class Contact
{
    [Key]
    [Column("cId")]
    public int CId { get; set; }

    [StringLength(150)]
    public string Name { get; set; } = null!;

    public bool Gender { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }

    [StringLength(150)]
    public string Address { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? Image { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Contacts")]
    public virtual Patient User { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Contacts")]
    public virtual User User1 { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Contacts")]
    public virtual Doctor UserNavigation { get; set; } = null!;
}
