using System;

namespace StoryTeller.Models
{
    public class Like
    {
        public int ID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime Created { get; set; }
    }
}