namespace StoryTeller.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStoryPicture2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stories", "Picture", c => c.Binary());
            DropColumn("dbo.Stories", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stories", "Photo", c => c.Binary());
            DropColumn("dbo.Stories", "Picture");
        }
    }
}
