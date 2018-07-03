using StoryTeller.Domain.Common;
using StoryTeller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTeller.Timer
{
    public static class StoryTellerTimer
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static async void Start()
        {
            var stories = db.Stories.ToList().Where(x => !x.IsFull).ToList(); /* db.Stories.AsNoTracking().ToList();*/

            foreach (var story in stories)
            {
                Timer_Helper.ResetStoryVoting(story);
                await db.SaveChangesAsync();
            }

            while (true)
            {

                foreach (var story in stories)
                {
                    db.Entry(story).Reload();
                    db.Entry(story).Collection(x => x.ChaptersToVote).Load();

                    Timer_Helper.SetStoryVotingState(story);
                    await db.SaveChangesAsync();
                }
            }
        }

    }

    class Program
    {

        static void Main(string[] args)
        {
          
        }

        //static void setStoryVoting(Story story)
        //{
        //    if (story.NextVotingDate < DateTime.Now &&
        //       DateTime.Now < story.NextVotingDate.Value.AddMinutes((double)story.TimeForVotings))
        //    {
        //        story.EndOfVotingsDate = story.NextVotingDate.Value.AddMinutes((double)story.TimeForVotings);
        //        story.IsVoting = true;
        //    }
        //    else if (DateTime.Now > story.NextVotingDate.Value.AddMinutes((double)story.TimeForVotings))
        //    {
        //        //if (story.NextVotingDate < DateTime.Now)
        //        //    story.NextVotingDate = story.NextVotingDate.Value.AddMinutes((double)story.TimeBetweenVotings);

        //        if (story.EndOfVotingsDate != null)
        //            story.NextVotingDate = story.EndOfVotingsDate.Value.AddMinutes((double)story.TimeBetweenVotings);
        //        else
        //            story.NextVotingDate = story.NextVotingDate.Value.AddMinutes((double)story.TimeBetweenVotings);

        //        story.IsVoting = false;

        //    }
        //}


    }
}
