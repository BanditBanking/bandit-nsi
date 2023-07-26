using System.ComponentModel.DataAnnotations;

namespace Bandit.NSI.Daemon.Models.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Mail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
