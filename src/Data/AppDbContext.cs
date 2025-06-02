using LinqToDB;
using Monitoring.Data.Entities;

namespace Monitoring.Data;

public sealed class AppDbContext : DataContext
{
    public AppDbContext(DataOptions<AppDbContext> options)
        : base(options.Options) { }

    public ITable<Board> Boards => this.GetTable<Board>();
}
