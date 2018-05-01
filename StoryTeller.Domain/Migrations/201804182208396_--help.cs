namespace StoryTeller.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class help : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stories", "ViewsCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stories", "ViewsCount");
        }
    }
}
