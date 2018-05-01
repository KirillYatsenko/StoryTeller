using StoryTeller.Domain.DAL_Models;
using StoryTeller.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace StoryTeller_Service
{
    public partial class StoryTellerService : ServiceBase
    {
       // private ApplicationDbContext db;

        public StoryTellerService()
        {
            InitializeComponent();

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";

        }

        private int eventId = 1;

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
           // db = new ApplicationDbContext();
            //eventLog1.WriteEntry("In OnStart");

            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            System.Diagnostics.Debugger.Launch();
            // TODO: Insert monitoring activities here.  

            var stories = Repository.GetAllStories.ToList(); /* db.Stories.AsNoTracking().ToList();*/
            eventLog1.WriteEntry($"story count: {stories.Count}", EventLogEntryType.Information, eventId++);

            eventLog1.WriteEntry("db passed", EventLogEntryType.Information, eventId++);

            foreach (var story in stories)
            {
                setStoryVoting(story);
            }

            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        private void setStoryVoting(Story story)
        {
            if (story.NextVotingDate < DateTime.Now &&
               DateTime.Now < story.NextVotingDate.Value.AddMinutes((double)story.TimeForVotings))
            {
                story.EndOfVotingsDate = story.NextVotingDate.Value.AddMinutes((double)story.TimeForVotings);
                story.IsVoting = true;

                eventLog1.WriteEntry($"change story: {story.Title} end of voting time to {story.EndOfVotingsDate.ToString()}", EventLogEntryType.Information, eventId++);
            }
            else if(DateTime.Now > story.NextVotingDate.Value.AddMinutes((double)story.TimeForVotings))
            {
                //if (story.NextVotingDate < DateTime.Now)
                //    story.NextVotingDate = story.NextVotingDate.Value.AddMinutes((double)story.TimeBetweenVotings);

                story.NextVotingDate = story.NextVotingDate.Value.AddMinutes((double)story.TimeBetweenVotings);
                story.IsVoting = false;

                eventLog1.WriteEntry($"change story: {story.Title} next voting time to {story.NextVotingDate.ToString()}", EventLogEntryType.Information, eventId++);
            }

            //db.SaveChanges();
        }


        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
        }
    }
}
