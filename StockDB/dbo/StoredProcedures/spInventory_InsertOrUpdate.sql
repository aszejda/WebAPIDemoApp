CREATE PROCEDURE [dbo].[spInventory_InsertOrUpdate]
    @InventoryData [dbo].[InventoryModelType] READONLY
AS
BEGIN
    MERGE INTO [dbo].[Inventory] AS target
    USING @InventoryData AS source
    ON target.product_id = source.product_id
    WHEN MATCHED THEN
        UPDATE SET
            [unit] = source.[unit], 
            [qty] = source.[qty], 
            [shipping_cost] = source.[shipping_cost]
    WHEN NOT MATCHED THEN
        INSERT ([product_id], [unit], [qty], [shipping_cost])
        VALUES (source.[product_id], source.[unit], source.[qty], source.[shipping_cost]);
END;
