using Bandit.NSI.Daemon.Models;
using System.ComponentModel.DataAnnotations;

namespace Bandit.NSI.Daemon.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }
}
