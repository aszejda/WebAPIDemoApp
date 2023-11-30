namespace DataAccess.Models;

public class InventoryModel
{
    public int product_id { get; set; }
    public string sku { get; set; } = string.Empty;
    public string unit { get; set; } = string.Empty;
    public decimal qty { get; set; }
    public string shipping { get; set; } = string.Empty;
    public decimal shipping_cost { get; set; }
}
