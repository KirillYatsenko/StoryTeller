namespace StoryTeller.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
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
            
            AddColumn("dbo.Stories", "MaxChapterLength", c => c.Int());
            AddColumn("dbo.Stories", "ViewsCount", c => c.Int());
            AddColumn("dbo.Stories", "Picture", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "Photo", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "Bio", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoryLikes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StoryLikes", "Story_ID", "dbo.Stories");
            DropForeignKey("dbo.Comments", "Story_ID", "dbo.Stories");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.StoryLikes", new[] { "User_Id" });
            DropIndex("dbo.StoryLikes", new[] { "Story_ID" });
            DropIndex("dbo.Comments", new[] { "Story_ID" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropColumn("dbo.AspNetUsers", "Bio");
            DropColumn("dbo.AspNetUsers", "Photo");
            DropColumn("dbo.Stories", "Picture");
            DropColumn("dbo.Stories", "ViewsCount");
            DropColumn("dbo.Stories", "MaxChapterLength");
            DropTable("dbo.StoryLikes");
            DropTable("dbo.Comments");
        }
    }
}
