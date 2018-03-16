namespace StoryTeller.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chapters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Text = c.String(),
                        User_Id = c.String(maxLength: 128),
                        Story_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Stories", t => t.Story_ID)
                .Index(t => t.User_Id)
                .Index(t => t.Story_ID);
            
            CreateTable(
                "dbo.ChapterToVotes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Chapter_ID = c.Int(),
                        Voting_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Chapters", t => t.Chapter_ID)
                .ForeignKey("dbo.Votings", t => t.Voting_ID)
                .Index(t => t.Chapter_ID)
                .Index(t => t.Voting_ID);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        ChapterToVote_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.ChapterToVotes", t => t.ChapterToVote_ID)
                .Index(t => t.User_Id)
                .Index(t => t.ChapterToVote_ID);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsVoting = c.Boolean(),
                        IsClosed = c.Boolean(),
                        MaxChaptersNumber = c.Int(),
                        NextVotingDatetime = c.DateTime(nullable: false),
                        TimeForVotings = c.Int(nullable: false),
                        TimeBetweenVotings = c.Int(nullable: false),
                        Creator_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.Votings",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Started = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stories", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votings", "ID", "dbo.Stories");
            DropForeignKey("dbo.ChapterToVotes", "Voting_ID", "dbo.Votings");
            DropForeignKey("dbo.Stories", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Chapters", "Story_ID", "dbo.Stories");
            DropForeignKey("dbo.Likes", "ChapterToVote_ID", "dbo.ChapterToVotes");
            DropForeignKey("dbo.Likes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChapterToVotes", "Chapter_ID", "dbo.Chapters");
            DropForeignKey("dbo.Chapters", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Votings", new[] { "ID" });
            DropIndex("dbo.Stories", new[] { "Creator_Id" });
            DropIndex("dbo.Likes", new[] { "ChapterToVote_ID" });
            DropIndex("dbo.Likes", new[] { "User_Id" });
            DropIndex("dbo.ChapterToVotes", new[] { "Voting_ID" });
            DropIndex("dbo.ChapterToVotes", new[] { "Chapter_ID" });
            DropIndex("dbo.Chapters", new[] { "Story_ID" });
            DropIndex("dbo.Chapters", new[] { "User_Id" });
            DropTable("dbo.Votings");
            DropTable("dbo.Stories");
            DropTable("dbo.Likes");
            DropTable("dbo.ChapterToVotes");
            DropTable("dbo.Chapters");
        }
    }
}
