-- 26/10/2016 
USE ARIBA
GO

-- PROCEDURE insertCustomer
IF EXISTS (SELECT * FROM sys.procedures WHERE name='insertCustomer') BEGIN
	DROP PROCEDURE [dbo].[insertCustomer] 
END
GO
CREATE PROCEDURE [dbo].[insertCustomer] 
	@BrCustNbr char(8),
    @BrCustName varchar(35),
	@Ariba_SuplierDomain varchar(50),
	@Ariba_SupplierID varchar(50),
	@Ariba_TEST char(1),
	@ImpFTP_Server varchar(255),
	@ImpFTP_User varchar(20),
	@ImpFTP_Password varchar(20),
	@ImpFTP_Path varchar(50),
	@ImpFTP_FileName varchar(12),
	@ImpFTP_Type char(1),
	@@id int output
	AS
		BEGIN
			INSERT INTO [dbo].[CustomerInfo] ([BrCustNbr],[BrCustName],[Ariba_SuplierDomain],[Ariba_SupplierID],[Ariba_TEST],[ImpFTP_Server],
											[ImpFTP_User],[ImpFTP_Password],[ImpFTP_Path],[ImpFTP_FileName],[ImpFTP_Type])
				VALUES (
					@BrCustNbr,
					@BrCustName,
					@Ariba_SuplierDomain,
					@Ariba_SupplierID,
					@Ariba_TEST,
					@ImpFTP_Server,
					@ImpFTP_User,
					@ImpFTP_Password,
					@ImpFTP_Path,
					@ImpFTP_FileName,
					@ImpFTP_Type
				);
			Select @@id = @@identity
		END
SET ANSI_PADDING OFF
GO


