namespace StoryTeller.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStoryPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stories", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stories", "Photo");
        }
    }
}
