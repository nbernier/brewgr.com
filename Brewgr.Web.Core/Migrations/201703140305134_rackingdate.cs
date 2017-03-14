namespace Brewgr.Web.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rackingdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BrewSession", "RackingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BrewSession", "RackingDate");
        }
    }
}
