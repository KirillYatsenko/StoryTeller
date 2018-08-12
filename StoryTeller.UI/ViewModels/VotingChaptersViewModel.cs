using StoryTeller.Domain.Models;
using System.Collections.Generic;

namespace StoryTeller.Models.ViewModels
{
    public class VotingChaptersViewModel
    {
        public List<ChapterToVote> ChaptersToVote { get; set; }
    }
}