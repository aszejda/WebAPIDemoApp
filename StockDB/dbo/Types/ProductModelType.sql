CREATE TYPE [dbo].[ProductModelType] AS TABLE
(
    [ID] INT,
    [SKU] NVARCHAR(50),
    [name] NVARCHAR(255),
    [EAN] NVARCHAR(14),
    [producer_name] NVARCHAR(100),
    [category] NVARCHAR(300),
    [is_wire] BIT,
    [available] BIT,
    [is_vendor] BIT,
    [default_image] VARCHAR(2048)
);