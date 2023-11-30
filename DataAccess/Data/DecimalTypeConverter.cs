using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;

namespace DataAccess.Data;

public class DecimalTypeConverter : DecimalConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {

        if (string.IsNullOrWhiteSpace(text) || text == "O")
        {
            return 0m;
        }
        else text = text.Replace(",", ".");

        return base.ConvertFromString(text, row, memberMapData);
    }
}