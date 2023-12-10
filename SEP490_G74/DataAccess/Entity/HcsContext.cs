using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Entity;

public partial class HcsContext : DbContext
{
    public HcsContext()
    {
    }

    public HcsContext(DbContextOptions<HcsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ExaminationResultId> ExaminationResultIds { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceMedicalRecord> ServiceMedicalRecords { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<SuppliesPrescription> SuppliesPrescriptions { get; set; }

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
            entity.HasKey(e => e.CId);

            entity.ToTable("Contact");

            entity.Property(e => e.CId)
                .ValueGeneratedNever()
                .HasColumnName("cId");
            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("dob");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.Img)
                .HasMaxLength(300)
                .HasColumnName("img");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.PatientId).HasColumnName("patientId");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Doctor).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Contact_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Contact_Patient");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK_Doctor");

            entity.ToTable("Employee");

            entity.Property(e => e.DoctorId)
                .ValueGeneratedNever()
                .HasColumnName("doctorId");
            entity.Property(e => e.ServiceTypeId).HasColumnName("serviceTypeId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ServiceTypeId)
                .HasConstraintName("FK_Doctor_ServiceType");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_User");
        });

        modelBuilder.Entity<ExaminationResultId>(entity =>
        {
            entity.HasKey(e => e.ExamResultId);

            entity.ToTable("ExaminationResultId");

            entity.Property(e => e.ExamResultId)
                .ValueGeneratedNever()
                .HasColumnName("examResultId");
            entity.Property(e => e.Conclusion)
                .HasMaxLength(350)
                .HasColumnName("conclusion");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(350)
                .HasColumnName("diagnosis");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.ExamDate)
                .HasColumnType("datetime")
                .HasColumnName("examDate");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medicalRecordID");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.ExaminationResultIds)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExaminationResultId_MedicalRecord");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("invoiceId");
            entity.Property(e => e.CashierId).HasColumnName("cashierId");
            entity.Property(e => e.PatientId).HasColumnName("patientId");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("paymentDate");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("paymentMethod");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");

            entity.HasOne(d => d.Cashier).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CashierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_User");

            entity.HasOne(d => d.Patient).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Patient");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.ToTable("InvoiceDetail");

            entity.Property(e => e.InvoiceDetailId)
                .ValueGeneratedNever()
                .HasColumnName("invoiceDetailId");
            entity.Property(e => e.InvoiceId).HasColumnName("invoiceId");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medicalRecordId");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_Invoice");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_MedicalRecord");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.ToTable("MedicalRecord");

            entity.Property(e => e.MedicalRecordId)
                .ValueGeneratedNever()
                .HasColumnName("medicalRecordID");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.ExamCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("examCode");
            entity.Property(e => e.ExamReason)
                .HasMaxLength(250)
                .HasColumnName("examReason");
            entity.Property(e => e.MedicalRecordDate)
                .HasColumnType("datetime")
                .HasColumnName("medicalRecordDate");
            entity.Property(e => e.PatientId).HasColumnName("patientId");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalRecord_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalRecord_Patient");

            entity.HasOne(d => d.Prescription).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PrescriptionId)
                .HasConstraintName("FK_MedicalRecord_Prescription");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.PatientId)
                .ValueGeneratedNever()
                .HasColumnName("patientId");
            entity.Property(e => e.Allergieshistory)
                .HasMaxLength(350)
                .HasColumnName("allergieshistory");
            entity.Property(e => e.BloodGroup)
                .HasMaxLength(10)
                .HasColumnName("blood group");
            entity.Property(e => e.BloodPressure).HasColumnName("blood pressure");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.ServiceDetailName)
                .HasMaxLength(350)
                .HasColumnName("serviceDetailName");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.ToTable("Prescription");

            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("createDate");
            entity.Property(e => e.Diagnose)
                .HasMaxLength(550)
                .HasColumnName("diagnose");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("roleId");
            entity.Property(e => e.RoleName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedNever()
                .HasColumnName("serviceId");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(150)
                .HasColumnName("serviceName");
            entity.Property(e => e.ServiceTypeId).HasColumnName("serviceTypeId");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.Services)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_ServiceType");
        });

        modelBuilder.Entity<ServiceMedicalRecord>(entity =>
        {
            entity.HasKey(e => new { e.ServiceId, e.MedicalRecordId });

            entity.ToTable("ServiceMedicalRecord");

            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medicalRecordId");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Doctor).WithMany(p => p.ServiceMedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_ServiceMedicalRecord_Employee");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.ServiceMedicalRecords)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceMedicalRecord_MedicalRecord");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceMedicalRecords)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceMedicalRecord_Service");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.ToTable("ServiceType");

            entity.Property(e => e.ServiceTypeId)
                .ValueGeneratedNever()
                .HasColumnName("serviceTypeId");
            entity.Property(e => e.ServiceTypeName)
                .HasMaxLength(150)
                .HasColumnName("serviceTypeName");
        });

        modelBuilder.Entity<SuppliesPrescription>(entity =>
        {
            entity.HasKey(e => new { e.SId, e.PrescriptionId });

            entity.ToTable("SuppliesPrescription");

            entity.Property(e => e.SId).HasColumnName("sId");
            entity.Property(e => e.PrescriptionId).HasColumnName("prescriptionId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Prescription).WithMany(p => p.SuppliesPrescriptions)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuppliesPrescription_Prescription");

            entity.HasOne(d => d.SIdNavigation).WithMany(p => p.SuppliesPrescriptions)
                .HasForeignKey(d => d.SId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuppliesPrescription_Supplies");
        });

        modelBuilder.Entity<SuppliesType>(entity =>
        {
            entity.ToTable("SuppliesType");

            entity.Property(e => e.SuppliesTypeId)
                .ValueGeneratedNever()
                .HasColumnName("suppliesTypeId");
            entity.Property(e => e.SuppliesTypeName)
                .HasMaxLength(150)
                .HasColumnName("suppliesTypeName");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.SId);

            entity.Property(e => e.SId).HasColumnName("sId");
            entity.Property(e => e.Distributor)
                .HasMaxLength(150)
                .HasColumnName("distributor");
            entity.Property(e => e.Exp)
                .HasColumnType("date")
                .HasColumnName("exp");
            entity.Property(e => e.Inputday)
                .HasColumnType("datetime")
                .HasColumnName("inputday");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.SName)
                .HasMaxLength(150)
                .HasColumnName("sName");
            entity.Property(e => e.SuppliesTypeId).HasColumnName("suppliesTypeId");
            entity.Property(e => e.UnitInStock).HasColumnName("unitInStock");
            entity.Property(e => e.Uses)
                .HasMaxLength(500)
                .HasColumnName("uses");

            entity.HasOne(d => d.SuppliesType).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.SuppliesTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_SuppliesType");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Status).HasColumnName("status");

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
