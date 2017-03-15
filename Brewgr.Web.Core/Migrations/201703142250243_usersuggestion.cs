namespace Brewgr.Web.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersuggestion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSuggestion",
                c => new
                    {
                        UserSuggestionId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SuggestionText = c.String(maxLength: 500),
                        UserHostAddress = c.String(maxLength: 50),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserSuggestionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserSuggestion");
        }
    }
}
