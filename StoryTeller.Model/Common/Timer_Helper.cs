using StoryTeller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTeller.Domain.Common
{
    public static class Timer_Helper
    {
        public static void ResetStoryVoting(Story story)
        {
            clearChaptersToVote(story);
            story.IsVoting = false;
            story.NextVotingDate = DateTime.Now.AddMinutes((double)story.TimeBetweenVotings);
        }

        public static void SetStoryVotingState(Story story)
        {
            if (checkStoryIsVoting(story))
            {
                if (checkChaptersToVoteExists(story))
                {
                    if (checkExistsOneChapter(story))
                    {
                        procedeVotingExpired(story);
                    }
                    else
                    {
                        story.EndOfVotingsDate = story.NextVotingDate.Value.AddMinutes((double)story.TimeForVotings);
                        story.IsVoting = true;
                    }
                }
                else
                {
                    resetNextVotingDate(story);
                }
            }
            else if (checkStoryVotingExpired(story))
            {
                procedeVotingExpired(story);
            }
        }

        private static void procedeVotingExpired(Story story)
        {
            if (story.EndOfVotingsDate != null)
            {
                story.NextVotingDate = story.EndOfVotingsDate.Value.AddMinutes((double)story.TimeBetweenVotings);
            }
            else
            {
                story.NextVotingDate = story.NextVotingDate.Value.AddMinutes((double)story.TimeBetweenVotings);
            }

            if (checkChaptersToVoteExists(story))
            {
                finishVoting(story);
            }
        }

        private static void resetNextVotingDate(Story story)
        {
            story.NextVotingDate = story.NextVotingDate.Value.AddMinutes((double)story.TimeBetweenVotings);
            story.IsVoting = false;
        }

        private static bool checkChaptersToVoteExists(Story story)
        {
            return story.ChaptersToVote.Count > 0;
        }

        private static bool checkExistsOneChapter(Story story)
        {
            return story.ChaptersToVote.Count == 1;
        }

        private static bool checkStoryIsVoting(Story story)
        {
            return story.NextVotingDate < DateTime.Now &&
                 DateTime.Now < story.NextVotingDate.Value.AddMinutes((double)story.TimeForVotings);
        }

        private static bool checkStoryVotingExpired(Story story)
        {
            return DateTime.Now > story.NextVotingDate.Value.AddMinutes((double)story.TimeForVotings);
        }

        // ToDo: leave most voted chapter
        private static void finishVoting(Story story)
        {
            var maxLikes = story.ChaptersToVote.Max(y => y.GetLikesCount);
            var mostVotedChapters = story.ChaptersToVote.Where(x => x.GetLikesCount == maxLikes).ToList();
            clearChaptersToVote(story);

            if (checkToRevote(mostVotedChapters,story))
            {
                addVotingChapters(mostVotedChapters, story);
                revoting(story);
            }
            else
            {
                story.Chapters.Add(mostVotedChapters[0].Chapter);
                story.IsVoting = false;
            }
        }

        private static void clearChaptersToVote(Story story)
        {
            story.ChaptersToVote.Clear();
        }

        private static void addVotingChapters(IEnumerable<ChapterToVote> mostVotedChapters, Story story)
        {
            foreach (var chapter in mostVotedChapters)
            {
                story.ChaptersToVote.Add(chapter);
            }
        }

        private static bool checkToRevote(IEnumerable<ChapterToVote> mostVotedChapters, Story story)
        {
            return mostVotedChapters.Count() > 1 && mostVotedChapters.Count() != story.ChaptersToVote.Count();
        }

        private static void revoting(Story story)
        {
            story.EndOfVotingsDate = story.EndOfVotingsDate.Value.AddMinutes((double)story.TimeForVotings);
            story.IsVoting = true;
        }


    }
}
