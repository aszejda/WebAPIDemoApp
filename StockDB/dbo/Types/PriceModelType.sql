CREATE TYPE [dbo].[PriceModelType] AS TABLE
(
    [Id] INT,
    [SKU] NVARCHAR(50), 
    [NetWithDiscountPerSet] DECIMAL(10,2)
);
