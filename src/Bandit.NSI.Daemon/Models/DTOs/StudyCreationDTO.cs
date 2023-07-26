namespace Bandit.NSI.Daemon.Models.DTOs
{
    public class StudyCreationDTO
    {
        public Guid StudyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }
    }
}
