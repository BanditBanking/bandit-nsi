using System.ComponentModel.DataAnnotations;

namespace Bandit.NSI.AuthNpgsqlRepository.Models
{
    public class Account
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Mail { get; set; }

        [Required]
        [MaxLength(72)]
        public string Password { get; set; }

        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;

        [Required]
        public Role Role { get; set; } = Role.User;
    }
}
