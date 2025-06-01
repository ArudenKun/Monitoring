using System.Text.Json;
using System.Text.Json.Serialization;
using Monitoring.Common.Settings;

namespace Monitoring.Common;

[JsonSerializable(typeof(AppSettings))]
[JsonSourceGenerationOptions(
    JsonSerializerDefaults.General,
    WriteIndented = true,
    UseStringEnumConverter = true
)]
public sealed partial class AppJsonContext : JsonSerializerContext;
