using Microsoft.EntityFrameworkCore;
using Persistence.Outbox;

namespace Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }
}
