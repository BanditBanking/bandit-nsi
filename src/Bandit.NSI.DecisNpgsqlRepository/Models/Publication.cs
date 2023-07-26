namespace Bandit.NSI.DecisNpgsqlRepository.Models
{
    public class Publication
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
