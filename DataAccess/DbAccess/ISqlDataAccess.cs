using DataAccess.Models;

namespace DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
        Task SaveDataBulk(string storedProcedure, IEnumerable<ProductModel> parameters, string connectionId = "Default");
        Task SaveInventoryBulk(string storedProcedure, IEnumerable<InventoryModel> parameters, string connectionId = "Default");
    }
}