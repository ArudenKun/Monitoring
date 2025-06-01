using System.ComponentModel;

namespace Monitoring.Models;

public enum ApplicationTheme
{
    [Description("Default from system")]
    Default,

    [Description("Light")]
    Light,

    [Description("Dark")]
    Dark,
}
