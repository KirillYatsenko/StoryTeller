namespace StoryTeller.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcategoriestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Story_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stories", t => t.Story_ID)
                .Index(t => t.Story_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "Story_ID", "dbo.Stories");
            DropIndex("dbo.Categories", new[] { "Story_ID" });
            DropTable("dbo.Categories");
        }
    }
}
