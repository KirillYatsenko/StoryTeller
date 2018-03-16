using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoryTeller.Models
{
    public class Story
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }
        public DateTime Created { get; set; }

        public Nullable<bool> IsVoting { get; set; }
        public Nullable<bool> IsClosed { get; set; }
        public Nullable<int> MaxChaptersNumber { get; set; }

        [Required]
        public Nullable<DateTime> NextVotingDatetime { get; set; }

        [Required]
        public Nullable<int> TimeForVotings { get; set; } //In minutes

        [Required]
        public Nullable<int> TimeBetweenVotings { get; set; } //In minutes

        public virtual ApplicationUser Creator { get; set; }
        public virtual Voting Voting { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}