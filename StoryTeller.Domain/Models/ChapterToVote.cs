using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoryTeller.Domain.Models
{
    public class ChapterToVote
    {
        public int ID { get; set; }
        public virtual Chapter Chapter { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual Story Story { get; set; }

        public int GetLikesCount
        {
            get
            {
                return Likes.Count;
            }
        }

        // View Model
        [NotMapped]
        public string voteBtnStyle { get; set; } // Options: (1)btnNotVoted, (2)btnVoted
        [NotMapped]
        public string voteBtnDisalbedAttr
        {
            get
            {
                if (this.voteBtnStyle == "btnNotVoted")
                    return string.Empty;
                else
                    return "disabled";
            }
        }
    }
}