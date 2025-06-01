using System.Diagnostics.CodeAnalysis;

namespace Monitoring.Models;

public record DescriptionValue<T>
{
    public string Description { get; init; } = string.Empty;
    public required T Value { get; init; }

    public DescriptionValue() { }

    [SetsRequiredMembers]
    public DescriptionValue(string description, T value)
    {
        Description = description;
        Value = value;
    }
}
