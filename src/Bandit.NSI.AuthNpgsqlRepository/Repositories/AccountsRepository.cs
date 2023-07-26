using Bandit.NSI.AuthNpgsqlRepository.Exceptions;
using Bandit.NSI.AuthNpgsqlRepository.Models;
using Bandit.NSI.NpgsqlRepository;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NSI.AuthNpgsqlRepository.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly AuthNpgsqlDbContext _context;
        private readonly DbSet<Account> _accounts;

        public AccountsRepository(AuthNpgsqlDbContext context)
        {
            _context = context;
            _accounts = context.Accounts;
        }

        public async Task CreateAsync(Account account)
        {
            if (await _accounts.Where(a => a.Mail == account.Mail).AnyAsync())
                throw new AccountAlreadyRegisteredException(account.Mail);

            await _accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            var account = await _accounts.Where(a => a.Id == id).FirstOrDefaultAsync();

            if (account == null)
                throw new ResourceNotFoundException($"No account with id {id}");

            return account;
        }

        public async Task<Account> GetByMailAsync(string mail)
        {
            var account = await _accounts.Where(a => a.Mail == mail).FirstOrDefaultAsync();

            if (account == null)
                throw new ResourceNotFoundException($"No account with email {mail}");

            return account;
        }
    }
}
