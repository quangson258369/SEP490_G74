﻿// <auto-generated />
using System;
using HCS.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HCS.ApplicationContext.Migrations
{
    [DbContext(typeof(HCSContext))]
    [Migration("20240103073817_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HCS.Domain.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HCS.Domain.Models.Contact", b =>
                {
                    b.Property<int>("CId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("HCS.Domain.Models.ExaminationResult", b =>
                {
                    b.Property<int>("ExamResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamResultId"));

                    b.Property<string>("Conclusion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExamDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PrescriptionId")
                        .HasColumnType("int");

                    b.HasKey("ExamResultId");

                    b.HasIndex("PrescriptionId")
                        .IsUnique()
                        .HasFilter("[PrescriptionId] IS NOT NULL");

                    b.ToTable("ExaminationResults");
                });

            modelBuilder.Entity("HCS.Domain.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceId"));

                    b.Property<int>("CashierId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalRecordId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CashierId");

                    b.HasIndex("MedicalRecordId");

                    b.HasIndex("PatientId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("HCS.Domain.Models.MedicalRecord", b =>
                {
                    b.Property<int>("MedicalRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicalRecordId"));

                    b.Property<string>("ExamReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExaminationResultId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCheckUp")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("MedicalRecordDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("MedicalRecordId");

                    b.HasIndex("ExaminationResultId")
                        .IsUnique()
                        .HasFilter("[ExaminationResultId] IS NOT NULL");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("HCS.Domain.Models.MedicalRecordCateogry", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalRecordId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "MedicalRecordId");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("MedicalRecordCategories");
                });

            modelBuilder.Entity("HCS.Domain.Models.MedicalRecordDoctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalRecordId")
                        .HasColumnType("int");

                    b.HasKey("DoctorId", "MedicalRecordId");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("MedicalRecordDoctors");
                });

            modelBuilder.Entity("HCS.Domain.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"));

                    b.Property<string>("Allergieshistory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BloodGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("BloodPressure")
                        .HasColumnType("tinyint");

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<byte?>("Height")
                        .HasColumnType("tinyint");

                    b.Property<string>("ServiceDetailName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Weight")
                        .HasColumnType("tinyint");

                    b.HasKey("PatientId");

                    b.HasIndex("ContactId")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("HCS.Domain.Models.Prescription", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrescriptionId"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diagnose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrescriptionId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("HCS.Domain.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HCS.Domain.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("int");

                    b.HasKey("ServiceId");

                    b.HasIndex("ServiceTypeId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("HCS.Domain.Models.ServiceMedicalRecord", b =>
                {
                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalRecordId")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("ServiceId", "MedicalRecordId");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("ServiceMedicalRecords");
                });

            modelBuilder.Entity("HCS.Domain.Models.ServiceType", b =>
                {
                    b.Property<int>("ServiceTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceTypeId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceTypeId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("HCS.Domain.Models.SuppliesPrescription", b =>
                {
                    b.Property<int>("SId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SId"));

                    b.Property<int>("PrescriptionId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SupplyId")
                        .HasColumnType("int");

                    b.HasKey("SId");

                    b.HasIndex("PrescriptionId");

                    b.HasIndex("SupplyId");

                    b.ToTable("SuppliesPrescriptions");
                });

            modelBuilder.Entity("HCS.Domain.Models.SuppliesType", b =>
                {
                    b.Property<int>("SuppliesTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SuppliesTypeId"));

                    b.Property<string>("SuppliesTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SuppliesTypeId");

                    b.ToTable("SuppliesTypes");
                });

            modelBuilder.Entity("HCS.Domain.Models.Supply", b =>
                {
                    b.Property<int>("SId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SId"));

                    b.Property<string>("Distributor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Exp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Inputday")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SuppliesTypeId")
                        .HasColumnType("int");

                    b.Property<short>("UnitInStock")
                        .HasColumnType("smallint");

                    b.Property<string>("Uses")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SId");

                    b.HasIndex("SuppliesTypeId");

                    b.ToTable("Supplies");
                });

            modelBuilder.Entity("HCS.Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ContactId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HCS.Domain.Models.ExaminationResult", b =>
                {
                    b.HasOne("HCS.Domain.Models.Prescription", "Prescription")
                        .WithOne("ExaminationResult")
                        .HasForeignKey("HCS.Domain.Models.ExaminationResult", "PrescriptionId");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("HCS.Domain.Models.Invoice", b =>
                {
                    b.HasOne("HCS.Domain.Models.User", "Cashier")
                        .WithMany("Invoices")
                        .HasForeignKey("CashierId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HCS.Domain.Models.MedicalRecord", "MedicalRecord")
                        .WithMany("Invoices")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HCS.Domain.Models.Patient", "Patient")
                        .WithMany("Invoices")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cashier");

                    b.Navigation("MedicalRecord");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HCS.Domain.Models.MedicalRecord", b =>
                {
                    b.HasOne("HCS.Domain.Models.ExaminationResult", "ExaminationResult")
                        .WithOne("MedicalRecord")
                        .HasForeignKey("HCS.Domain.Models.MedicalRecord", "ExaminationResultId");

                    b.HasOne("HCS.Domain.Models.Patient", "Patient")
                        .WithMany("MedicalRecords")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExaminationResult");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HCS.Domain.Models.MedicalRecordCateogry", b =>
                {
                    b.HasOne("HCS.Domain.Models.Category", "Category")
                        .WithMany("MedicalRecordCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HCS.Domain.Models.MedicalRecord", "MedicalRecord")
                        .WithMany("MedicalRecordCategories")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("MedicalRecord");
                });

            modelBuilder.Entity("HCS.Domain.Models.MedicalRecordDoctor", b =>
                {
                    b.HasOne("HCS.Domain.Models.User", "Doctor")
                        .WithMany("MedicalRecordDoctors")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HCS.Domain.Models.MedicalRecord", "MedicalRecord")
                        .WithMany("MedicalRecordDoctors")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("MedicalRecord");
                });

            modelBuilder.Entity("HCS.Domain.Models.Patient", b =>
                {
                    b.HasOne("HCS.Domain.Models.Contact", "Contact")
                        .WithOne("Patient")
                        .HasForeignKey("HCS.Domain.Models.Patient", "ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("HCS.Domain.Models.Service", b =>
                {
                    b.HasOne("HCS.Domain.Models.ServiceType", "ServiceType")
                        .WithMany("Services")
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("HCS.Domain.Models.ServiceMedicalRecord", b =>
                {
                    b.HasOne("HCS.Domain.Models.MedicalRecord", "MedicalRecord")
                        .WithMany("ServiceMedicalRecords")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HCS.Domain.Models.Service", "Service")
                        .WithMany("ServiceMedicalRecords")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalRecord");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("HCS.Domain.Models.ServiceType", b =>
                {
                    b.HasOne("HCS.Domain.Models.Category", "Category")
                        .WithMany("ServiceTypes")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("HCS.Domain.Models.SuppliesPrescription", b =>
                {
                    b.HasOne("HCS.Domain.Models.Prescription", "Prescription")
                        .WithMany("SuppliesPrescriptions")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HCS.Domain.Models.Supply", "Supply")
                        .WithMany("SuppliesPrescriptions")
                        .HasForeignKey("SupplyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prescription");

                    b.Navigation("Supply");
                });

            modelBuilder.Entity("HCS.Domain.Models.Supply", b =>
                {
                    b.HasOne("HCS.Domain.Models.SuppliesType", "SuppliesType")
                        .WithMany("Supplies")
                        .HasForeignKey("SuppliesTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SuppliesType");
                });

            modelBuilder.Entity("HCS.Domain.Models.User", b =>
                {
                    b.HasOne("HCS.Domain.Models.Category", "Category")
                        .WithMany("Doctors")
                        .HasForeignKey("CategoryId");

                    b.HasOne("HCS.Domain.Models.Contact", "Contact")
                        .WithOne("User")
                        .HasForeignKey("HCS.Domain.Models.User", "ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HCS.Domain.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Contact");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HCS.Domain.Models.Category", b =>
                {
                    b.Navigation("Doctors");

                    b.Navigation("MedicalRecordCategories");

                    b.Navigation("ServiceTypes");
                });

            modelBuilder.Entity("HCS.Domain.Models.Contact", b =>
                {
                    b.Navigation("Patient");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HCS.Domain.Models.ExaminationResult", b =>
                {
                    b.Navigation("MedicalRecord")
                        .IsRequired();
                });

            modelBuilder.Entity("HCS.Domain.Models.MedicalRecord", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("MedicalRecordCategories");

                    b.Navigation("MedicalRecordDoctors");

                    b.Navigation("ServiceMedicalRecords");
                });

            modelBuilder.Entity("HCS.Domain.Models.Patient", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("MedicalRecords");
                });

            modelBuilder.Entity("HCS.Domain.Models.Prescription", b =>
                {
                    b.Navigation("ExaminationResult")
                        .IsRequired();

                    b.Navigation("SuppliesPrescriptions");
                });

            modelBuilder.Entity("HCS.Domain.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("HCS.Domain.Models.Service", b =>
                {
                    b.Navigation("ServiceMedicalRecords");
                });

            modelBuilder.Entity("HCS.Domain.Models.ServiceType", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("HCS.Domain.Models.SuppliesType", b =>
                {
                    b.Navigation("Supplies");
                });

            modelBuilder.Entity("HCS.Domain.Models.Supply", b =>
                {
                    b.Navigation("SuppliesPrescriptions");
                });

            modelBuilder.Entity("HCS.Domain.Models.User", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("MedicalRecordDoctors");
                });
#pragma warning restore 612, 618
        }
    }
}