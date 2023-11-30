namespace DataAccess.Models;

public class PriceModel
{
    public string ID { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal NetPerUnit { get; set; }
    public decimal NetWithDiscount { get; set; }
    public decimal VAT { get; set; }
    public decimal NetWithDiscountPerSet { get; set; }
}
