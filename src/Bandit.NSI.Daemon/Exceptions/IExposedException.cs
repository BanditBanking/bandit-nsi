using Bandit.NSI.Daemon.Models.DTOs;

namespace Bandit.NSI.Daemon.Exceptions
{
    public interface IExposedException
    {
        ProblemDetailDTO Expose();
    }
}
