using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StoryTeller.Common;
using StoryTeller.Domain.Common;
using StoryTeller.Domain.Models;
using StoryTeller.Models;
using StoryTeller.Models.ViewModels;
using StoryTeller.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StoryTeller.Controllers
{
    [RequireHttps]
    public class StoryController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;

        public StoryController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        [HttpPost]
        public JsonResult IsTitleExists(string Title)
        {
            return Json(!db.Stories.Any(x => x.Title == Title));
        }

        // GET: Stories/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var story = await db.Stories.FindAsync(id);

            if (story == null)
            {
                return HttpNotFound();
            }

            story.ViewsCount++;
            await db.SaveChangesAsync();

            //    Timer_Helper.SetStoryVoting(story);

            return View(story);
        }

        // GET: Stories/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Votings, "ID", "ID");
            return View();
        }

        [HttpGet]
        public ActionResult CreateNewChapter(int storyId)
        {
            var story = db.Stories.Find(storyId);
            var userChapter = story.ChaptersToVote.FirstOrDefault(x => x.Chapter.User == this.CurrentUser);

            if (userChapter != null)
            {
                return PartialView("~/Views/Story/Partials/_ChapterCreate.cshtml", new ChapterCreateViewModel() { StoryId = storyId, Text = userChapter.Chapter.Text, MaxChapterLength = (int)story.MaxChapterLength });
            }

            return PartialView("~/Views/Story/Partials/_ChapterCreate.cshtml", new ChapterCreateViewModel() { StoryId = storyId, MaxChapterLength = (int)story.MaxChapterLength });
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewChapter(ChapterCreateViewModel chapter)
        {
            if (ModelState.IsValid)
            {
                var story = await db.Stories.FindAsync(chapter.StoryId);
                var userChapter = story.ChaptersToVote.FirstOrDefault(x => x.Chapter.User == this.CurrentUser);

                if (userChapter != null)
                {
                    userChapter.Chapter.Text = chapter.Text;
                }
                else
                {
                    var chapterToVote = new ChapterToVote() { Chapter = new Chapter() { Text = chapter.Text, Created = DateTime.Now, User = CurrentUser } };
                    story.ChaptersToVote.Add(chapterToVote);
                }

                await db.SaveChangesAsync();
                return PartialView("~/Views/Story/Partials/_Thanks.cshtml");
            }

            return PartialView("~/Views/Story/Partials/_ChapterCreate.cshtml", chapter);
        }

        public async Task<ActionResult> GetVotingPartial(int storyID)
        {
            var story = await db.Stories.FindAsync(storyID);

            if (story != null)
            {
                VotingSectionViewModel votingSectionVM;
                if (!story.IsFull)
                {
                    var timerVM = new TimerViewModel();
                    var votingChaptersVM = new VotingChaptersViewModel();
                    votingSectionVM = new VotingSectionViewModel(votingChaptersVM, timerVM);

                    votingChaptersVM.ChaptersToVote = setChaptersToVoteViewModel(story.ChaptersToVote.ToList());

                    if (story.IsVoting == true)
                    {
                        setStoryVoting(votingSectionVM, timerVM, story);
                    }
                    else
                    {
                        setStoryNotVoting(votingSectionVM, timerVM, story);
                    }

                    setUserAddedChapter(votingSectionVM, storyID);
                }
                else
                {
                    votingSectionVM = new VotingSectionViewModel();
                }

                votingSectionVM.IsFull = story.IsFull;

                return PartialView("~/Views/Story/Partials/_StoryVotingSection.cshtml", votingSectionVM);
            }

            return HttpNotFound();
        }

        private void setUserAddedChapter(VotingSectionViewModel votingSectionVM, int storyID)
        {
            var chapter = getUserChapterToVoteAsync(storyID).Result;
            if (chapter != null)
            {
                votingSectionVM.AlreadyAddedChapter = true;
            }
            else
            {
                votingSectionVM.AlreadyAddedChapter = false;
            }
        }

        private void setStoryNotVoting(VotingSectionViewModel votingSectionVM, TimerViewModel timerVM, Story story)
        {
            votingSectionVM.IsVoting = false;

            timerVM.CssClass = "NotVoting";
            timerVM.TimerText = "Time before voting";
            timerVM.Distance = (int)(story.NextVotingDate - DateTime.Now).Value.TotalMilliseconds;
            timerVM.CountDownDate = (DateTime)story.NextVotingDate;
        }

        private void setStoryVoting(VotingSectionViewModel votingSectionVM, TimerViewModel timerVM, Story story)
        {
            votingSectionVM.IsVoting = true;
            timerVM.CssClass = "text-danger";
            timerVM.TimerText = "Time before voting end";
            timerVM.Distance = (int)(story.EndOfVotingsDate - DateTime.Now).Value.TotalMilliseconds;
            timerVM.CountDownDate = (DateTime)story.EndOfVotingsDate;
        }

        [HttpGet]
        public async Task<ActionResult> VoteChapter(int storyID, int chapterID)
        {
            var story = await db.Stories.FindAsync(storyID);
            var chapter = story.ChaptersToVote.ToList().Find(x => x.ID == chapterID);
            List<Like> allLikes = new List<Like>();

            foreach (var chapterToVote in story.ChaptersToVote)
                allLikes.AddRange(chapterToVote.Likes);

            var userLike = allLikes.FirstOrDefault(x => x.User == CurrentUser);

            if (userLike != null)
            {
                var chapterToVote = userLike.ChapterToVote;
                db.Likes.Remove(userLike);
            }

            chapter.Likes.Add(new Like() { Created = DateTime.Now, User = CurrentUser });
            await db.SaveChangesAsync();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        private List<ChapterToVote> setChaptersToVoteViewModel(List<ChapterToVote> chaptersToVote)
        {
            foreach (var chapter in chaptersToVote)
            {
                if (chapter.Likes.ToList().Exists(x => x.User == CurrentUser))
                    chapter.voteBtnStyle = "btnVoted";
                else
                    chapter.voteBtnStyle = "btnNotVoted";
            }

            return chaptersToVote;
        }

        private async Task<ChapterToVote> getUserChapterToVoteAsync(int storyID)
        {
            var story = await db.Stories.FindAsync(storyID);
            var chapter = story.ChaptersToVote.Where(x => x.Chapter.User == CurrentUser).FirstOrDefault();

            return chapter;
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude = "Picture")]StoryViewModel storyVM)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = FileUploader.GetFile("Photo", Request);

                var newStory = new Story()
                {
                    Title = storyVM.Title,
                    Created = DateTime.Now,
                    IsClosed = false,
                    IsVoting = false,
                    MaxChaptersNumber = storyVM.MaxChaptersNumber,
                    MaxChapterLength = storyVM.MaxChapterLength,
                    NextVotingDate = DateTime.Now.AddMinutes((double)storyVM.TimeBetweenVotings),
                    TimeBetweenVotings = storyVM.TimeBetweenVotings,
                    TimeForVotings = storyVM.TimeForVotings,
                    Creator = CurrentUser,
                    ViewsCount = 0,
                    Picture = imageData

                };

                var firstChapter = new Chapter() { User = CurrentUser, Created = DateTime.Now, Text = storyVM.FirstChapterText };

                newStory.Chapters.Add(firstChapter);

                db.Chapters.Add(firstChapter);
                db.Stories.Add(newStory);

                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(storyVM);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int storyID)
        {
            var story = await db.Stories.FindAsync(storyID);

            var storyViewModel = new StoryViewModel()
            {
                ID = storyID,
                TimeBetweenVotings = (int)story.TimeBetweenVotings,
                TimeForVotings = (int)story.TimeForVotings,
                Title = story.Title,
                MaxChaptersNumber = (int)story.MaxChaptersNumber,
                FirstChapterText = "passEditRequiredValidation"
            };

            return PartialView("~/Views/Home/Partials/_StoryEdit.cshtml", storyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(/*[Bind(Exclude = "Picture")] */StoryViewModel storyViewModel)
        {
            if (ModelState.IsValid)
            {
                var story = await db.Stories.FindAsync(storyViewModel.ID);

                story.Title = storyViewModel.Title;
                story.TimeForVotings = storyViewModel.TimeForVotings;
                story.TimeBetweenVotings = storyViewModel.TimeBetweenVotings;
                story.MaxChapterLength = storyViewModel.MaxChapterLength;
                story.MaxChaptersNumber = storyViewModel.MaxChaptersNumber;

                //byte[] imageData = FileUploader.GetFile("Picture", Request);
                //if(imageData != null && imageData.Count() > 0)
                //{
                //    story.Picture = imageData;
                //}

                db.Entry(story).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return PartialView("~/Views/Home/Partials/_Story.cshtml",story);
            }

            return View();
        }

        // GET: Stories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = await db.Stories.FindAsync(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int storyID)
        {
            //Story story = await db.Stories.FindAsync(storyID);

            //story.ChaptersToVote.Clear();
            //story.Chapters.Clear();
            //db.Stories.Remove(story);
            //await db.SaveChangesAsync();

            return Json(new { success = true });
        }

       

        [HttpPost]
        public async Task<ActionResult> Like(int storyID)
        {
            var operation = "liked";

            Story story = await db.Stories.FindAsync(storyID);

            var like = story.Likes.ToList().FirstOrDefault(x => x.User.Id == CurrentUser.Id);
            if (like != null)
            {
                operation = "unliked";
                story.Likes.Remove(like);
            }
            else
            {
                operation = "liked";
                story.Likes.Add(new StoryLikes() { Story = story, User = CurrentUser });
            }

            await db.SaveChangesAsync();

            return Json(new { success = true, operation = operation });
        }

        public async Task<FileContentResult> StoryPhoto(int storyID)
        {
            var story = await db.Stories.FindAsync(storyID);

            if (story.Picture.Count() > 0)
            {
                return new FileContentResult(story.Picture, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Content/Images/story.jpg");
                var imageData = FileBinaryConvertor.GetFile(fileName);

                return File(imageData, "image/png");
            }
        }

        private ApplicationUser CurrentUser
        {
            get
            {
                return manager.FindById(User.Identity.GetUserId());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
