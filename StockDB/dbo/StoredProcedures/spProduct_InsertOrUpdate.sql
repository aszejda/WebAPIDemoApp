CREATE PROCEDURE [dbo].[spProduct_InsertOrUpdate]
    @ProductData [dbo].[ProductModelType] READONLY
AS
BEGIN
    MERGE INTO [dbo].[Products] AS target
    USING @ProductData AS source
    ON target.ID = source.ID
    WHEN MATCHED THEN
        UPDATE SET
            [SKU] = source.[SKU],
            [name] = source.[name],
            [EAN] = source.[EAN],
            [producer_name] = source.[producer_name],
            [category] = source.[category],
            [is_wire] = source.[is_wire],
            [available] = source.[available],
            [is_vendor] = source.[is_vendor],
            [default_image] = source.[default_image]
    WHEN NOT MATCHED THEN
        INSERT ([ID], [SKU], [name], [EAN], [producer_name], [category], [is_wire], [available], [is_vendor], [default_image])
        VALUES (source.[ID], source.[SKU], source.[name], source.[EAN], source.[producer_name], source.[category],
                source.[is_wire], source.[available], source.[is_vendor], source.[default_image]);
END;
