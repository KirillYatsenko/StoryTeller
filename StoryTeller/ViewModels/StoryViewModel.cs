using StoryTeller.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoryTeller.Models
{
    public class StoryViewModel
    {
        public int ID { get; set; }

        [Required]
        [System.Web.Mvc.Remote("IsTitleExists", "Story", HttpMethod = "POST", ErrorMessage = "Title already exists.")]
        public string Title { get; set; }

        [Required]
        [Display(Name="Max number of chapters")]
        public int MaxChaptersNumber { get; set; } = 10;

        [Required]
        [Display(Name = "Time for voting")]
        public int TimeForVotings { get; set; } = 1; //In minutes

        [Required]
        [Display(Name = "Time for writing chapter ")]
        public int TimeBetweenVotings { get; set; } = 1; //In minutes

        [Required]
        [Display(Name = "Max chapter length")]
        public int MaxChapterLength { get; set; } = 100;

        [Required]
        [DataType(DataType.MultilineText)]
        [Length("MaxChapterLength")]
        [Display(Name ="First chapter text")]
        public string FirstChapterText { get; set; } 

        [Display(Name="Picture")]
        public byte[] Picture { get; set; }
    }
}