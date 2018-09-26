USE [BD_NFeS]
GO
/****** Object:  Table [dbo].[Certificates]    Script Date: 26/09/2018 14:36:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Certificates](
	[CertificateId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[Certificate] [image] NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NULL,
 CONSTRAINT [PK_Certificates] PRIMARY KEY CLUSTERED 
(
	[CertificateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[CFPS]    Script Date: 26/09/2018 14:36:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CFPS](
	[CFPSId] [bigint] IDENTITY(1,1) NOT NULL,
	[CFPS] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_CFPS] PRIMARY KEY CLUSTERED 
(
	[CFPSId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Companys]    Script Date: 26/09/2018 14:36:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companys](
	[CompanyId] [bigint] IDENTITY(1,1) NOT NULL,
	[CNPJ] [nvarchar](14) NOT NULL,
	[IM] [nvarchar](10) NOT NULL,
	[IE] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[NameFantasy] [nvarchar](1000) NOT NULL,
	[CEP] [nvarchar](8) NOT NULL,
	[Street] [nvarchar](1000) NOT NULL,
	[Neighborhood] [nvarchar](1000) NOT NULL,
	[City] [nvarchar](1000) NOT NULL,
	[State] [nvarchar](2) NOT NULL,
	[Telephone] [nvarchar](11) NULL,
	[Email] [nvarchar](500) NULL,
	[Logo] [image] NULL,
	[IRRF] [decimal](18, 6) NOT NULL,
	[PIS] [decimal](18, 6) NOT NULL,
	[COFINS] [decimal](18, 6) NOT NULL,
	[CSLL] [decimal](18, 6) NOT NULL,
	[INSS] [decimal](18, 6) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[NFeS]    Script Date: 26/09/2018 14:36:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NFeS](
	[NFeSId] [bigint] IDENTITY(1,1) NOT NULL,
	[TakerId] [bigint] NOT NULL,
	[DateEmission] [datetime] NOT NULL,
	[RPS] [nvarchar](10) NOT NULL,
	[DateEmissionRPS] [nvarchar](10) NOT NULL,
	[ServicesProvied] [nvarchar](1000) NOT NULL,
	[LocalService] [nvarchar](10) NOT NULL,
	[Note] [nvarchar](max) NOT NULL,
	[TaxWithheld] [decimal](18, 6) NOT NULL,
	[TotalISS] [decimal](18, 6) NOT NULL,
	[TotalIRRF] [decimal](18, 6) NOT NULL,
	[TotalPIS] [decimal](18, 6) NOT NULL,
	[TotalINSS] [decimal](18, 6) NOT NULL,
	[TotalCONFIS] [decimal](18, 6) NOT NULL,
	[TotalCSLL] [decimal](18, 6) NOT NULL,
	[TotalOther] [decimal](18, 6) NOT NULL,
	[TotalServices] [decimal](18, 6) NOT NULL,
	[ValueDeductions] [decimal](18, 6) NOT NULL,
	[TotalNFeS] [decimal](18, 6) NOT NULL,
 CONSTRAINT [PK_NFeS] PRIMARY KEY CLUSTERED 
(
	[NFeSId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[NFeSInvoices]    Script Date: 26/09/2018 14:36:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NFeSInvoices](
	[NFeSInvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[NFeSId] [bigint] NOT NULL,
	[Number] [nvarchar](10) NOT NULL,
	[Maturity] [date] NOT NULL,
	[Value] [decimal](18, 6) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_NFeSInvoce] PRIMARY KEY CLUSTERED 
(
	[NFeSInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[NFeSItens]    Script Date: 26/09/2018 14:36:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NFeSItens](
	[NFeSItensId] [bigint] IDENTITY(1,1) NOT NULL,
	[NFesId] [bigint] NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[ActivityCode] [nvarchar](10) NOT NULL,
	[UnitaryValue] [decimal](18, 6) NOT NULL,
	[Aliquot] [decimal](18, 6) NOT NULL,
	[TaxWithheld] [bit] NOT NULL,
	[ValueTaxEstimated] [decimal](18, 6) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_NFeSItens] PRIMARY KEY CLUSTERED 
(
	[NFeSItensId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Services]    Script Date: 26/09/2018 14:36:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[ServicesId] [bigint] IDENTITY(1,1) NOT NULL,
	[Unity] [nvarchar](10) NOT NULL,
	[Value] [decimal](18, 6) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IRRF] [decimal](18, 6) NOT NULL,
	[PIS] [decimal](18, 6) NOT NULL,
	[CSLL] [decimal](18, 6) NOT NULL,
	[INSS] [decimal](18, 6) NOT NULL,
	[COFINS] [decimal](18, 6) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[ServicesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Session]    Script Date: 26/09/2018 14:36:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[SessionHash] [nvarchar](max) NOT NULL,
	[DateStart] [datetime] NOT NULL,
	[DateEnd] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[ShippingCompany]    Script Date: 26/09/2018 14:36:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingCompany](
	[ShippingCompanyId] [bigint] IDENTITY(1,1) NOT NULL,
	[CPF_CNPJ] [nvarchar](14) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[NameFantasy] [nvarchar](1000) NOT NULL,
	[CEP] [nvarchar](8) NOT NULL,
	[Street] [nvarchar](1000) NOT NULL,
	[Neighborhood] [nvarchar](100) NOT NULL,
	[City] [nvarchar](1000) NOT NULL,
	[State] [nvarchar](2) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_ShippingCompany] PRIMARY KEY CLUSTERED 
(
	[ShippingCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Takers]    Script Date: 26/09/2018 14:36:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Takers](
	[TakerId] [bigint] IDENTITY(1,1) NOT NULL,
	[IM] [nvarchar](10) NOT NULL,
	[CPF_CNPJ] [nvarchar](14) NOT NULL,
	[RG_IE] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[NameFantasy] [nvarchar](1000) NOT NULL,
	[TypePerson] [char](1) NOT NULL,
	[CEP] [nvarchar](8) NOT NULL,
	[Street] [nvarchar](1000) NOT NULL,
	[Neighborhood] [nvarchar](1000) NOT NULL,
	[City] [nvarchar](1000) NOT NULL,
	[State] [nvarchar](2) NOT NULL,
	[Email] [nvarchar](500) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NULL,
 CONSTRAINT [PK_Taker] PRIMARY KEY CLUSTERED 
(
	[TakerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaxpayerActivities]    Script Date: 26/09/2018 14:36:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxpayerActivities](
	[TaxpayerActivitiesId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CNAE] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Aliquot] [decimal](18, 6) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_TaxpayerActivities] PRIMARY KEY CLUSTERED 
(
	[TaxpayerActivitiesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Users]    Script Date: 26/09/2018 14:36:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](500) NOT NULL,
	[CPF] [nvarchar](14) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[DateUpdate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[Certificates]  WITH CHECK ADD  CONSTRAINT [FK_Certificates_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companys] ([CompanyId])
GO
ALTER TABLE [dbo].[Certificates] CHECK CONSTRAINT [FK_Certificates_Company]
GO
ALTER TABLE [dbo].[NFeSInvoices]  WITH CHECK ADD  CONSTRAINT [FK_NFeSInvoce_NFeS] FOREIGN KEY([NFeSId])
REFERENCES [dbo].[NFeS] ([NFeSId])
GO
ALTER TABLE [dbo].[NFeSInvoices] CHECK CONSTRAINT [FK_NFeSInvoce_NFeS]
GO
ALTER TABLE [dbo].[NFeSItens]  WITH CHECK ADD  CONSTRAINT [FK_NFeSItens_NFeS] FOREIGN KEY([NFesId])
REFERENCES [dbo].[NFeS] ([NFeSId])
GO
ALTER TABLE [dbo].[NFeSItens] CHECK CONSTRAINT [FK_NFeSItens_NFeS]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Users]
GO
ALTER TABLE [dbo].[TaxpayerActivities]  WITH CHECK ADD  CONSTRAINT [FK_TaxpayerActivities_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companys] ([CompanyId])
GO
ALTER TABLE [dbo].[TaxpayerActivities] CHECK CONSTRAINT [FK_TaxpayerActivities_Company]
GO
