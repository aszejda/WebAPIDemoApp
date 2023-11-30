CREATE PROCEDURE [dbo].[spProduct_Get]
	@ID int
AS
BEGIN
	SELECT EAN, producer_name
	FROM dbo.[Products]
	WHERE ID = @ID;
END
