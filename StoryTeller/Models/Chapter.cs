using System;

namespace StoryTeller.Models
{
    public class Chapter
    {
        public int ID { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
    }
}