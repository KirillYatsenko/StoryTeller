namespace StoryTeller.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StoryTellerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "StoryTellerName");
        }
    }
}
