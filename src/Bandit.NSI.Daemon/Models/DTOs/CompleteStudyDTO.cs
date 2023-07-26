using Bandit.NSI.TempNpgsqlRepository.Models;

namespace Bandit.NSI.Daemon.Models.DTOs
{
    public class CompleteStudyDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreationDate { get; set; }
        public Status Status { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
