using Bandit.NSI.AuthNpgsqlRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NSI.NpgsqlRepository
{
    public class AuthNpgsqlDbContext : DbContext
    {
        public AuthNpgsqlDbContext(DbContextOptions<AuthNpgsqlDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } = null!;
    }
}
