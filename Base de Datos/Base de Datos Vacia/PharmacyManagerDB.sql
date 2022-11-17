USE [PharmacyManagerDB]
GO
/****** Object:  Table [dbo].[DrugInfoSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DrugInfoSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Symptoms] [nvarchar](max) NOT NULL,
	[Presentation] [nvarchar](max) NOT NULL,
	[QuantityPerPresentation] [real] NOT NULL,
	[UnitOfMeasurement] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DrugInfoSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DrugSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DrugSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DrugCode] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Stock] [int] NOT NULL,
	[NeedsPrescription] [bit] NOT NULL,
	[DrugInfoId] [int] NOT NULL,
	[PharmacyId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_DrugSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvitationSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvitationSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[PharmacyId] [int] NULL,
	[Used] [bit] NOT NULL,
 CONSTRAINT [PK_InvitationSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionRoleSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionRoleSet](
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_PermissionRoleSet] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Endpoint] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PermissionSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PharmacySet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PharmacySet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_PharmacySet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseItem]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DrugId] [int] NOT NULL,
	[PharmacyId] [int] NOT NULL,
	[State] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[PurchaseId] [int] NULL,
 CONSTRAINT [PK_PurchaseItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalPrice] [float] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[UserEmail] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PurchaseSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RoleSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_SessionSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolicitudeItem]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolicitudeItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DrugQuantity] [int] NOT NULL,
	[DrugCode] [nvarchar](max) NOT NULL,
	[SolicitudeId] [int] NULL,
 CONSTRAINT [PK_SolicitudeItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolicitudeSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolicitudeSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[State] [int] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[PharmacyId] [int] NOT NULL,
 CONSTRAINT [PK_SolicitudeSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSet]    Script Date: 17/11/2022 16:01:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[OwnerPharmacyId] [int] NULL,
	[EmployeePharmacyId] [int] NULL,
	[RegistrationDate] [datetime2](7) NOT NULL,
	[PharmacyId] [int] NULL,
 CONSTRAINT [PK_UserSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[DrugSet]  WITH CHECK ADD  CONSTRAINT [FK_DrugSet_DrugInfoSet_DrugInfoId] FOREIGN KEY([DrugInfoId])
REFERENCES [dbo].[DrugInfoSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DrugSet] CHECK CONSTRAINT [FK_DrugSet_DrugInfoSet_DrugInfoId]
GO
ALTER TABLE [dbo].[DrugSet]  WITH CHECK ADD  CONSTRAINT [FK_DrugSet_PharmacySet_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[PharmacySet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DrugSet] CHECK CONSTRAINT [FK_DrugSet_PharmacySet_PharmacyId]
GO
ALTER TABLE [dbo].[InvitationSet]  WITH CHECK ADD  CONSTRAINT [FK_InvitationSet_PharmacySet_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[PharmacySet] ([Id])
GO
ALTER TABLE [dbo].[InvitationSet] CHECK CONSTRAINT [FK_InvitationSet_PharmacySet_PharmacyId]
GO
ALTER TABLE [dbo].[InvitationSet]  WITH CHECK ADD  CONSTRAINT [FK_InvitationSet_RoleSet_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvitationSet] CHECK CONSTRAINT [FK_InvitationSet_RoleSet_RoleId]
GO
ALTER TABLE [dbo].[PermissionRoleSet]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRoleSet_PermissionSet_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[PermissionSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermissionRoleSet] CHECK CONSTRAINT [FK_PermissionRoleSet_PermissionSet_PermissionId]
GO
ALTER TABLE [dbo].[PermissionRoleSet]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRoleSet_RoleSet_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermissionRoleSet] CHECK CONSTRAINT [FK_PermissionRoleSet_RoleSet_RoleId]
GO
ALTER TABLE [dbo].[PurchaseItem]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseItem_DrugSet_DrugId] FOREIGN KEY([DrugId])
REFERENCES [dbo].[DrugSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseItem] CHECK CONSTRAINT [FK_PurchaseItem_DrugSet_DrugId]
GO
ALTER TABLE [dbo].[PurchaseItem]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseItem_PharmacySet_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[PharmacySet] ([Id])
GO
ALTER TABLE [dbo].[PurchaseItem] CHECK CONSTRAINT [FK_PurchaseItem_PharmacySet_PharmacyId]
GO
ALTER TABLE [dbo].[PurchaseItem]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseItem_PurchaseSet_PurchaseId] FOREIGN KEY([PurchaseId])
REFERENCES [dbo].[PurchaseSet] ([Id])
GO
ALTER TABLE [dbo].[PurchaseItem] CHECK CONSTRAINT [FK_PurchaseItem_PurchaseSet_PurchaseId]
GO
ALTER TABLE [dbo].[SessionSet]  WITH CHECK ADD  CONSTRAINT [FK_SessionSet_UserSet_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SessionSet] CHECK CONSTRAINT [FK_SessionSet_UserSet_UserId]
GO
ALTER TABLE [dbo].[SolicitudeItem]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudeItem_SolicitudeSet_SolicitudeId] FOREIGN KEY([SolicitudeId])
REFERENCES [dbo].[SolicitudeSet] ([Id])
GO
ALTER TABLE [dbo].[SolicitudeItem] CHECK CONSTRAINT [FK_SolicitudeItem_SolicitudeSet_SolicitudeId]
GO
ALTER TABLE [dbo].[SolicitudeSet]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudeSet_PharmacySet_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[PharmacySet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolicitudeSet] CHECK CONSTRAINT [FK_SolicitudeSet_PharmacySet_PharmacyId]
GO
ALTER TABLE [dbo].[SolicitudeSet]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudeSet_UserSet_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[UserSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolicitudeSet] CHECK CONSTRAINT [FK_SolicitudeSet_UserSet_EmployeeId]
GO
ALTER TABLE [dbo].[UserSet]  WITH CHECK ADD  CONSTRAINT [FK_UserSet_PharmacySet_EmployeePharmacyId] FOREIGN KEY([EmployeePharmacyId])
REFERENCES [dbo].[PharmacySet] ([Id])
GO
ALTER TABLE [dbo].[UserSet] CHECK CONSTRAINT [FK_UserSet_PharmacySet_EmployeePharmacyId]
GO
ALTER TABLE [dbo].[UserSet]  WITH CHECK ADD  CONSTRAINT [FK_UserSet_PharmacySet_OwnerPharmacyId] FOREIGN KEY([OwnerPharmacyId])
REFERENCES [dbo].[PharmacySet] ([Id])
GO
ALTER TABLE [dbo].[UserSet] CHECK CONSTRAINT [FK_UserSet_PharmacySet_OwnerPharmacyId]
GO
ALTER TABLE [dbo].[UserSet]  WITH CHECK ADD  CONSTRAINT [FK_UserSet_PharmacySet_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[PharmacySet] ([Id])
GO
ALTER TABLE [dbo].[UserSet] CHECK CONSTRAINT [FK_UserSet_PharmacySet_PharmacyId]
GO
ALTER TABLE [dbo].[UserSet]  WITH CHECK ADD  CONSTRAINT [FK_UserSet_RoleSet_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserSet] CHECK CONSTRAINT [FK_UserSet_RoleSet_RoleId]
GO
