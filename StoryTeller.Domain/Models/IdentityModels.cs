using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StoryTeller.Domain.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string StoryTellerName { get; set; }
        public byte[] Photo { get; set; }
        public string Bio { get; set; }

        public virtual ICollection<Story> Stories { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
 
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // Custom tables
        public DbSet<Story> Stories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<ChapterToVote> ChaptersToVote { get; set; }
        public DbSet<Voting> Votings{ get; set; }
        public DbSet<Like> Likes{ get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<StoryLikes> StoryLikes { get; set; }
    }
}