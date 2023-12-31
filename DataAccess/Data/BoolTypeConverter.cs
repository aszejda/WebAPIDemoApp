﻿using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;

namespace DataAccess.Data;

public class BoolTypeConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        if (int.TryParse(text, out int intValue))
        {
            return intValue != 0;
        }

        return base.ConvertFromString(text, row, memberMapData);
    }
}


