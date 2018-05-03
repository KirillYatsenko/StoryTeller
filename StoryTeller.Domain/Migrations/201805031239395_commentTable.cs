namespace StoryTeller.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentTable : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Story_ID", "dbo.Stories");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "Story_ID" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropTable("dbo.Comments");
        }
    }
}
