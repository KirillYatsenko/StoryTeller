namespace StoryTeller.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedStory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stories", "MaxChapterLength", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stories", "MaxChapterLength");
        }
    }
}
