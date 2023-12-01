using CsvHelper;
using CsvHelper.Configuration;
using DataAccess.Models;
using System.Globalization;

namespace DataAccess.Data;

public class CsvData
{
    private static List<int?> _productIDsWith24HShipping = new();
    private static List<string> _productSKUsWith24HShipping = new();

    public static async Task<List<ProductModel>> LoadProducts()
    {
        string url = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv";
        string filePath = Directory.GetCurrentDirectory() + @"\Products.csv";

        await DownloadFileIfNotExists(url, filePath);

        List<ProductModel> products = new List<ProductModel>();
        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            BadDataFound = context => { },
            MissingFieldFound = null
        };

        using (var reader = new System.IO.StreamReader(filePath))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            csv.Context.TypeConverterCache.AddConverter<bool>(new BoolTypeConverter());
            csv.Context.TypeConverterCache.AddConverter<int>(new Int32TypeConverter());

            products = csv.GetRecords<ProductModel>()
                .Where(p => p.is_wire == false && (p.shipping == "24h" || p.shipping == "Wysyłka w 24h"))
                .ToList();
        }
        if (products is not null)
        {
            _productIDsWith24HShipping = products.Select(p => p.ID).ToList();
            _productSKUsWith24HShipping = products.Select(p => p.SKU).ToList();
        }


        return products;
    }

    public static async Task<List<InventoryModel>> LoadInventory()
    {
        string url = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv";
        string filePath = Directory.GetCurrentDirectory() + @"\Inventory.csv";

        await DownloadFileIfNotExists(url, filePath);
        if (_productIDsWith24HShipping.Count == 0) await LoadProducts();

        List<InventoryModel> inventory = new List<InventoryModel>();
        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
            BadDataFound = context => { },
            MissingFieldFound = null
        };

        using (var reader = new System.IO.StreamReader(filePath))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            csv.Context.TypeConverterCache.AddConverter<decimal>(new DecimalTypeConverter());
            inventory = csv.GetRecords<InventoryModel>()
                .Where(p => _productIDsWith24HShipping.Contains(p.product_id))
                .ToList();
        }

        return inventory;
    }

    public static async Task<List<PriceModel>> LoadPrices()
    {
        string url = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv";
        string filePath = Directory.GetCurrentDirectory() + @"\Prices.csv";

        await DownloadFileIfNotExists(url, filePath);
        if (_productIDsWith24HShipping.Count == 0) await LoadProducts();

        List<PriceModel> prices = new List<PriceModel>();
        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = false,
            BadDataFound = context => { },
            MissingFieldFound = null
        };

        using (var reader = new System.IO.StreamReader(filePath))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            csv.Context.TypeConverterCache.AddConverter<decimal>(new DecimalTypeConverter());
            csv.Context.RegisterClassMap<PriceModelMap>();

            var records = csv.GetRecords<PriceModel>().ToList();

            Parallel.ForEach(records, price =>
            {
                if (_productSKUsWith24HShipping.Contains(price.SKU))
                {
                    lock (prices)
                    {
                        prices.Add(price);
                        File.AppendAllText(Directory.GetCurrentDirectory() + @"\Log.txt", price.SKU + Environment.NewLine);
                    }
                }
            });
        }

        return prices;
    }


    public static async Task DownloadFileIfNotExists(string url, string localFilePath)
    {
        if (File.Exists(localFilePath)) return;
        
        using (HttpClient client = new HttpClient())
        {
            using (var stream = await client.GetStreamAsync(url))
            using (var fileStream = new FileStream(localFilePath, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}

public sealed class PriceModelMap : ClassMap<PriceModel>
{
    public PriceModelMap()
    {
        Map(m => m.SKU).Index(1); 
        Map(m => m.NetWithDiscountPerSet).Index(5);
    }
}
