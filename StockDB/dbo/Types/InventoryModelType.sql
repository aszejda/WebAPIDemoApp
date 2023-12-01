CREATE TYPE [dbo].[InventoryModelType] AS TABLE
(
    [id] INT,
    [product_id] INT,
    [unit] NVARCHAR(10), 
    [qty] DECIMAL(10,3), 
    [shipping_cost] DECIMAL(10,2)
);