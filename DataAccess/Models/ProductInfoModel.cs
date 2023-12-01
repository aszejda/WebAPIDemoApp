namespace DataAccess.Models;

public class ProductInfoModel
{    
    public string name { get; set; } = string.Empty;
    public string EAN { get; set; } = string.Empty;
    public string producer_name { get; set; } = string.Empty;
    public string category { get; set; } = string.Empty;
    public string default_image { get; set; } = string.Empty;
    public double qty { get; set; }
    public string unit {  get; set; } = string.Empty;
    public decimal NetPriceWithDiscountPerSet { get; set; }
    public decimal shipping_cost { get; set; }
}
