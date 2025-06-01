using System;

namespace Monitoring.Data.Entities;

public class Board
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public required string Name { get; set; }
}
