using System.Collections.Generic;

namespace StoryTeller.Models
{
    public class ChapterToVote
    {
        public int ID { get; set; }
        public virtual Chapter Chapter { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

        public int GetLikesCount {
            get
            {
                return Likes.Count; 
            }
        }

    }
}