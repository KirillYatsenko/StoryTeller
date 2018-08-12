namespace StoryTeller.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChapterToVotes", "Story_ID", c => c.Int());
            CreateIndex("dbo.ChapterToVotes", "Story_ID");
            AddForeignKey("dbo.ChapterToVotes", "Story_ID", "dbo.Stories", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChapterToVotes", "Story_ID", "dbo.Stories");
            DropIndex("dbo.ChapterToVotes", new[] { "Story_ID" });
            DropColumn("dbo.ChapterToVotes", "Story_ID");
        }
    }
}
