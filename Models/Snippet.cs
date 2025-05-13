using System;

namespace SnipIt.Models
{
    public class Snippet
    {
        // snippet class model
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsPinned { get; set; } 
        public string CodeType { get; set; }


        public Snippet()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            LastModified = DateTime.Now;
            IsPinned = false; // Default to not pinned
        }
    }
}