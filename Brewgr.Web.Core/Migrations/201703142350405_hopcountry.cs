namespace Brewgr.Web.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hopcountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hop", "Country", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hop", "Country");
        }
    }
}
