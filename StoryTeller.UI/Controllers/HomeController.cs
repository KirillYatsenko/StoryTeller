using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StoryTeller.Domain.Models;
using StoryTeller.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StoryTeller.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;

        public HomeController()
        {
            Trace.WriteLine("Homecontroller()");

            db = new ApplicationDbContext();
            Trace.WriteLine("db created in Homeconroller()");
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Stories
        public async Task<ActionResult> Index(string searchText)
        {
            Trace.WriteLine("home index begin...");

            List<Story> stories;

            if(searchText != null)
            {
                stories = await db.Stories.Where(x => x.Title.Contains(searchText) || x.Chapters.FirstOrDefault().Text.Contains(searchText)).ToListAsync();
            }
            else
            {
                stories = await db.Stories.ToListAsync();
            }


            Trace.WriteLine("home index end");

            return View(stories.OrderByDescending(x => x.Created));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}