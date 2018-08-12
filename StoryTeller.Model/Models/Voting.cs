using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace StoryTeller.Domain.Models
{
    public class Voting
    {
        [Key, ForeignKey("Story")]
        public int ID { get; set; }

        public DateTime Started { get; set; }

        public virtual Story Story { get; set; }  
        public virtual ICollection<ChapterToVote> Chapters { get; set; }

        public IEnumerable<ChapterToVote> GetBestChapters {
            get
            {
                var chaptersList = Chapters.ToList();
                int maxLikes = chaptersList.Max(x => x.GetLikesCount);
                return chaptersList.Where(x => x.GetLikesCount == maxLikes);
            }
        }
    }
}