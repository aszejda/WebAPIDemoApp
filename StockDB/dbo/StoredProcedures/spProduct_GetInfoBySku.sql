CREATE PROCEDURE [dbo].[spProduct_GetInfoBySku]
	@SKU nvarchar(50)
AS
BEGIN
	SELECT P.name, P.EAN, P.producer_name, P.category, P.default_image, I.qty, I.unit, Pr.NetWithDiscountPerSet, I.shipping_cost
	FROM dbo.Products AS P
	INNER JOIN dbo.Inventory AS I ON I.product_id = P.ID
	INNER JOIN dbo.Prices AS Pr ON Pr.SKU = P.SKU
	WHERE P.SKU = @SKU
END

