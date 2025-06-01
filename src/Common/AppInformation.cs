using System;

namespace Monitoring.Common;

public static class AppInformation
{
    public static string Name { get; } = AppDomain.CurrentDomain.FriendlyName;

    public static DateTime Born =>
        DateTime.SpecifyKind(new DateTime(2020, 4, 6, 20, 33, 14), DateTimeKind.Utc);
}
