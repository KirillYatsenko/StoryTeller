using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StoryTeller.Domain.Models
{
    public class Story
    {
        public Story()
        {
            Chapters = new List<Chapter>();
            Categories = new List<Category>();
        }

        public int ID { get; set; }

        public string Title { get; set; }
        public DateTime Created { get; set; }

        public Nullable<bool> IsVoting { get; set; }
        public Nullable<DateTime> EndOfVotingsDate { get; set; }
        public Nullable<bool> IsClosed { get; set; }
        public Nullable<int> MaxChaptersNumber { get; set; }
        public Nullable<int> MaxChapterLength { get; set; }
        public Nullable<DateTime> NextVotingDate { get; set; }
        public Nullable<double> TimeForVotings { get; set; } //In minutes
        public Nullable<double> TimeBetweenVotings { get; set; } //In minutes
        public Nullable<int> ViewsCount { get; set; }
        public byte[] Picture { get; set; }

        public virtual ApplicationUser Creator { get; set; }
        public virtual Voting Voting { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<ChapterToVote> ChaptersToVote { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<StoryLikes> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public string ProgressStatusText
        {
            get
            {
                if (this.IsFull)
                {
                    return "Story finished";
                }
                else
                {
                    return "Story writing in progress";
                }
                    
            }
        }

        [NotMapped]
        public bool IsFull
        {
            get
            {
                return this.Chapters.Count == this.MaxChaptersNumber;
            }
        }
    }
}