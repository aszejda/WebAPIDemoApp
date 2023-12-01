CREATE PROCEDURE [dbo].[spPrice_InsertOrUpdate]
    @PriceData [dbo].[PriceModelType] READONLY
AS
BEGIN
    MERGE INTO [dbo].[Prices] AS target
    USING @PriceData AS source
    ON target.SKU = source.SKU
    WHEN MATCHED THEN
        UPDATE SET [NetWithDiscountPerSet] = source.[NetWithDiscountPerSet]
    WHEN NOT MATCHED THEN
        INSERT ([SKU], [NetWithDiscountPerSet])
        VALUES (source.[SKU], source.[NetWithDiscountPerSet]);
END;