using Microsoft.AspNet.SignalR;
using StoryTeller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoryTeller.Common.Hubs
{
    public class ViewsHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public  void Read(string storyID)
        {
            var story =  db.Stories.Find(int.Parse(storyID));
            story.ViewsCount++;
            db.SaveChanges();

            Clients.All.updateViewsCount(story.ViewsCount, storyID);
        }
    }
}