using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;

namespace DataAccess.Data;

public class Int32TypeConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        
        if (int.TryParse(text, out int intValue))
        {
            return intValue;
        }

        return null; 
    }
}
