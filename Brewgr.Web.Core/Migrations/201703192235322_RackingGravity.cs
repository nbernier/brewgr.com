namespace Brewgr.Web.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RackingGravity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BrewSession", "RackingGravity", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BrewSession", "RackingGravity");
        }
    }
}
