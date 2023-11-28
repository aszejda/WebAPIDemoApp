CREATE TABLE [dbo].[Products]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SKU] NVARCHAR(50) NOT NULL, 
    [name] NVARCHAR(255) NOT NULL, 
    [EAN] NVARCHAR(14) NULL, 
    [producer_name] NVARCHAR(100) NULL, 
    [category] NVARCHAR(300) NULL,
    [is_wire] BIT NOT NULL, 
    [available] BIT NOT NULL, 
    [is_vendor] BIT NOT NULL, 
    [default_image] VARCHAR(2048) NULL
)
