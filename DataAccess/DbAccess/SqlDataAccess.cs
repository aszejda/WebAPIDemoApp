using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using DataAccess.Models;

namespace DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveProductsBulk(string storedProcedure, IEnumerable<ProductModel> parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
        var dataTable = new DataTable();
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("SKU", typeof(string));
        dataTable.Columns.Add("name", typeof(string));
        dataTable.Columns.Add("EAN", typeof(string));
        dataTable.Columns.Add("producer_name", typeof(string));
        dataTable.Columns.Add("category", typeof(string));
        dataTable.Columns.Add("is_wire", typeof(bool));
        dataTable.Columns.Add("available", typeof(bool));
        dataTable.Columns.Add("is_vendor", typeof(bool));
        dataTable.Columns.Add("default_image", typeof(string));

        foreach (var parameter in parameters)
            dataTable.Rows.Add(parameter.ID, parameter.SKU, parameter.name, parameter.EAN, parameter.producer_name, parameter.category, parameter.is_wire, parameter.available, parameter.is_vendor, parameter.default_image);

        await connection.ExecuteAsync(storedProcedure, new { @ProductData = dataTable.AsTableValuedParameter("dbo.ProductModelType") }, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveInventoryBulk(string storedProcedure, IEnumerable<InventoryModel> parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
        var dataTable = new DataTable();
        dataTable.Columns.Add("id", typeof(int)).AutoIncrement = true;
        dataTable.Columns.Add("product_id", typeof(int));
        dataTable.Columns.Add("unit", typeof(string));
        dataTable.Columns.Add("qty", typeof(decimal));
        dataTable.Columns.Add("shipping_cost", typeof(decimal));

        foreach (var parameter in parameters)
            dataTable.Rows.Add(null, parameter.product_id, parameter.unit, parameter.qty, parameter.shipping_cost);

        await connection.ExecuteAsync(storedProcedure, new { @InventoryData = dataTable.AsTableValuedParameter("dbo.InventoryModelType") }, commandType: CommandType.StoredProcedure);
    }

    public async Task SavePricesBulk(string storedProcedure, IEnumerable<PriceModel> parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
        var dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int)).AutoIncrement = true;
        dataTable.Columns.Add("SKU", typeof(string));
        dataTable.Columns.Add("NetWithDiscountPerSet", typeof(decimal));

        foreach (var parameter in parameters)
            dataTable.Rows.Add(null, parameter.SKU, parameter.NetWithDiscountPerSet);

        await connection.ExecuteAsync(storedProcedure, new { @PriceData = dataTable.AsTableValuedParameter("dbo.PriceModelType") }, commandType: CommandType.StoredProcedure);
    }
}
