namespace StoryTeller.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhotoProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Photo");
        }
    }
}
