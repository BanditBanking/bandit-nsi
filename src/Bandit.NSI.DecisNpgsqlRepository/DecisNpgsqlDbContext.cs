using Bandit.NSI.DecisNpgsqlRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NSI.DecisNpgsqlRepository
{
    public class DecisNpgsqlDbContext : DbContext
    {
        public DecisNpgsqlDbContext(DbContextOptions<DecisNpgsqlDbContext> options) : base(options)
        {
        }

        public DbSet<Publication> Publications { get; set; } = null!;
    }
}
