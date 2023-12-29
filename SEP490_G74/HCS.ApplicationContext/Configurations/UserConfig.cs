using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.ApplicationContext.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(c => c.Category)
                .WithMany(c => c.Doctors)
                .HasForeignKey(c => c.CategoryId);

            builder
                .HasData
                    (
                        new User() // Admin
                        {
                            UserId = 1,
                            Email = "vkhoa871@gmail.com",
                            Password = "d0c406e82877aacad00415ca64f821e9",
                            CategoryId = null,
                            Status = true,
                            RoleId = 1
                        },
                        new User() // Doctor
                        {
                            UserId = 2,
                            Email = "sonnk1@gmail.com",
                            Password = "d0c406e82877aacad00415ca64f821e9",
                            CategoryId = 1, // Noi khoa
                            Status = true,
                            RoleId = 2
                        },
                        new User()
                        {
                            UserId = 3,
                            Email = "yta1@gmail.com",
                            Password = "d0c406e82877aacad00415ca64f821e9",
                            CategoryId = null,
                            Status = true, // Y ta
                            RoleId = 4
                        }
                    );
        }
    } 
}
