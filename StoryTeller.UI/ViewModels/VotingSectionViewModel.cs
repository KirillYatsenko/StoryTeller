using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoryTeller.Models.ViewModels
{
    public class VotingSectionViewModel
    {
        public VotingSectionViewModel() { }

        public VotingSectionViewModel(VotingChaptersViewModel votingChaptersVM, TimerViewModel timerVM)
        {
           this.VotingChaptersViewModel = votingChaptersVM;
           this.TimerViewModel = timerVM;
        }

        public bool IsVoting { get; set; }
        public bool IsFull { get; set; }
        public bool? AlreadyAddedChapter { get; set; }
        public int VotedChapter { get; set; }
        public VotingChaptersViewModel VotingChaptersViewModel { get;set;}
        public TimerViewModel TimerViewModel { get; set; }
    }
}