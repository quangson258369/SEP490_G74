USE [HCS]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 12/16/2023 8:45:48 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 12/16/2023 8:45:48 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExaminationResultId]    Script Date: 12/16/2023 8:45:48 PM ******/
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
/****** Object:  Table [dbo].[Invoice]    Script Date: 12/16/2023 8:45:48 PM ******/
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
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 12/16/2023 8:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[invoiceDetailId] [int] NOT NULL,
	[invoiceId] [int] NOT NULL,
	[medicalRecordId] [int] NOT NULL,
	[isPrescription] [bit] NOT NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[invoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalRecord]    Script Date: 12/16/2023 8:45:48 PM ******/
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
	[PrescriptionId] [int] NULL,
 CONSTRAINT [PK_MedicalRecord] PRIMARY KEY CLUSTERED 
(
	[medicalRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 12/16/2023 8:45:48 PM ******/
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
	[allergieshistory] [nvarchar](350) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[patientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prescription]    Script Date: 12/16/2023 8:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prescription](
	[PrescriptionId] [int] IDENTITY(1,1) NOT NULL,
	[createDate] [date] NOT NULL,
	[diagnose] [nvarchar](550) NOT NULL,
 CONSTRAINT [PK_Prescription] PRIMARY KEY CLUSTERED 
(
	[PrescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/16/2023 8:45:48 PM ******/
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
/****** Object:  Table [dbo].[Service]    Script Date: 12/16/2023 8:45:48 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceMedicalRecord]    Script Date: 12/16/2023 8:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceMedicalRecord](
	[serviceId] [int] NOT NULL,
	[medicalRecordId] [int] NOT NULL,
	[doctorId] [int] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_ServiceMedicalRecord] PRIMARY KEY CLUSTERED 
(
	[serviceId] ASC,
	[medicalRecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 12/16/2023 8:45:48 PM ******/
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
/****** Object:  Table [dbo].[Supplies]    Script Date: 12/16/2023 8:45:48 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesPrescription]    Script Date: 12/16/2023 8:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesPrescription](
	[sId] [int] NOT NULL,
	[prescriptionId] [int] NOT NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_SuppliesPrescription] PRIMARY KEY CLUSTERED 
(
	[sId] ASC,
	[prescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesType]    Script Date: 12/16/2023 8:45:48 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 12/16/2023 8:45:48 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 12/16/2023 8:45:48 PM ******/
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
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (1, N'Nguyen Hoang Nam', 1, N'098734534', CAST(N'1998-02-02T00:00:00.000' AS DateTime), N'Cam Yen - Thach That - Ha Noi', NULL, 1, NULL)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (2, N'Hoang Van Nam', 1, N'093273983', CAST(N'2001-05-22T00:00:00.000' AS DateTime), N'Sơn Lộc - Son Tay - Ha Noi', NULL, NULL, 5)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (3, N'Nguyen Hai Trieu', 1, N'0932847853', CAST(N'1997-11-22T00:00:00.000' AS DateTime), N'Quan Tay Ho - Ha Noi', NULL, NULL, 1)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (4, N'Nguyễn Tiến Nam', 1, N'0923847006', CAST(N'2000-11-22T00:00:00.000' AS DateTime), N'Sơn Lộc - Sơn Tây - Hà Nội', NULL, NULL, 2)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (5, N'kodko', 1, N'0934859384', CAST(N'2023-08-02T00:00:00.000' AS DateTime), N'Whyyyyyyy', NULL, NULL, 6)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (6, N'lplpl', 1, N'0943858334', CAST(N'2023-11-29T00:00:00.000' AS DateTime), N'kokdskf 3928,2', NULL, NULL, 7)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (7, N'La Việt Khoa', 1, N'0932847832', CAST(N'1991-07-16T00:00:00.000' AS DateTime), N'Sơn Lộc - Sơn Tây - Hà Nội', NULL, 2, NULL)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (8, N'Nguyen Ngoc Huyen', 0, N'0934584755', CAST(N'2023-07-05T00:00:00.000' AS DateTime), N'Thanh Xuân - Hà Nội', NULL, NULL, 8)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (9, N'Nguyen Khoa Dang', 1, N'', CAST(N'2023-12-06T00:00:00.000' AS DateTime), N'Sơn Lộc - Sơn Tây - Hà Nội', NULL, NULL, 9)
INSERT [dbo].[Contact] ([cId], [Name], [Gender], [Phone], [dob], [Address], [img], [doctorId], [patientId]) VALUES (10, N'Hoàng Ngọc Lâm', 1, N'93273983', CAST(N'2003-01-09T00:00:00.000' AS DateTime), N'110/10 Kim Sơn, Sơn Tây, Hà Nội', NULL, NULL, 10)
GO
INSERT [dbo].[Employee] ([doctorId], [serviceTypeId], [userId]) VALUES (1, 1, 5)
INSERT [dbo].[Employee] ([doctorId], [serviceTypeId], [userId]) VALUES (2, 1, 6)
GO
INSERT [dbo].[ExaminationResultId] ([examResultId], [medicalRecordID], [doctorId], [diagnosis], [conclusion], [examDate], [serviceId]) VALUES (1, 1, 1, N'Phổi có bóng mờ', N'Tổn thương phổi độ 1', CAST(N'2023-12-03T04:27:37.187' AS DateTime), 1)
GO
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (1, 5, CAST(N'2023-12-15T10:54:21.750' AS DateTime), N'Đau Lưng', N'1', 2, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (2, 5, CAST(N'2023-12-09T21:38:07.890' AS DateTime), N'Khó Thở, Tức Ngực', N'1', 1, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (5, 2, CAST(N'2023-12-14T17:13:33.577' AS DateTime), N'ho nhiều liên tục', N'2', 1, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (7, 8, CAST(N'2023-12-13T17:39:43.353' AS DateTime), N'tức ngực', N'3', 1, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (8, 1, CAST(N'2023-12-12T17:41:38.910' AS DateTime), N'', N'4', 1, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (9, 2, CAST(N'2023-12-13T16:34:48.677' AS DateTime), N'', N'2', 1, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (10, 1, CAST(N'2023-12-15T10:24:32.160' AS DateTime), N'đau lưng dai dẳng, mỏi vai gáy', N'4', 2, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (11, 1, CAST(N'2023-12-15T10:32:14.770' AS DateTime), N'Kiểm Tra Tình Trạng Thoái Hoá', N'4', 2, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (12, 1, CAST(N'2023-12-15T10:32:32.690' AS DateTime), N'Oke', N'4', 2, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (13, 1, CAST(N'2023-12-15T10:48:15.390' AS DateTime), N'test add lại với exam code tăng', N'5', 2, NULL)
INSERT [dbo].[MedicalRecord] ([medicalRecordID], [patientId], [medicalRecordDate], [examReason], [examCode], [doctorId], [PrescriptionId]) VALUES (14, 10, CAST(N'2023-12-15T11:19:15.833' AS DateTime), N'kiểm tra', N'6', 2, NULL)
GO
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure], [allergieshistory]) VALUES (1, N'XQuang Lồng Ngực,XQuang Cột Sống Thắt Lưng,XQuang Cột Sống Cổ', 172, 62, N'A', 0, NULL)
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure], [allergieshistory]) VALUES (2, N'XQuang Lồng Ngực,XQuang Cột Sống Thắt Lưng', 168, 64, N'', 110, NULL)
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure], [allergieshistory]) VALUES (5, N'XQuang Lồng Ngực,XQuang Cột Sống Thắt Lưng', 172, 65, N'', 110, NULL)
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure], [allergieshistory]) VALUES (6, N'Chưa Chọn Dịch Vụ Khám', 160, 0, N'', 0, NULL)
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure], [allergieshistory]) VALUES (7, N'Siêu Âm Ổ Bụng,Siêu Âm Tuyến Giáp', 165, 52, N'O', NULL, NULL)
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure], [allergieshistory]) VALUES (8, N'XQuang Lồng Ngực,XQuang Cột Sống Thắt Lưng', 0, 0, N'', 0, NULL)
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure], [allergieshistory]) VALUES (9, N'chuan bi xoa bo', 170, 64, N'', 110, NULL)
INSERT [dbo].[Patient] ([patientId], [serviceDetailName], [height], [weight], [blood group], [blood pressure], [allergieshistory]) VALUES (10, N'XQuang Cột Sống Thắt Lưng,XQuang Cột Sống Cổ', 172, 66, N'', 120, NULL)
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
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (1, 1, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (1, 5, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (1, 7, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (1, 8, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (1, 10, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (1, 11, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (1, 12, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (1, 13, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (2, 2, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (4, 1, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (4, 5, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (4, 7, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (4, 9, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (4, 13, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (4, 14, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (5, 8, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (5, 10, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (5, 11, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (5, 12, 1, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (5, 13, 2, 0)
INSERT [dbo].[ServiceMedicalRecord] ([serviceId], [medicalRecordId], [doctorId], [status]) VALUES (5, 14, 2, 0)
GO
INSERT [dbo].[ServiceType] ([serviceTypeId], [serviceTypeName]) VALUES (1, N'Xquang')
INSERT [dbo].[ServiceType] ([serviceTypeId], [serviceTypeName]) VALUES (2, N'Siêu Âm')
INSERT [dbo].[ServiceType] ([serviceTypeId], [serviceTypeName]) VALUES (3, N'Xét Nghiệm')
INSERT [dbo].[ServiceType] ([serviceTypeId], [serviceTypeName]) VALUES (4, N'Răng Hàm Mặt')
GO
SET IDENTITY_INSERT [dbo].[Supplies] ON 

INSERT [dbo].[Supplies] ([sId], [sName], [uses], [exp], [distributor], [unitInStock], [price], [suppliesTypeId], [inputday]) VALUES (1, N'Kim Tiêm', N'Kim Tiêm kích thước 0.05mm', CAST(N'2024-06-21' AS Date), N'VinaDex', 12, 0.0000, 1, CAST(N'2023-12-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Supplies] OFF
GO
INSERT [dbo].[SuppliesType] ([suppliesTypeId], [suppliesTypeName]) VALUES (1, N'Dụng Cụ Y Tế')
INSERT [dbo].[SuppliesType] ([suppliesTypeId], [suppliesTypeName]) VALUES (2, N'Thuốc')
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([userId], [password], [email], [status]) VALUES (5, N'sonloc123', N'sonnk1@gmail.com', 1)
INSERT [dbo].[User] ([userId], [password], [email], [status]) VALUES (6, N'd0c406e82877aacad00415ca64f821e9', N'vkhoa871@gmail.com', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
INSERT [dbo].[UserRole] ([userId], [roleId]) VALUES (5, 2)
INSERT [dbo].[UserRole] ([userId], [roleId]) VALUES (6, 1)
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
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_MedicalRecord] FOREIGN KEY([medicalRecordId])
REFERENCES [dbo].[MedicalRecord] ([medicalRecordID])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_MedicalRecord]
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
ALTER TABLE [dbo].[MedicalRecord]  WITH CHECK ADD  CONSTRAINT [FK_MedicalRecord_Prescription] FOREIGN KEY([PrescriptionId])
REFERENCES [dbo].[Prescription] ([PrescriptionId])
GO
ALTER TABLE [dbo].[MedicalRecord] CHECK CONSTRAINT [FK_MedicalRecord_Prescription]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceType] FOREIGN KEY([serviceTypeId])
REFERENCES [dbo].[ServiceType] ([serviceTypeId])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ServiceType]
GO
ALTER TABLE [dbo].[ServiceMedicalRecord]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMedicalRecord_Employee] FOREIGN KEY([doctorId])
REFERENCES [dbo].[Employee] ([doctorId])
GO
ALTER TABLE [dbo].[ServiceMedicalRecord] CHECK CONSTRAINT [FK_ServiceMedicalRecord_Employee]
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
