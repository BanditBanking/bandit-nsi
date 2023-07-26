﻿namespace Bandit.NSI.Daemon.Models.DTOs
{
    public class StudyResumeDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public string AuthorName { get; set; }
        public DateTime Date { get; set; }
    }
}
