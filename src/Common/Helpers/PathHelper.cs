using System;
using System.IO;
using Monitoring.Common.Extensions;

namespace Monitoring.Common.Helpers;

public static class PathHelper
{
    /// <summary>
    ///     Returns the directory from which the application is run.
    /// </summary>
    public static readonly string AppDirectory = AppContext.BaseDirectory;

    /// <summary>
    ///     Returns the path of the roaming directory.
    /// </summary>
    public static readonly string RoamingDirectory = Environment.GetFolderPath(
        Environment.SpecialFolder.ApplicationData
    );

    /// <summary>
    ///     Returns the directory of the user directory (ex: C:\Users\[the name of the user])
    /// </summary>
    public static string UserDirectory =>
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    /// <summary>
    ///     Returns the path of the ApplicationData.
    /// </summary>
    public static readonly string DataDirectory =
        File.Exists(".portable") || Directory.Exists("data")
            ? AppDirectory.CombinePath("data")
            : RoamingDirectory.CombinePath(AppInformation.Name);

    /// <summary>
    ///     Returns the directory of the downloads directory
    /// </summary>
    public static readonly string DownloadsDirectory = UserDirectory.CombinePath("Downloads");

    public static readonly string LogsDirectory = DataDirectory.CombinePath("logs");

    public static readonly string SettingsPath = DataDirectory.CombinePath("settings.json");

    public static readonly string DatabasePath =
        $"Data Source={DataDirectory.CombinePath($"{AppInformation.Name.ToLowerInvariant()}.db")}";

    public static string GetParent(string currentPath, int levels = 1)
    {
        if (levels is 0)
        {
            levels = 1;
        }

        ArgumentOutOfRangeException.ThrowIfNegative(levels);
        var path = "";
        for (var i = 0; i < levels; i++)
            path += $"..{Path.DirectorySeparatorChar}";

        return Path.GetFullPath(Path.Combine(currentPath, path));
    }
}
