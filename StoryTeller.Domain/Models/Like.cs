using System;

namespace StoryTeller.Domain.Models
{
    public class Like
    {
        public int ID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime Created { get; set; }

        public virtual ChapterToVote ChapterToVote { get; set; }
    }
}