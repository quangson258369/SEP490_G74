USE [HCS]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 10/18/2023 6:41:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[cId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Address] [nvarchar](150) NOT NULL,
	[Image] [varchar](250) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[cId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[doctorSpecialist] [nvarchar](150) NOT NULL,
	[serviceTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExaminationResultId]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExaminationResultId](
	[examResultId] [int] NOT NULL,
	[medicalRecordID] [int] NOT NULL,
	[doctorId] [int] NOT NULL,
	[diagnosis] [nvarchar](350) NOT NULL,
	[conclusion] [nvarchar](350) NOT NULL,
	[examDate] [datetime] NOT NULL,
	[serviceId] [int] NOT NULL,
 CONSTRAINT [PK_ExaminationResultId] PRIMARY KEY CLUSTERED 
(
	[examResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[invoiceId] [int] IDENTITY(1,1) NOT NULL,
	[patientId] [int] NOT NULL,
	[cashierId] [int] NOT NULL,
	[paymentDate] [datetime] NOT NULL,
	[status] [bit] NOT NULL,
	[total] [money] NOT NULL,
	[paymentMethod] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[invoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[invoiceDetailId] [int] NOT NULL,
	[invoiceId] [int] NOT NULL,
	[medicalRecordId] [int] NOT NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[invoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalRecord]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalRecord](
	[medicalRecordID] [int] NOT NULL,
	[patientId] [int] NOT NULL,
	[medicalRecordDate] [datetime] NOT NULL,
	[examReason] [nvarchar](250) NULL,
	[examCode] [varchar](50) NOT NULL,
	[doctorId] [int] NOT NULL,
 CONSTRAINT [PK_MedicalRecord] PRIMARY KEY CLUSTERED 
(
	[medicalRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[patientId] [int] NOT NULL,
	[serviceDetailName] [nvarchar](350) NOT NULL,
	[examDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[patientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prescription]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prescription](
	[PrescriptionId] [int] IDENTITY(1,1) NOT NULL,
	[createDate] [date] NOT NULL,
	[diagnose] [nvarchar](550) NOT NULL,
	[quantity] [tinyint] NOT NULL,
	[medicalRecordId] [int] NOT NULL,
 CONSTRAINT [PK_Prescription] PRIMARY KEY CLUSTERED 
(
	[PrescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[roleId] [int] NOT NULL,
	[roleName] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[serviceId] [int] NOT NULL,
	[serviceTypeId] [int] NOT NULL,
	[serviceName] [nvarchar](150) NOT NULL,
	[detailId] [nchar](10) NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[serviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceMedicalRecord]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceMedicalRecord](
	[serviceId] [int] NOT NULL,
	[medicalRecordId] [int] NOT NULL,
 CONSTRAINT [PK_ServiceMedicalRecord] PRIMARY KEY CLUSTERED 
(
	[serviceId] ASC,
	[medicalRecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceSupplies]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceSupplies](
	[sid] [int] NOT NULL,
	[serviceId] [int] NOT NULL,
	[quantity] [tinyint] NOT NULL,
 CONSTRAINT [PK_ServiceSupplies] PRIMARY KEY CLUSTERED 
(
	[sid] ASC,
	[serviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType](
	[serviceTypeId] [int] NOT NULL,
	[serviceTypeName] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[serviceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplies]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplies](
	[sId] [int] IDENTITY(1,1) NOT NULL,
	[sName] [nvarchar](150) NOT NULL,
	[uses] [nvarchar](500) NOT NULL,
	[exp] [date] NOT NULL,
	[distributor] [nvarchar](150) NOT NULL,
	[unitInStock] [smallint] NOT NULL,
	[price] [money] NOT NULL,
	[suppliesTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Supplies] PRIMARY KEY CLUSTERED 
(
	[sId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesPrescription]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesPrescription](
	[sId] [int] NOT NULL,
	[prescriptionId] [int] NOT NULL,
 CONSTRAINT [PK_SuppliesPrescription] PRIMARY KEY CLUSTERED 
(
	[sId] ASC,
	[prescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesType]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesType](
	[suppliesTypeId] [int] NOT NULL,
	[suppliesTypeName] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_SuppliesType] PRIMARY KEY CLUSTERED 
(
	[suppliesTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 10/18/2023 6:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[userId] [int] NOT NULL,
	[roleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[userId] ASC,
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Doctor] FOREIGN KEY([UserId])
REFERENCES [dbo].[Doctor] ([userId])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Doctor]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Patient] FOREIGN KEY([UserId])
REFERENCES [dbo].[Patient] ([patientId])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Patient]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_User]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_ServiceType] FOREIGN KEY([serviceTypeId])
REFERENCES [dbo].[ServiceType] ([serviceTypeId])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_ServiceType]
GO
ALTER TABLE [dbo].[ExaminationResultId]  WITH CHECK ADD  CONSTRAINT [FK_ExaminationResultId_MedicalRecord] FOREIGN KEY([medicalRecordID])
REFERENCES [dbo].[MedicalRecord] ([medicalRecordID])
GO
ALTER TABLE [dbo].[ExaminationResultId] CHECK CONSTRAINT [FK_ExaminationResultId_MedicalRecord]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Patient] FOREIGN KEY([patientId])
REFERENCES [dbo].[Patient] ([patientId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Patient]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User] FOREIGN KEY([cashierId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Invoice] FOREIGN KEY([invoiceId])
REFERENCES [dbo].[Invoice] ([invoiceId])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Invoice]
GO
ALTER TABLE [dbo].[MedicalRecord]  WITH CHECK ADD  CONSTRAINT [FK_MedicalRecord_Doctor] FOREIGN KEY([doctorId])
REFERENCES [dbo].[Doctor] ([userId])
GO
ALTER TABLE [dbo].[MedicalRecord] CHECK CONSTRAINT [FK_MedicalRecord_Doctor]
GO
ALTER TABLE [dbo].[MedicalRecord]  WITH CHECK ADD  CONSTRAINT [FK_MedicalRecord_Patient] FOREIGN KEY([patientId])
REFERENCES [dbo].[Patient] ([patientId])
GO
ALTER TABLE [dbo].[MedicalRecord] CHECK CONSTRAINT [FK_MedicalRecord_Patient]
GO
ALTER TABLE [dbo].[Prescription]  WITH CHECK ADD  CONSTRAINT [FK_Prescription_MedicalRecord] FOREIGN KEY([medicalRecordId])
REFERENCES [dbo].[MedicalRecord] ([medicalRecordID])
GO
ALTER TABLE [dbo].[Prescription] CHECK CONSTRAINT [FK_Prescription_MedicalRecord]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceType] FOREIGN KEY([serviceTypeId])
REFERENCES [dbo].[ServiceType] ([serviceTypeId])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ServiceType]
GO
ALTER TABLE [dbo].[ServiceMedicalRecord]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMedicalRecord_MedicalRecord] FOREIGN KEY([medicalRecordId])
REFERENCES [dbo].[MedicalRecord] ([medicalRecordID])
GO
ALTER TABLE [dbo].[ServiceMedicalRecord] CHECK CONSTRAINT [FK_ServiceMedicalRecord_MedicalRecord]
GO
ALTER TABLE [dbo].[ServiceMedicalRecord]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMedicalRecord_Service] FOREIGN KEY([serviceId])
REFERENCES [dbo].[Service] ([serviceId])
GO
ALTER TABLE [dbo].[ServiceMedicalRecord] CHECK CONSTRAINT [FK_ServiceMedicalRecord_Service]
GO
ALTER TABLE [dbo].[ServiceSupplies]  WITH CHECK ADD  CONSTRAINT [FK_ServiceSupplies_Service] FOREIGN KEY([serviceId])
REFERENCES [dbo].[Service] ([serviceId])
GO
ALTER TABLE [dbo].[ServiceSupplies] CHECK CONSTRAINT [FK_ServiceSupplies_Service]
GO
ALTER TABLE [dbo].[ServiceSupplies]  WITH CHECK ADD  CONSTRAINT [FK_ServiceSupplies_Supplies] FOREIGN KEY([sid])
REFERENCES [dbo].[Supplies] ([sId])
GO
ALTER TABLE [dbo].[ServiceSupplies] CHECK CONSTRAINT [FK_ServiceSupplies_Supplies]
GO
ALTER TABLE [dbo].[Supplies]  WITH CHECK ADD  CONSTRAINT [FK_Supplies_SuppliesType] FOREIGN KEY([suppliesTypeId])
REFERENCES [dbo].[SuppliesType] ([suppliesTypeId])
GO
ALTER TABLE [dbo].[Supplies] CHECK CONSTRAINT [FK_Supplies_SuppliesType]
GO
ALTER TABLE [dbo].[SuppliesPrescription]  WITH CHECK ADD  CONSTRAINT [FK_SuppliesPrescription_Prescription] FOREIGN KEY([prescriptionId])
REFERENCES [dbo].[Prescription] ([PrescriptionId])
GO
ALTER TABLE [dbo].[SuppliesPrescription] CHECK CONSTRAINT [FK_SuppliesPrescription_Prescription]
GO
ALTER TABLE [dbo].[SuppliesPrescription]  WITH CHECK ADD  CONSTRAINT [FK_SuppliesPrescription_Supplies] FOREIGN KEY([sId])
REFERENCES [dbo].[Supplies] ([sId])
GO
ALTER TABLE [dbo].[SuppliesPrescription] CHECK CONSTRAINT [FK_SuppliesPrescription_Supplies]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([roleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
