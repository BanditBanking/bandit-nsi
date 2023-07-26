using Bandit.NSI.AuthNpgsqlRepository.Models;

namespace Bandit.NSI.AuthNpgsqlRepository.Repositories
{
    public interface IAccountsRepository
    {
        Task<Account> GetByMailAsync(string mail);
        Task<Account> GetByIdAsync(Guid id);
        Task CreateAsync(Account account);
    }
}
