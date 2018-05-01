using StoryTeller.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoryTeller.ViewModels
{
    public class ChapterCreateViewModel
    {
        public int StoryId { get; set; }
        public int MaxChapterLength { get; set; }

        [Required]
        [Length("MaxChapterLength")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}