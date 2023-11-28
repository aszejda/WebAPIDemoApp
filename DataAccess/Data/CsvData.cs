using CsvHelper;
using CsvHelper.Configuration;
using DataAccess.Models;
using System.Globalization;

namespace DataAccess.Data;

public class CsvData
{
    public static async Task<List<ProductModel>> LoadDataFromCsv(string url)
    {
        List<ProductModel> products = new List<ProductModel>();
        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            BadDataFound = context => { },
            MissingFieldFound = null
        };

        using (HttpClient client = new HttpClient())
        using (var stream = await client.GetStreamAsync(url))
        using (var reader = new System.IO.StreamReader(stream))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            csv.Context.TypeConverterCache.AddConverter<bool>(new BoolTypeConverter());
            csv.Context.TypeConverterCache.AddConverter<int>(new Int32TypeConverter());            

            products = csv.GetRecords<ProductModel>()
                .Where(p => p.is_wire == false && p.shipping == "24h")
                .ToList();
        }

        return products;
    }
}
