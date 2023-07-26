using Bandit.NSI.Daemon.Models.DTOs;

namespace Bandit.NSI.Daemon.Mappers
{
    public static class AccountExtensions
    {
        public static AuthNpgsqlRepository.Models.Account ToModel(this RegisterDTO registerDto) => new()
        {
            Id = Guid.NewGuid(),
            Mail = registerDto.Mail,
            Password = registerDto.Password,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Role = AuthNpgsqlRepository.Models.Role.User
        };

        public static Models.Account ToContract(this AuthNpgsqlRepository.Models.Account account) => new()
        {
            Id = Guid.NewGuid(),
            Mail = account.Mail,
            FirstName = account.FirstName,
            LastName = account.LastName,
            Role = (Models.Role) account.Role
        };
    }
}
