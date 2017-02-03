SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


-- 26/10/2016 
USE ARIBA
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='CustomerInfo')
BEGIN
	CREATE TABLE [dbo].[CustomerInfo](
		[id] [int] IDENTITY(1,1) NOT NULL,
		[BrCustNbr] [char](8) NOT NULL,
		[BrCustName] [varchar](35) NULL,
		[Ariba_SuplierDomain] [varchar](50) NULL,
		[Ariba_SupplierID] [varchar](50) NULL,
		[Ariba_TEST] [char] (1) NULL,   -- Y/N
		[ImpFTP_Server] [varchar](255) NULL,
		[ImpFTP_User] [varchar](20) NULL,
		[ImpFTP_Password] [varchar](20) NULL,
		[ImpFTP_Path]  [varchar](50) NULL,
		[ImpFTP_FileName] [varchar](12) NULL,
		[ImpFTP_Type] [char] (1) NULL,   -- F/P
		CONSTRAINT pk_CustomerInfo PRIMARY KEY (id)
	) ON [PRIMARY]
END
GO


SET ANSI_PADDING OFF
GO
