CREATE TABLE [dbo].[Inventory]
(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [product_id] INT NOT NULL FOREIGN KEY REFERENCES Products(ID),
    [unit] NVARCHAR(10) NOT NULL, 
    [qty] DECIMAL(10,3) NOT NULL, 
    [shipping_cost] DECIMAL(10,2) NOT NULL
);
