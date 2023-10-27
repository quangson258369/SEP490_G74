using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("Role")]
public partial class Role
{
    [Key]
    [Column("roleId")]
    public int RoleId { get; set; }

    [Column("roleName")]
    [StringLength(150)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("Roles")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
