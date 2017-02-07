-- 26/10/2016 
USE ARIBA
GO

-- PROCEDURE insertFile
IF EXISTS (SELECT * FROM sys.procedures WHERE name='insertFile') BEGIN
	DROP PROCEDURE [dbo].[insertFile] 
END
GO
CREATE PROCEDURE [dbo].[insertFile] 
	@ChangeCode char(1),
    @SKU char(8),
	@VPN varchar(20),
	@Description1 varchar(31),
	@Description2 varchar(35),
	@CatSubcat char(4),
	@ExternalCategory varchar(20),
	@CatType varchar(20),
	@CustPrice money,
	@UnitsOfMeasure char(2),
	@VendorName varchar(20),
	@SubVendorName varchar(35),
	@BackOrder char(1),
	@Stock int,
	@SKUClass char(1),
	@CRC varchar(20),
	@Comp_IH_SW char(1),
	@SKUType char(1),
	@VendorNumber char(4),
	@MediaCode char(4),
	@DateInsert date,
	@@id int output
	AS
		BEGIN
			INSERT INTO [dbo].[PriceFile-29005207] ([ChangeCode],[SKU],[VPN],[Description1],[Description2],[CatSubcat],
											[ExternalCategory],[CatType],[CustPrice],[UnitsOfMeasure],[VendorName],[SubVendorName],[BackOrder],
											[Stock],[SKUClass],[CRC],[Comp_IH_SW],[SKUType],[VendorNumber],[MediaCode],[DateInsert])
				VALUES (
					@ChangeCode,
					@SKU,
					@VPN,
					@Description1,
					@Description2,
					@CatSubcat,
					@ExternalCategory,
					@CatType,
					@CustPrice,
					@UnitsOfMeasure,
					@VendorName,
					@SubVendorName,
					@BackOrder,
					@Stock,
					@SKUClass,
					@CRC,
					@Comp_IH_SW,
					@SKUType,
					@VendorNumber,
					@MediaCode,
					@DateInsert
				);
			Select @@id = @@identity
		END
SET ANSI_PADDING OFF
GO