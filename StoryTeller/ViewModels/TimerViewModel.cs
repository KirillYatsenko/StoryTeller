using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoryTeller.Models.ViewModels
{
    public class TimerViewModel
    {
        public int StoryID { get; set; }
        public int Distance { get; set; } // in miliseconds
        public string TimerText { get; set; }
        public string CssClass { get; set; }
        public DateTime CountDownDate { get; set; }
    }

}