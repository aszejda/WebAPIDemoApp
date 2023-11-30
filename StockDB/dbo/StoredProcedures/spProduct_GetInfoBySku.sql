CREATE PROCEDURE [dbo].[spProduct_GetInfoBySku]
	@SKU nvarchar(50)
AS
BEGIN
	SELECT name, EAN, producer_name, category, default_image
	FROM dbo.[Products]
	WHERE SKU = @SKU;
END

