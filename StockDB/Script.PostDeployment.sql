if not exists(select 1 from dbo.Products)
begin
	insert into dbo.[Products] (ID, SKU, name, EAN, producer_name, category, is_wire, available, is_vendor, default_image)
	values 
	(1, 'SKU-1', 'Product name 1', '3465897612564', 'ProducerOne', 'Category 1', 0, 1, 0, 'http://domain.com/image1.jpg');
end