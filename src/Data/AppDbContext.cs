using Microsoft.EntityFrameworkCore;
using Monitoring.Data.Entities;

namespace Monitoring.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Board> Boards => Set<Board>();
}
