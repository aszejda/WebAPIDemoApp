namespace DataAccess.Models;

public class PriceModel
{
    public string SKU { get; set; } = string.Empty;
    public decimal NetWithDiscountPerSet { get; set; }
}
