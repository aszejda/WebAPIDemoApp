namespace DataAccess.Models;

public class ProductModel
{
    public int? ID { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string EAN { get; set; } = string.Empty;
    public string producer_name { get; set; } = string.Empty;
    public string category { get; set; } = string.Empty;
    public bool is_wire { get; set; }
    public bool available { get; set; }
    public bool is_vendor { get; set; }
    public string default_image { get; set; } = string.Empty;
    public string shipping { get; set; } = string.Empty;
}

//public class ProductModel
//{
//    public int? ID { get; set; }
//    public string SKU { get; set; } = string.Empty;
//    public string name { get; set; } = string.Empty;
//    public string EAN { get; set; } = string.Empty;
//    public string producer_name { get; set; } = string.Empty;
//    public string category { get; set; } = string.Empty;
//    public bool is_wire { get; set; }
//    public bool available { get; set; }
//    public bool is_vendor { get; set; }
//    public string default_image { get; set; } = string.Empty;

//    // Dodatkowe pole zgodne z procedurą składowaną
//    public string shipping { get; set; } = string.Empty;
//}