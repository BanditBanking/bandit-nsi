using Bandit.NSI.Daemon.Models.DTOs;

namespace Bandit.NSI.Daemon.Services
{
    public interface IAccountsService
    {
        Task<SessionTokenDTO> Login(LoginDTO loginDTO, string ipAddress);
        Task<SessionTokenDTO> Register(RegisterDTO registerDTO, string ipAddress);
    }
}
