using System;

namespace StoryTeller.Domain.Models
{
    public class Chapter
    {
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Story Story { get; set; }
    }
}