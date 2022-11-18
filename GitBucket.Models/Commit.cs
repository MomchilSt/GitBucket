﻿using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models
{
    public class Commit
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public string ContentBeforeCommit { get; set; }

        [Required]
        public int RepositoryId { get; set; }
        public Repository Repository { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
