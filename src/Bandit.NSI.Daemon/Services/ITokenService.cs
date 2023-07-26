using Bandit.NSI.AuthNpgsqlRepository.Models;
using Bandit.NSI.Daemon.Models;
using Bandit.NSI.Daemon.Models.DTOs;

namespace Bandit.NSI.Daemon.Services
{
    public interface ITokenService
    {
        SessionTokenDTO GenerateToken(Guid userId, string mail, AuthNpgsqlRepository.Models.Role role);
        Task<Models.Account> GetAccountAsync(string? token);
    }
}
