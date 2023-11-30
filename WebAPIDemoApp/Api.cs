namespace WebAPIDemoApp;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapPost("/products/initilize", InitilizeProductInfo);
        app.MapGet("/products/{sku}", GetProductInfoBySKU);
    }

    private static async Task<IResult> InitilizeProductInfo(IProductInfoData data)
    {
        try
        {
            await data.InitilizeProductInfoData();
            return Results.Ok();
        }
        catch (Exception ex) { return Results.Problem(ex.Message); }
    }

    private static async Task<IResult> GetProductInfoBySKU(string sku, IProductInfoData data)
    {
        try
        {
            var results = await data.GetProductInfoBySku(sku);
            if (results is null) return Results.NotFound();
            return Results.Ok(results);
        }
        catch (Exception ex) { return Results.Problem(ex.Message); }
    }


}
