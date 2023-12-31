USE [HCS]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 11/29/2023 11:06:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[cId] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[dob] [datetime] NOT NULL,
	[Address] [nvarchar](150) NOT NULL,
	[img] [nvarchar](300) NULL,
	[doctorId] [int] NULL,
	[patientId] [int] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[cId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/29/2023 11:06:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[doctorId] [int] NOT NULL,
	[serviceTypeId] [int] NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[doctorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExaminationResultId]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalRecord]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 11/29/2023 11:06:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[patientId] [int] NOT NULL,
	[serviceDetailName] [nvarchar](350) NOT NULL,
	[height] [tinyint] NULL,
	[weight] [tinyint] NULL,
	[blood group] [nvarchar](10) NULL,
	[blood pressure] [tinyint] NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[patientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prescription]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 11/29/2023 11:06:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[serviceId] [int] NOT NULL,
	[serviceTypeId] [int] NOT NULL,
	[serviceName] [nvarchar](150) NOT NULL,
	[price] [money] NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[serviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceMedicalRecord]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceSupplies]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplies]    Script Date: 11/29/2023 11:06:27 PM ******/
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
	[inputday] [datetime] NULL,
 CONSTRAINT [PK_Supplies] PRIMARY KEY CLUSTERED 
(
	[sId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesPrescription]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesType]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/29/2023 11:06:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 11/29/2023 11:06:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (1, N'Nguyen Hoang Nam', 1, N'098734534', CAST(N'1998-02-02T00:00:00.000' AS DateTime), N'Thach That - Ha Noi', NULL, 1, NULL)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (2, N'Hoang Van Hau', 1, N'093273253', CAST(N'2000-12-03T00:00:00.000' AS DateTime), N'Son Tay - Ha Noi', NULL, NULL, 5)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (3, N'Nguyen Hai Trieu', 1, N'0932847853', CAST(N'1997-11-22T10:07:03.293' AS DateTime), N'Tay Ho - Ha Noi', N'string', NULL, 1)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (4, N'Nguyễn Tiến Đạt', 1, N'0923847844', CAST(N'2000-11-22T10:09:29.223' AS DateTime), N'Son Loc - Sơn Tây - Hà Nội', N'string', NULL, 2)
GO
INSERT [dbo].[Employee] ([doctorId], [serviceTypeId], [userId]) VALUES (1, 1, 5)
GO
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId]) VALUES (1, 5, CAST(N'2023-11-22T03:24:11.837' AS DateTime), N'Đau Lưng', N'2', 1)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId]) VALUES (2, 5, CAST(N'2023-11-22T07:04:13.313' AS DateTime), N'Khó Thở, Tức Ngực', N'1', 1)
GO
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure]) VALUES (1, N'Sieu Am O Bung', NULL, NULL, NULL, NULL)
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure]) VALUES (2, N'Chup Lồng Ngực', NULL, NULL, NULL, NULL)
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure]) VALUES (5, N'XQuang Lồng Ngực, XQuang Cột Sống Thắt Lưng', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (2, N'Doctor')
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (3, N'Cashier')
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (4, N'Nurse')
GO
INSERT [dbo].[Service] ([serviceId], [serviceTypeId], [serviceName], [price]) VALUES (1, 1, N'XQuang Lồng Ngực', 120000.0000)
INSERT [dbo].[Service] ([serviceId], [serviceTypeId], [serviceName], [price]) VALUES (2, 2, N'Siêu Âm Ổ Bụng', 100000.0000)
INSERT [dbo].[Service] ([serviceId], [serviceTypeId], [serviceName], [price]) VALUES (3, 2, N'Siêu Âm Tuyến Giáp', 100000.0000)
INSERT [dbo].[Service] ([serviceId], [serviceTypeId], [serviceName], [price]) VALUES (4, 1, N'XQuang Cột Sống Thắt Lưng', 120000.0000)
INSERT [dbo].[Service] ([serviceId], [serviceTypeId], [serviceName], [price]) VALUES (5, 1, N'XQuang Cột Sống Cổ', 120000.0000)
INSERT [dbo].[Service] ([serviceId], [serviceTypeId], [serviceName], [price]) VALUES (6, 2, N'Siêu Âm Gối', 100000.0000)
INSERT [dbo].[Service] ([serviceId], [serviceTypeId], [serviceName], [price]) VALUES (7, 3, N'Xét Nghiệm HP', 150000.0000)
INSERT [dbo].[Service] ([serviceId], [serviceTypeId], [serviceName], [price]) VALUES (8, 3, N'Xét Nghiệm Máu', 150000.0000)
GO
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId]) VALUES (1, 1)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId]) VALUES (2, 2)
GO
INSERT [dbo].[ServiceType] ([serviceTypeId], [serviceTypeName]) VALUES (1, N'Xquang')
INSERT [dbo].[ServiceType] ([serviceTypeId], [serviceTypeName]) VALUES (2, N'Siêu Âm')
INSERT [dbo].[ServiceType] ([serviceTypeId], [serviceTypeName]) VALUES (3, N'Xét Nghiệm')
INSERT [dbo].[ServiceType] ([serviceTypeId], [serviceTypeName]) VALUES (4, N'Răng Hàm Mặt')
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([userId], [password], [email], [status]) VALUES (5, N'sonloc123', N'sonnk1@gmail.com', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
INSERT [dbo].[UserRole] ([userId], [roleId]) VALUES (5, 2)
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Doctor] FOREIGN KEY([doctorId])
REFERENCES [dbo].[Employee] ([doctorId])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Doctor]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Patient] FOREIGN KEY([patientId])
REFERENCES [dbo].[Patient] ([patientId])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Patient]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_ServiceType] FOREIGN KEY([serviceTypeId])
REFERENCES [dbo].[ServiceType] ([serviceTypeId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Doctor_ServiceType]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Doctor_User]
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
REFERENCES [dbo].[Employee] ([doctorId])
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
