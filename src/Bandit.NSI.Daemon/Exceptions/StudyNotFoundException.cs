using Bandit.NSI.Daemon.Models.DTOs;

namespace Bandit.NSI.Daemon.Exceptions
{
    [Serializable]
    public class StudyNotFoundException : Exception, IExposedException
    {
        private Guid _id;

        public StudyNotFoundException() { }

        public StudyNotFoundException(Guid id) : base($"A study with id {id} could not be found") 
        {
            _id = id;
        }

        public ProblemDetailDTO Expose() => new()
        {
            HttpCode = StatusCodes.Status404NotFound,
            ErrorCode = "miracle",
            Title = "Study not found",
            Detail = $"A study with id {_id} could not be found"
        };
    }
}
