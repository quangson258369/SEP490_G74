using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCS.ApplicationContext.Configurations
{
    internal class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .HasOne(u => u.User)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.UserId);

            builder
                .HasOne(u => u.Patient)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.PatientId);

            builder.HasKey(c => c.CId);

            builder
                .HasData
                    (
                        new Contact() { CId = 1, Name = "Khoa", UserId = 1, Address = string.Empty },
                        new Contact() { CId = 2, Name = "Son", UserId = 2, Address = string.Empty },
                        new Contact() { CId = 3, Name = "Y Ta", UserId = 3, Address = string.Empty }
                    );
        }
    }
}
