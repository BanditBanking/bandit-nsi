using Bandit.NSI.AuthNpgsqlRepository.Repositories;
using Bandit.NSI.Daemon.Exceptions;
using Bandit.NSI.Daemon.Models.DTOs;
using Bandit.NSI.Daemon.Mappers;
using Bandit.NSI.AuthNpgsqlRepository.Exceptions;

namespace Bandit.NSI.Daemon.Services
{
    public class AccountsService : IAccountsService
    {
        private IAccountsRepository _accountsRepository;
        private ITokenService _tokenService;

        public AccountsService(IAccountsRepository accountsRepository, ITokenService tokenService)
        {
            _accountsRepository = accountsRepository;
            _tokenService = tokenService;
        }

        public async Task<SessionTokenDTO> Login(LoginDTO loginDTO, string ipAddress)
        {
            try
            {
                var account = await _accountsRepository.GetByMailAsync(loginDTO.Mail);
                if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, account.Password))
                    throw new InvalidCredentialsException();
                return _tokenService.GenerateToken(account.Id, account.Mail, account.Role);
            } catch(ResourceNotFoundException)
            {
                throw new InvalidCredentialsException();
            }
        }

        public async Task<SessionTokenDTO> Register(RegisterDTO registerDTO, string ipAddress)
        {
            try
            {
                registerDTO.Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
                var newAccount = registerDTO.ToModel();
                await _accountsRepository.CreateAsync(newAccount);
                return _tokenService.GenerateToken(newAccount.Id, newAccount.Mail, newAccount.Role);
            } catch(AuthNpgsqlRepository.Exceptions.AccountAlreadyRegisteredException)
            {
                throw new Exceptions.AccountAlreadyRegisteredException(registerDTO.Mail);
            }
        }
    }
}
