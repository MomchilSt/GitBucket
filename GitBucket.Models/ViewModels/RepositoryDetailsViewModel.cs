﻿using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models.ViewModels
{
    public class RepositoryDetailsViewModel
    {
        public int Id { get; set; }
        public string Access { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public ICollection<Commit> Commits { get; set; }
        public ICollection<Issue> Issues { get; set; }
        public ICollection<PullRequest> PullRequests { get; set; }
        public User User { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
