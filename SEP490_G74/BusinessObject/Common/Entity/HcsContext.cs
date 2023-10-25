using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HCS﻿;Persist Security Info=True;User ID=sa;Password=qead123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.CId);

            entity.ToTable("Contact");

            entity.Property(e => e.CId).HasColumnName("cId");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Image)
                .HasMaxLength(250)
                .IsUnicode(false)
                .UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Vietnamese_CI_AS");

            entity.HasOne(d => d.User).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Patient");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Doctor");

            entity.HasOne(d => d.User1).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_User");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Doctor");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.DoctorSpecialist)
                .HasMaxLength(150)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("doctorSpecialist");
            entity.Property(e => e.ServiceTypeId).HasColumnName("serviceTypeId");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_ServiceType");
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
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("conclusion");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(350)
                .UseCollation("Vietnamese_CI_AS")
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
                .UseCollation("Vietnamese_CI_AS")
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
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("examCode");
            entity.Property(e => e.ExamReason)
                .HasMaxLength(250)
                .UseCollation("Vietnamese_CI_AS")
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
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.PatientId)
                .ValueGeneratedNever()
                .HasColumnName("patientId");
            entity.Property(e => e.ExamDate)
                .HasColumnType("datetime")
                .HasColumnName("examDate");
            entity.Property(e => e.ServiceDetailName)
                .HasMaxLength(350)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("serviceDetailName");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.ToTable("Prescription");

            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("createDate");
            entity.Property(e => e.Diagnose)
                .HasMaxLength(550)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("diagnose");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medicalRecordId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_MedicalRecord");
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
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedNever()
                .HasColumnName("serviceId");
            entity.Property(e => e.DetailId)
                .HasMaxLength(10)
                .IsFixedLength()
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("detailId");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(150)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("serviceName");
            entity.Property(e => e.ServiceTypeId).HasColumnName("serviceTypeId");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.Services)
                .HasForeignKey(d => d.ServiceTypeId)
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
            entity.HasKey(e => new { e.Sid, e.ServiceId });

            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceSupplies)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceSupplies_Service");

            entity.HasOne(d => d.SidNavigation).WithMany(p => p.ServiceSupplies)
                .HasForeignKey(d => d.Sid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceSupplies_Supplies");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.ToTable("ServiceType");

            entity.Property(e => e.ServiceTypeId)
                .ValueGeneratedNever()
                .HasColumnName("serviceTypeId");
            entity.Property(e => e.ServiceTypeName)
                .HasMaxLength(150)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("serviceTypeName");
        });

        modelBuilder.Entity<SuppliesType>(entity =>
        {
            entity.ToTable("SuppliesType");

            entity.Property(e => e.SuppliesTypeId)
                .ValueGeneratedNever()
                .HasColumnName("suppliesTypeId");
            entity.Property(e => e.SuppliesTypeName)
                .HasMaxLength(150)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("suppliesTypeName");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.SId);

            entity.Property(e => e.SId).HasColumnName("sId");
            entity.Property(e => e.Distributor)
                .HasMaxLength(150)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("distributor");
            entity.Property(e => e.Exp)
                .HasColumnType("date")
                .HasColumnName("exp");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.SName)
                .HasMaxLength(150)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("sName");
            entity.Property(e => e.SuppliesTypeId).HasColumnName("suppliesTypeId");
            entity.Property(e => e.UnitInStock).HasColumnName("unitInStock");
            entity.Property(e => e.Uses)
                .HasMaxLength(500)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("uses");

            entity.HasOne(d => d.SuppliesType).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.SuppliesTypeId)
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
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("password");

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
