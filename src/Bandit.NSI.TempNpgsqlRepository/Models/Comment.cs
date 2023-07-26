namespace Bandit.NSI.TempNpgsqlRepository.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public DateTime Date { get; set; }
    }
}
