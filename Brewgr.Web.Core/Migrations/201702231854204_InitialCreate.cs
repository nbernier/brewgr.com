namespace Brewgr.Web.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adjunct",
                c => new
                    {
                        AdjunctId = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DatePromoted = c.DateTime(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.AdjunctId)
                .ForeignKey("dbo.User", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength:50),
                        EmailAddress = c.String(maxLength:255),
                        Password = c.Binary(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Bio = c.String(),
                        HasCustomUsername = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CalculatedUsername = c.String(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(i => i.Username, unique: true)
                .Index(i=>i.EmailAddress,unique:true);
            
            CreateTable(
                "dbo.Badge",
                c => new
                    {
                        BadgeId = c.Int(nullable: false, identity: true),
                        BadgeTypeId = c.Int(nullable: false),
                        BadgeName = c.String(),
                        BadgeDescription = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BadgeId);
            
            CreateTable(
                "dbo.BrewSession",
                c => new
                    {
                        BrewSessionId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        UnitTypeId = c.Int(nullable: false),
                        BrewDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                        GrainWeight = c.Double(),
                        GrainTemp = c.Double(),
                        BoilTime = c.Double(),
                        BoilVolumeEst = c.Double(),
                        FermentVolumeEst = c.Double(),
                        TargetMashTemp = c.Double(),
                        MashThickness = c.Double(),
                        TotalWaterNeeded = c.Double(),
                        StrikeWaterTemp = c.Double(),
                        StrikeWaterVolume = c.Double(),
                        FirstRunningsVolume = c.Double(),
                        SpargeWaterVolume = c.Double(),
                        BrewKettleLoss = c.Double(),
                        WortShrinkage = c.Double(),
                        MashTunLoss = c.Double(),
                        BoilLoss = c.Double(),
                        MashGrainAbsorption = c.Double(),
                        SpargeGrainAbsorption = c.Double(),
                        MashPH = c.Double(),
                        MashStartTemp = c.Double(),
                        MashEndTemp = c.Double(),
                        MashTime = c.Int(),
                        BoilVolumeActual = c.Double(),
                        PreBoilGravity = c.Double(),
                        BoilTimeActual = c.Int(),
                        PostBoilVolume = c.Double(),
                        FermentVolumeActual = c.Double(),
                        OriginalGravity = c.Double(),
                        FinalGravity = c.Double(),
                        ConditionDate = c.DateTime(),
                        ConditionTypeId = c.Int(),
                        PrimingSugarType = c.String(),
                        PrimingSugarAmount = c.Double(),
                        KegPSI = c.Int(),
                        IsPublic = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.BrewSessionId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Recipe", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RecipeComment",
                c => new
                    {
                        RecipeCommentId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RecipeId = c.Int(nullable: false),
                        CommentText = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeCommentId)
                .ForeignKey("dbo.Recipe", t => t.RecipeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.Recipe",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        RecipeTypeId = c.Int(nullable: false),
                        OriginalRecipeId = c.Int(),
                        RecipeName = c.String(),
                        ImageUrlRoot = c.String(),
                        Description = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        BjcpStyleSubCategoryId = c.String(maxLength: 128),
                        BatchSize = c.Double(nullable: false),
                        BoilSize = c.Double(nullable: false),
                        BoilTime = c.Int(nullable: false),
                        Efficiency = c.Double(nullable: false),
                        Og = c.Double(nullable: false),
                        Fg = c.Double(nullable: false),
                        Srm = c.Double(nullable: false),
                        Ibu = c.Double(nullable: false),
                        BgGu = c.Double(nullable: false),
                        Abv = c.Double(nullable: false),
                        Calories = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        UnitTypeId = c.Int(nullable: false),
                        IbuFormulaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.BjcpStyle", t => t.BjcpStyleSubCategoryId)
                .ForeignKey("dbo.User", t => t.CreatedBy, cascadeDelete: false)
                .Index(t => t.CreatedBy)
                .Index(t => t.BjcpStyleSubCategoryId);
            
            CreateTable(
                "dbo.RecipeAdjunct",
                c => new
                    {
                        RecipeAdjunctId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        AdjunctUsageTypeId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Unit = c.String(),
                    })
                .PrimaryKey(t => t.RecipeAdjunctId)
                .ForeignKey("dbo.Adjunct", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Recipe", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.BjcpStyle",
                c => new
                    {
                        SubCategoryId = c.String(nullable: false, maxLength: 128),
                        Class = c.String(),
                        CategoryId = c.Int(nullable: false),
                        CategoryName = c.String(),
                        SubCategoryName = c.String(),
                        Aroma = c.String(),
                        Appearance = c.String(),
                        Flavor = c.String(),
                        Mouthfeel = c.String(),
                        Impression = c.String(),
                        Comments = c.String(),
                        Ingredients = c.String(),
                        Og_Low = c.Double(),
                        Og_High = c.Double(),
                        Fg_Low = c.Double(),
                        Fg_High = c.Double(),
                        Ibu_Low = c.Int(),
                        Ibu_High = c.Int(),
                        Srm_Low = c.Double(),
                        Srm_High = c.Double(),
                        Abv_Low = c.Double(),
                        Abv_High = c.Double(),
                        Examples = c.String(),
                    })
                .PrimaryKey(t => t.SubCategoryId);
            
            CreateTable(
                "dbo.BjcpStyleUrlFriendlyName",
                c => new
                    {
                        SubCategoryId = c.String(nullable: false, maxLength: 128),
                        UrlFriendlyName = c.String(),
                    })
                .PrimaryKey(t => t.SubCategoryId)
                .Index(t => t.SubCategoryId);
            
            CreateTable(
                "dbo.RecipeFermentable",
                c => new
                    {
                        RecipeFermentableId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Ppg = c.Int(nullable: false),
                        Lovibond = c.Int(nullable: false),
                        FermentableUsageTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeFermentableId)
                .ForeignKey("dbo.Fermentable", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Recipe", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Fermentable",
                c => new
                    {
                        FermentableId = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        Ppg = c.Int(nullable: false),
                        Lovibond = c.Int(nullable: false),
                        DefaultUsageTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DatePromoted = c.DateTime(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.FermentableId)
                .ForeignKey("dbo.User", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.RecipeHop",
                c => new
                    {
                        RecipeHopId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        HopUsageTypeId = c.Int(nullable: false),
                        HopTypeId = c.Int(nullable: false),
                        AlphaAcidAmount = c.Double(nullable: false),
                        Amount = c.Double(nullable: false),
                        TimeInMinutes = c.Int(nullable: false),
                        Ibu = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeHopId)
                .ForeignKey("dbo.Hop", t => t.IngredientId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Hop",
                c => new
                    {
                        HopId = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        AA = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DatePromoted = c.DateTime(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.HopId)
                .ForeignKey("dbo.User", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.RecipeMashStep",
                c => new
                    {
                        RecipeMashStepId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        Heat = c.String(),
                        Temp = c.Double(nullable: false),
                        Time = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeMashStepId)
                .ForeignKey("dbo.MashStep", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Recipe", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.MashStep",
                c => new
                    {
                        MashStepId = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DatePromoted = c.DateTime(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.MashStepId)
                .ForeignKey("dbo.User", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.RecipeStep",
                c => new
                    {
                        RecipeStepId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        Description = c.String(),
                        Rank = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.RecipeStepId)
                .ForeignKey("dbo.Recipe", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.RecipeYeast",
                c => new
                    {
                        RecipeYeastId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        Attenuation = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeYeastId)
                .ForeignKey("dbo.Recipe", t => t.RecipeId, cascadeDelete: true)
                .ForeignKey("dbo.Yeast", t => t.IngredientId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Yeast",
                c => new
                    {
                        YeastId = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        Attenuation = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DatePromoted = c.DateTime(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.YeastId)
                .ForeignKey("dbo.User", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.TastingNote",
                c => new
                    {
                        TastingNoteId = c.Int(nullable: false, identity: true),
                        BrewSessionId = c.Int(),
                        RecipeId = c.Int(),
                        UserId = c.Int(nullable: false),
                        TasteDate = c.DateTime(nullable: false),
                        Rating = c.Double(nullable: false),
                        Notes = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.TastingNoteId)
                .ForeignKey("dbo.BrewSession", t => t.BrewSessionId)
                .ForeignKey("dbo.Recipe", t => t.RecipeId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BrewSessionId)
                .Index(t => t.RecipeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserAdmin",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserConnection",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FollowedById = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.UserId, t.FollowedById })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        LoginDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginDate })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserNotificationType",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        NotificationTypeId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.NotificationTypeId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserOAuthUserId",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        OAuthProviderId = c.Int(nullable: false),
                        OAuthUserId = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.OAuthProviderId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPartnerAdmin",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        PartnerId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.UserId, t.PartnerId })
                .ForeignKey("dbo.Partner", t => t.PartnerId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PartnerId);
            
            CreateTable(
                "dbo.Partner",
                c => new
                    {
                        PartnerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Token = c.String(),
                        ContactName = c.String(),
                        ContactAddress1 = c.String(),
                        ContactAddress2 = c.String(),
                        ContactCity = c.String(),
                        ContactStateProvince = c.String(),
                        ContactPostalCode = c.String(),
                        ContactCountry = c.String(),
                        ContactPhone = c.String(),
                        ContactFax = c.String(),
                        ContactEmail = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.PartnerId);
            
            CreateTable(
                "dbo.PartnerService",
                c => new
                    {
                        PartnerId = c.Int(nullable: false),
                        PartnerServiceTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.PartnerId, t.PartnerServiceTypeId })
                .ForeignKey("dbo.Partner", t => t.PartnerId, cascadeDelete: true)
                .Index(t => t.PartnerId);
            
           
            CreateTable(
                "dbo.PartnerSendToShopIngredient",
                c => new
                    {
                        PartnerId = c.Int(nullable: false),
                        IngredientTypeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PartnerId, t.IngredientTypeId, t.IngredientId })
                .ForeignKey("dbo.Partner", t => t.PartnerId, cascadeDelete: true)
                .Index(t => t.PartnerId);
            
            CreateTable(
                "dbo.PartnerSendToShopSettings",
                c => new
                    {
                        PartnerId = c.Int(nullable: false),
                        SendToShopFormatTypeId = c.Int(nullable: false),
                        SendToShopMethodTypeId = c.Int(nullable: false),
                        DayStart = c.Int(nullable: false),
                        DayEnd = c.Int(nullable: false),
                        HourStart = c.Int(nullable: false),
                        HourEnd = c.Int(nullable: false),
                        AllowOutOfRangeOrders = c.Boolean(nullable: false),
                        DeliveryEmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.PartnerId)
                .ForeignKey("dbo.Partner", t => t.PartnerId)
                .Index(t => t.PartnerId);
            
            CreateTable(
                "dbo.UserReputationAward",
                c => new
                    {
                        UserReputationAwardId = c.Long(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ReputationAwardTypeId = c.Int(nullable: false),
                        ReputationObjectTypeId = c.Int(nullable: false),
                        ReputationObjectId = c.Int(nullable: false),
                        DateAwarded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserReputationAwardId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            
            CreateTable(
                "dbo.AffiliateProduct",
                c => new
                    {
                        AffiliateProductId = c.Int(nullable: false, identity: true),
                        AffiliateId = c.Int(nullable: false),
                        Sku = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                        ImageUrl = c.String(),
                        Category = c.String(),
                        InStock = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.AffiliateProductId)
                .ForeignKey("dbo.Affiliate", t => t.AffiliateId, cascadeDelete: true)
                .Index(t => t.AffiliateId);
            
            CreateTable(
                "dbo.Affiliate",
                c => new
                    {
                        AffiliateId = c.Int(nullable: false, identity: true),
                        AffiliateName = c.String(),
                        AffiliateUrl = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AffiliateId);
            
            CreateTable(
                "dbo.BrewSessionComment",
                c => new
                    {
                        BrewSessionCommentId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BrewSessionId = c.Int(nullable: false),
                        CommentText = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BrewSessionCommentId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BrewSessionId);
            
           
            CreateTable(
                "dbo.Content",
                c => new
                    {
                        ContentId = c.Int(nullable: false, identity: true),
                        ContentTypeId = c.Int(nullable: false),
                        Name = c.String(maxLength:100),
                        ShortName = c.String(maxLength:25),
                        Text = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContentId)
                .Index(i=>i.Name, unique:true)
                .Index(i => i.ShortName, unique: true);
            
            CreateTable(
                "dbo.FermentableAffiliateProduct",
                c => new
                    {
                        IngredientAffiliateProductId = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        AffiliateProductId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Rank = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        FermentableId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientAffiliateProductId)
                .ForeignKey("dbo.AffiliateProduct", t => t.AffiliateProductId, cascadeDelete: true)
                .ForeignKey("dbo.Fermentable", t => t.FermentableId, cascadeDelete: true)
                .Index(t => t.AffiliateProductId)
                .Index(t => t.FermentableId);
            
            CreateTable(
                "dbo.IngredientCategory",
                c => new
                    {
                        IngredientTypeId = c.Int(nullable: false),
                        Category = c.String(nullable: false, maxLength: 128),
                        Rank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IngredientTypeId, t.Category });
            
            CreateTable(
                "dbo.NewsletterSignup",
                c => new
                    {
                        NewsletterSignupId = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        IPAddress = c.String(),
                        Source = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NewsletterSignupId);
            
            
            CreateTable(
                "dbo.SendToShopOrderItem",
                c => new
                    {
                        SendToShopOrderItemId = c.Int(nullable: false, identity: true),
                        SendToShopOrderId = c.Int(nullable: false),
                        IngredientTypeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Unit = c.String(),
                        Instructions = c.String(),
                    })
                .PrimaryKey(t => t.SendToShopOrderItemId)
                .ForeignKey("dbo.SendToShopOrder", t => t.SendToShopOrderId, cascadeDelete: true)
                .Index(t => t.SendToShopOrderId);
            
            CreateTable(
                "dbo.SendToShopOrder",
                c => new
                    {
                        SendToShopOrderId = c.Int(nullable: false, identity: true),
                        PartnerId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        RecipeId = c.Int(nullable: false),
                        SendToShopOrderStatusId = c.Int(nullable: false),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        AllowTextMessages = c.Boolean(nullable: false),
                        Comments = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.SendToShopOrderId);
            
         
            CreateTable(
                "dbo.UserAuthToken",
                c => new
                    {
                        UserAuthTokenId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AuthToken = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserAuthTokenId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserFeedback",
                c => new
                    {
                        UserFeedbackId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Feedback = c.String(),
                        UserHostAddress = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateResponded = c.DateTime(),
                        RespondedBy = c.Int(),
                    })
                .PrimaryKey(t => t.UserFeedbackId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserReputationSummary",
                c => new
                    {
                        UserReputationAwardId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        ReputationAwardTypeId = c.Int(nullable: false),
                        ReputationAwardTypeName = c.String(),
                        ReputationObjectTypeId = c.Int(nullable: false),
                        ReputationObjectTypeName = c.String(),
                        ReputationObjectName = c.String(),
                        DateAwarded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserReputationAwardId);
            
            CreateTable(
                "dbo.UserBadge",
                c => new
                    {
                        BadgeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BadgeId, t.UserId })
                .ForeignKey("dbo.Badge", t => t.BadgeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BadgeId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFeedback", "UserId", "dbo.User");
            DropForeignKey("dbo.UserAuthToken", "UserId", "dbo.User");
            DropForeignKey("dbo.SendToShopOrderItem", "SendToShopOrderId", "dbo.SendToShopOrder");
            DropForeignKey("dbo.FermentableAffiliateProduct", "FermentableId", "dbo.Fermentable");
            DropForeignKey("dbo.FermentableAffiliateProduct", "AffiliateProductId", "dbo.AffiliateProduct");
            DropForeignKey("dbo.BrewSessionComment", "UserId", "dbo.User");
            DropForeignKey("dbo.AffiliateProduct", "AffiliateId", "dbo.Affiliate");
            DropForeignKey("dbo.Adjunct", "CreatedByUserId", "dbo.User");
            DropForeignKey("dbo.UserReputationAward", "UserId", "dbo.User");
            DropForeignKey("dbo.UserPartnerAdmin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserPartnerAdmin", "PartnerId", "dbo.Partner");
            DropForeignKey("dbo.PartnerSendToShopSettings", "PartnerId", "dbo.Partner");
            DropForeignKey("dbo.PartnerSendToShopIngredient", "PartnerId", "dbo.Partner");
            DropForeignKey("dbo.PartnerService", "PartnerId", "dbo.Partner");
            DropForeignKey("dbo.UserOAuthUserId", "UserId", "dbo.User");
            DropForeignKey("dbo.UserNotificationType", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserConnection", "UserId", "dbo.User");
            DropForeignKey("dbo.UserAdmin", "UserId", "dbo.User");
            DropForeignKey("dbo.TastingNote", "UserId", "dbo.User");
            DropForeignKey("dbo.TastingNote", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.TastingNote", "BrewSessionId", "dbo.BrewSession");
            DropForeignKey("dbo.RecipeComment", "UserId", "dbo.User");
            DropForeignKey("dbo.RecipeYeast", "IngredientId", "dbo.Yeast");
            DropForeignKey("dbo.Yeast", "CreatedByUserId", "dbo.User");
            DropForeignKey("dbo.RecipeYeast", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.Recipe", "CreatedBy", "dbo.User");
            DropForeignKey("dbo.RecipeStep", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.RecipeComment", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.RecipeMashStep", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.RecipeMashStep", "IngredientId", "dbo.MashStep");
            DropForeignKey("dbo.MashStep", "CreatedByUserId", "dbo.User");
            DropForeignKey("dbo.RecipeHop", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.RecipeHop", "IngredientId", "dbo.Hop");
            DropForeignKey("dbo.Hop", "CreatedByUserId", "dbo.User");
            DropForeignKey("dbo.RecipeFermentable", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.RecipeFermentable", "IngredientId", "dbo.Fermentable");
            DropForeignKey("dbo.Fermentable", "CreatedByUserId", "dbo.User");
            DropForeignKey("dbo.BrewSession", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.Recipe", "BjcpStyleSubCategoryId", "dbo.BjcpStyle");
            DropForeignKey("dbo.BjcpStyleUrlFriendlyName", "SubCategoryId", "dbo.BjcpStyle");
            DropForeignKey("dbo.RecipeAdjunct", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.RecipeAdjunct", "IngredientId", "dbo.Adjunct");
            DropForeignKey("dbo.BrewSession", "UserId", "dbo.User");
            DropForeignKey("dbo.UserBadge", "UserId", "dbo.User");
            DropForeignKey("dbo.UserBadge", "BadgeId", "dbo.Badge");
            DropIndex("dbo.UserBadge", new[] { "UserId" });
            DropIndex("dbo.UserBadge", new[] { "BadgeId" });
            DropIndex("dbo.UserFeedback", new[] { "UserId" });
            DropIndex("dbo.UserAuthToken", new[] { "UserId" });
            DropIndex("dbo.SendToShopOrderItem", new[] { "SendToShopOrderId" });
            DropIndex("dbo.FermentableAffiliateProduct", new[] { "FermentableId" });
            DropIndex("dbo.FermentableAffiliateProduct", new[] { "AffiliateProductId" });
            DropIndex("dbo.BrewSessionComment", new[] { "BrewSessionId" });
            DropIndex("dbo.BrewSessionComment", new[] { "UserId" });
            DropIndex("dbo.AffiliateProduct", new[] { "AffiliateId" });
            DropIndex("dbo.UserReputationAward", new[] { "UserId" });
            DropIndex("dbo.PartnerSendToShopSettings", new[] { "PartnerId" });
            DropIndex("dbo.PartnerSendToShopIngredient", new[] { "PartnerId" });
            DropIndex("dbo.PartnerService", new[] { "PartnerId" });
            DropIndex("dbo.UserPartnerAdmin", new[] { "PartnerId" });
            DropIndex("dbo.UserPartnerAdmin", new[] { "UserId" });
            DropIndex("dbo.UserOAuthUserId", new[] { "UserId" });
            DropIndex("dbo.UserNotificationType", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserConnection", new[] { "UserId" });
            DropIndex("dbo.UserAdmin", new[] { "UserId" });
            DropIndex("dbo.TastingNote", new[] { "UserId" });
            DropIndex("dbo.TastingNote", new[] { "RecipeId" });
            DropIndex("dbo.TastingNote", new[] { "BrewSessionId" });
            DropIndex("dbo.Yeast", new[] { "CreatedByUserId" });
            DropIndex("dbo.RecipeYeast", new[] { "IngredientId" });
            DropIndex("dbo.RecipeYeast", new[] { "RecipeId" });
            DropIndex("dbo.RecipeStep", new[] { "RecipeId" });
            DropIndex("dbo.MashStep", new[] { "CreatedByUserId" });
            DropIndex("dbo.RecipeMashStep", new[] { "IngredientId" });
            DropIndex("dbo.RecipeMashStep", new[] { "RecipeId" });
            DropIndex("dbo.Hop", new[] { "CreatedByUserId" });
            DropIndex("dbo.RecipeHop", new[] { "IngredientId" });
            DropIndex("dbo.RecipeHop", new[] { "RecipeId" });
            DropIndex("dbo.Fermentable", new[] { "CreatedByUserId" });
            DropIndex("dbo.RecipeFermentable", new[] { "IngredientId" });
            DropIndex("dbo.RecipeFermentable", new[] { "RecipeId" });
            DropIndex("dbo.BjcpStyleUrlFriendlyName", new[] { "SubCategoryId" });
            DropIndex("dbo.RecipeAdjunct", new[] { "IngredientId" });
            DropIndex("dbo.RecipeAdjunct", new[] { "RecipeId" });
            DropIndex("dbo.Recipe", new[] { "BjcpStyleSubCategoryId" });
            DropIndex("dbo.Recipe", new[] { "CreatedBy" });
            DropIndex("dbo.RecipeComment", new[] { "RecipeId" });
            DropIndex("dbo.RecipeComment", new[] { "UserId" });
            DropIndex("dbo.BrewSession", new[] { "UserId" });
            DropIndex("dbo.BrewSession", new[] { "RecipeId" });
            DropIndex("dbo.Adjunct", new[] { "CreatedByUserId" });
            DropTable("dbo.UserBadge");
            DropTable("dbo.UserReputationSummary");
            DropTable("dbo.UserFeedback");
            DropTable("dbo.UserAuthToken");
            DropTable("dbo.SendToShopOrder");
            DropTable("dbo.SendToShopOrderItem");
            DropTable("dbo.NewsletterSignup");
            DropTable("dbo.IngredientCategory");
            DropTable("dbo.FermentableAffiliateProduct");
            DropTable("dbo.Content");
            DropTable("dbo.BrewSessionComment");
            DropTable("dbo.Affiliate");
            DropTable("dbo.AffiliateProduct");
            DropTable("dbo.UserReputationAward");
            DropTable("dbo.PartnerSendToShopSettings");
            DropTable("dbo.PartnerSendToShopIngredient");
            DropTable("dbo.PartnerService");
            DropTable("dbo.Partner");
            DropTable("dbo.UserPartnerAdmin");
            DropTable("dbo.UserOAuthUserId");
            DropTable("dbo.UserNotificationType");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserConnection");
            DropTable("dbo.UserAdmin");
            DropTable("dbo.TastingNote");
            DropTable("dbo.Yeast");
            DropTable("dbo.RecipeYeast");
            DropTable("dbo.RecipeStep");
            DropTable("dbo.MashStep");
            DropTable("dbo.RecipeMashStep");
            DropTable("dbo.Hop");
            DropTable("dbo.RecipeHop");
            DropTable("dbo.Fermentable");
            DropTable("dbo.RecipeFermentable");
            DropTable("dbo.BjcpStyleUrlFriendlyName");
            DropTable("dbo.BjcpStyle");
            DropTable("dbo.RecipeAdjunct");
            DropTable("dbo.Recipe");
            DropTable("dbo.RecipeComment");
            DropTable("dbo.BrewSession");
            DropTable("dbo.Badge");
            DropTable("dbo.User");
            DropTable("dbo.Adjunct");
        }
    }
}
