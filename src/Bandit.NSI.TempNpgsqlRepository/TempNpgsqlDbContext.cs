using Bandit.NSI.TempNpgsqlRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NSI.TempNpgsqlRepository
{
    public class TempNpgsqlDbContext : DbContext
    {
        public TempNpgsqlDbContext(DbContextOptions<TempNpgsqlDbContext> options) : base(options)
        {
        }

        public DbSet<Study> Studies { get; set; } = null!;
    }
}
