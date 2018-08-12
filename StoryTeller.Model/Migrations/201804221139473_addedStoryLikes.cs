namespace StoryTeller.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedStoryLikes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoryLikes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Story_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stories", t => t.Story_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Story_ID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoryLikes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StoryLikes", "Story_ID", "dbo.Stories");
            DropIndex("dbo.StoryLikes", new[] { "User_Id" });
            DropIndex("dbo.StoryLikes", new[] { "Story_ID" });
            DropTable("dbo.StoryLikes");
        }
    }
}
