using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class ProductInfoData : IProductInfoData
{
    private readonly ISqlDataAccess _db;

    public ProductInfoData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task InitilizeProductInfoData()
    {
        List<ProductModel> products = await CsvData.LoadProducts();
        List<InventoryModel> inventory = await CsvData.LoadInventory();
        List<PriceModel> prices = await CsvData.LoadPrices();

        await _db.SaveBulk(storedProcedure: "dbo.spProduct_InsertOrUpdate", products, "dbo.ProductModelType");

        //await _db.SaveProductsBulk(storedProcedure: "dbo.spProduct_InsertOrUpdate", products);
        await _db.SaveInventoryBulk(storedProcedure: "dbo.spInventory_InsertOrUpdate", inventory);
        await _db.SavePricesBulk(storedProcedure: "dbo.spPrice_InsertOrUpdate", prices);
    }

    public async Task<ProductInfoModel> GetProductInfoBySku(string sku)
    {
        var results = await _db.LoadData<ProductInfoModel, dynamic>(storedProcedure: "dbo.spProduct_GetInfoBySku", new { SKU = sku });
        if (results is null || results.Count() == 0) return new();
        return results.FirstOrDefault()!;
    }
}
