using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Common.Entity;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<ExaminationResultId> ExaminationResultIds { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceSupply> ServiceSupplies { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<SuppliesType> SuppliesTypes { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("MyCnn"));
    }
        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.Property(e => e.Address).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.Image).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.Name).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.Phone).UseCollation("Vietnamese_CI_AS");

            entity.HasOne(d => d.User).WithMany(p => p.Contacts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Patient");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Contacts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Doctor");

            entity.HasOne(d => d.User1).WithMany(p => p.Contacts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_User");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.Property(e => e.DoctorSpecialist).UseCollation("Vietnamese_CI_AS");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.Doctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_ServiceType");
        });

        modelBuilder.Entity<ExaminationResultId>(entity =>
        {
            entity.Property(e => e.ExamResultId).ValueGeneratedNever();
            entity.Property(e => e.Conclusion).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.Diagnosis).UseCollation("Vietnamese_CI_AS");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.ExaminationResultIds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExaminationResultId_MedicalRecord");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(e => e.PaymentMethod).UseCollation("Vietnamese_CI_AS");

            entity.HasOne(d => d.Cashier).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_User");

            entity.HasOne(d => d.Patient).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Patient");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.Property(e => e.InvoiceDetailId).ValueGeneratedNever();

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_Invoice");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.Property(e => e.MedicalRecordId).ValueGeneratedNever();
            entity.Property(e => e.ExamCode).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.ExamReason).UseCollation("Vietnamese_CI_AS");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalRecord_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalRecord_Patient");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.PatientId).ValueGeneratedNever();
            entity.Property(e => e.ServiceDetailName).UseCollation("Vietnamese_CI_AS");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.Property(e => e.Diagnose).UseCollation("Vietnamese_CI_AS");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Prescriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_MedicalRecord");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).UseCollation("Vietnamese_CI_AS");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.ServiceId).ValueGeneratedNever();
            entity.Property(e => e.ServiceName).UseCollation("Vietnamese_CI_AS");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.Services)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_ServiceType");

            entity.HasMany(d => d.MedicalRecords).WithMany(p => p.Services)
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceMedicalRecord",
                    r => r.HasOne<MedicalRecord>().WithMany()
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ServiceMedicalRecord_MedicalRecord"),
                    l => l.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ServiceMedicalRecord_Service"),
                    j =>
                    {
                        j.HasKey("ServiceId", "MedicalRecordId");
                        j.ToTable("ServiceMedicalRecord");
                        j.IndexerProperty<int>("ServiceId").HasColumnName("serviceId");
                        j.IndexerProperty<int>("MedicalRecordId").HasColumnName("medicalRecordId");
                    });
        });

        modelBuilder.Entity<ServiceSupply>(entity =>
        {
            entity.HasOne(d => d.Service).WithMany(p => p.ServiceSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceSupplies_Service");

            entity.HasOne(d => d.SidNavigation).WithMany(p => p.ServiceSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceSupplies_Supplies");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.Property(e => e.ServiceTypeId).ValueGeneratedNever();
            entity.Property(e => e.ServiceTypeName).UseCollation("Vietnamese_CI_AS");
        });

        modelBuilder.Entity<SuppliesType>(entity =>
        {
            entity.Property(e => e.SuppliesTypeId).ValueGeneratedNever();
            entity.Property(e => e.SuppliesTypeName).UseCollation("Vietnamese_CI_AS");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.Property(e => e.Distributor).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.SName).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.Uses).UseCollation("Vietnamese_CI_AS");

            entity.HasOne(d => d.SuppliesType).WithMany(p => p.Supplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_SuppliesType");

            entity.HasMany(d => d.Prescriptions).WithMany(p => p.SIds)
                .UsingEntity<Dictionary<string, object>>(
                    "SuppliesPrescription",
                    r => r.HasOne<Prescription>().WithMany()
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SuppliesPrescription_Prescription"),
                    l => l.HasOne<Supply>().WithMany()
                        .HasForeignKey("SId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SuppliesPrescription_Supplies"),
                    j =>
                    {
                        j.HasKey("SId", "PrescriptionId");
                        j.ToTable("SuppliesPrescription");
                        j.IndexerProperty<int>("SId").HasColumnName("sId");
                        j.IndexerProperty<int>("PrescriptionId").HasColumnName("prescriptionId");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.Password).UseCollation("Vietnamese_CI_AS");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRole_Role"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRole_User"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRole");
                        j.IndexerProperty<int>("UserId").HasColumnName("userId");
                        j.IndexerProperty<int>("RoleId").HasColumnName("roleId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
