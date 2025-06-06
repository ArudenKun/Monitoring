﻿using System;
using System.Linq;
using Avalonia.Data.Converters;
using ZLinq;

namespace Monitoring.Common.Converters;

public class FromStringToEnumConverter : IValueConverter
{
    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value == null)
            return null;
        var list = Enum.GetValues(value.GetType()).Cast<Enum>().AsValueEnumerable();
        return list.FirstOrDefault(vd => Equals(vd, value));
    }

    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value is null)
            return null;
        var list = Enum.GetValues(value.GetType()).Cast<Enum>().AsValueEnumerable();
        return list.FirstOrDefault(vd => Equals(vd, value));
    }
}
