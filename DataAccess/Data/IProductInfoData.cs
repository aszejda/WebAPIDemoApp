using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IProductInfoData
    {
        Task<ProductInfoModel> GetProductInfoBySku(string sku);
        Task InitilizeProductInfoData();
    }
}