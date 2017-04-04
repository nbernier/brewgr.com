using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Brewgr.Web.Core.Model;
using ctorx.Core.Data;

namespace Brewgr.Web.Core.Data
{
	public class BrewgrContext : AbstractDbContext, IDataContext
	{
        private const string CNN_KEY = "Brewgr_ConnectionString";

        public BrewgrContext() : base(CNN_KEY)
        {
            Database.SetInitializer<BrewgrContext>(new MigrateDatabaseToLatestVersion<BrewgrContext, Migrations.Configuration>(CNN_KEY));
        }

        public virtual IDbSet<User> Users { get; set; }
		public virtual IDbSet<UserSummary> UserSummaries { get; set; }
		public virtual IDbSet<MiniUserSummary> MiniUserSummaries { get; set; }
		public virtual IDbSet<UserAdmin> UserAdmins { get; set; }
		public virtual IDbSet<UserConnection> UserConnections { get; set; }
		public virtual IDbSet<UserReputationAward> UserReputationAwards { get; set; }
		public virtual IDbSet<UserReputationSummary> UserReputationSummaries { get; set; }
		public virtual IDbSet<UserNotificationType> UserNotificationTypes { get; set; }
		public virtual IDbSet<Badge> Badges { get; set; }
		public virtual IDbSet<UserOAuthUserId> UserOAuthUserIds { get; set; }
		public virtual IDbSet<UserLogin> UserLogins { get; set; }
		public virtual IDbSet<UserAuthToken> UserAuthTokens { get; set; }
		public virtual IDbSet<Recipe> Recipes { get; set; }
		public virtual IDbSet<RecipeMetaData> RecipeMetaDatas { get; set; }
		public virtual IDbSet<RecipeSummary> RecipeSummaries { get; set; }
		public virtual IDbSet<Fermentable> Fermentables{ get; set; }
		public virtual IDbSet<Hop> Hops{ get; set; }
		public virtual IDbSet<Yeast> Yeasts { get; set; }
		public virtual IDbSet<Adjunct> Adjuncts { get; set; }
        public virtual IDbSet<MashStep> MashSteps { get; set; }
		public virtual IDbSet<NewsletterSignup> NewsletterSignups { get; set; }
        public virtual IDbSet<BjcpStyle> BJCPStyles { get; set; }
		public virtual IDbSet<BjcpStyleSummary> BjcpStyleSummaries { get; set; }
		public virtual IDbSet<BjcpStyleUrlFriendlyName> BjcpStyleUrlFriendlyNames { get; set; }
		public virtual IDbSet<UserFeedback> UserFeedbacks { get; set; }
        public virtual IDbSet<RecipeComment> RecipeComments { get; set; }
        public virtual IDbSet<BrewSessionComment> BrewSessionComments { get; set; }
        public virtual IDbSet<TastingNote> TastingNotes { get; set; }
        public virtual IDbSet<RecipeCommentSummary> RecipeCommentSummaries { get; set; }
		public virtual IDbSet<BrewSession> BrewSessions { get; set; }
		public virtual IDbSet<BrewSessionSummary> BrewSessionSummaries { get; set; }        //public virtual IDbSet<BrewSessionConditioning> BrewConditionings { get; set; }
		public virtual IDbSet<IngredientCategory> IngredientCategories { get; set; }
		public virtual IDbSet<Affiliate> Affiliates { get; set; }
		public virtual IDbSet<AffiliateProduct> AffiliateProducts { get; set; }
		public virtual IDbSet<FermentableAffiliateProduct> FermentableAffiliateProducts { get; set; }
		public virtual IDbSet<Content> Contents { get; set; }
		public virtual IDbSet<Partner> Partners { get; set; }
		public virtual IDbSet<PartnerSummary> PartnerSummaries { get; set; }
		public virtual IDbSet<PartnerService> PartnerServices { get; set; }
		public virtual IDbSet<UserPartnerAdmin> UserPartnerAdmins { get; set; }
		public virtual IDbSet<PartnerSendToShopSettings> PartnerSendToShopSettingses { get; set; }
		public virtual IDbSet<PartnerSendToShopIngredient> PartnerSendToShopIngredients { get; set; }
		public virtual IDbSet<SendToShopOrder> SendToShopOrders { get; set; }
		public virtual IDbSet<SendToShopOrderItem> SendToShopOrderItems { get; set; }
		public virtual IDbSet<TastingNoteSummary> TastingNoteSummaries { get; set; }
        public virtual IDbSet<UserSuggestion> UserSuggestions { get; set; }

        /// <summary>
        /// Fires on Model Creating
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Fermentable>().Property(x => x.IngredientId).HasColumnName("FermentableId");
			modelBuilder.Entity<Hop>().Property(x => x.IngredientId).HasColumnName("HopId");
			modelBuilder.Entity<Yeast>().Property(x => x.IngredientId).HasColumnName("YeastId");
			modelBuilder.Entity<Adjunct>().Property(x => x.IngredientId).HasColumnName("AdjunctId");
            modelBuilder.Entity<MashStep>().Property(x => x.IngredientId).HasColumnName("MashStepId");

			// Non-Inferred Keys
			modelBuilder.Entity<UserSummary>().HasKey(x => x.UserId);
			modelBuilder.Entity<MiniUserSummary>().HasKey(x => x.UserId);
			modelBuilder.Entity<RecipeSummary>().HasKey(x => x.RecipeId);
			modelBuilder.Entity<RecipeCommentSummary>().HasKey(x => x.RecipeCommentId);
			modelBuilder.Entity<Fermentable>().HasKey(x => x.IngredientId);
			modelBuilder.Entity<Hop>().HasKey(x => x.IngredientId);
			modelBuilder.Entity<Yeast>().HasKey(x => x.IngredientId);
			modelBuilder.Entity<Adjunct>().HasKey(x => x.IngredientId);
            modelBuilder.Entity<MashStep>().HasKey(x => x.IngredientId);
			modelBuilder.Entity<UserLogin>().HasKey(x => new { x.UserId, x.LoginDate });
            modelBuilder.Entity<UserOAuthUserId>().HasKey(x => new { x.UserId, x.OAuthProviderId });
            modelBuilder.Entity<BjcpStyle>().HasKey(x => x.SubCategoryId );
            modelBuilder.Entity<BjcpStyleSummary>().HasKey(x => x.SubCategoryId );
			modelBuilder.Entity<BjcpStyleUrlFriendlyName>().HasKey(x => x.SubCategoryId);
			modelBuilder.Entity<UserReputationSummary>().HasKey(x => x.UserReputationAwardId);
			modelBuilder.Entity<UserAdmin>().HasKey(x => x.UserId);
			modelBuilder.Entity<BrewSessionSummary>().HasKey(x => x.BrewSessionId);
			modelBuilder.Entity<UserNotificationType>().HasKey(x => new {x.UserId, x.NotificationTypeId });
			modelBuilder.Entity<UserConnection>().HasKey(x => new { x.UserId, x.FollowedById });
			modelBuilder.Entity<RecipeMetaData>().HasKey(x => x.RecipeId);
			modelBuilder.Entity<IngredientCategory>().HasKey(x => new { x.IngredientTypeId, x.Category });
			modelBuilder.Entity<FermentableAffiliateProduct>().HasKey(x => x.IngredientAffiliateProductId);
			modelBuilder.Entity<PartnerService>().HasKey(x => new { x.PartnerId, x.PartnerServiceTypeId});
			modelBuilder.Entity<UserPartnerAdmin>().HasKey(x => new { x.UserId, x.PartnerId });
			modelBuilder.Entity<PartnerSummary>().HasKey(x => x.PartnerId);
			modelBuilder.Entity<PartnerSendToShopSettings>().HasKey(x => x.PartnerId);
			modelBuilder.Entity<PartnerSendToShopIngredient>().HasKey(x => new { x.PartnerId, x.IngredientTypeId, x.IngredientId });
			modelBuilder.Entity<TastingNoteSummary>().HasKey(x => x.TastingNoteId);

			// Non-Inferred Relationships
			modelBuilder.Entity<UserAdmin>().HasRequired(x => x.User).WithOptional(x => x.UserAdmin);
			modelBuilder.Entity<Recipe>().HasOptional(x => x.BjcpStyle).WithMany(x => x.Recipes);
			modelBuilder.Entity<BjcpStyleUrlFriendlyName>().HasRequired(x => x.BjcpStyle).WithRequiredDependent(x => x.BjcpStyleUrlFriendlyName);
			modelBuilder.Entity<Recipe>().HasRequired(x => x.User).WithMany(x => x.Recipes).HasForeignKey(x => x.CreatedBy);

			modelBuilder.Entity<TastingNote>().HasRequired(x => x.User).WithMany(x => x.TastingNotes).HasForeignKey(x => x.UserId);
			modelBuilder.Entity<BrewSession>().HasRequired(x => x.BrewedByUser).WithMany(x => x.BrewSessions).HasForeignKey(x => x.UserId);
			modelBuilder.Entity<TastingNote>().HasRequired(x => x.User).WithMany(x => x.TastingNotes).HasForeignKey(x => x.UserId);
			modelBuilder.Entity<UserNotificationType>().HasRequired(x => x.User).WithMany(x => x.UserNotificationTypes);

			// Ingredient / User FK Mappings
			modelBuilder.Entity<Fermentable>().HasOptional(x => x.User).WithMany(x => x.Fermentables).HasForeignKey(x => x.CreatedByUserId);
			modelBuilder.Entity<Hop>().HasOptional(x => x.User).WithMany(x => x.Hops).HasForeignKey(x => x.CreatedByUserId);
			modelBuilder.Entity<Yeast>().HasOptional(x => x.User).WithMany(x => x.Yeasts).HasForeignKey(x => x.CreatedByUserId);
			modelBuilder.Entity<Adjunct>().HasOptional(x => x.User).WithMany(x => x.Adjuncts).HasForeignKey(x => x.CreatedByUserId);
            modelBuilder.Entity<MashStep>().HasOptional(x => x.User).WithMany(x => x.MashSteps).HasForeignKey(x => x.CreatedByUserId);
			
			modelBuilder.Entity<User>().HasRequired(x => x.UserSummary).WithRequiredPrincipal(x => x.User);
			modelBuilder.Entity<Recipe>().HasRequired(x => x.RecipeMetaData).WithRequiredPrincipal();
			modelBuilder.Entity<Recipe>().HasRequired(x => x.RecipeSummary).WithRequiredPrincipal();
			modelBuilder.Entity<Partner>().HasRequired(x => x.PartnerSummary).WithRequiredPrincipal();
			modelBuilder.Entity<Partner>().HasOptional(x => x.SendToShopSettings).WithRequired();


			// View Mappings
			modelBuilder.Entity<BjcpStyleSummary>().HasMany(x => x.Recipes).WithOptional(x => x.BjcpStyleSummary).HasForeignKey(z => z.BjcpStyleSubCategoryId);
            modelBuilder.Entity<RecipeSummary>().HasMany(x => x.RecipeComments).WithOptional().HasForeignKey(z => z.RecipeId);

			// Define Transparent Relationships
			modelBuilder.Entity<User>().HasMany(x => x.Badges)
				.WithMany(x => x.Users)
				.Map(x =>
				{
					x.ToTable("UserBadge");
					x.MapLeftKey("UserId");
					x.MapRightKey("BadgeId");
				});

			modelBuilder.Entity<Badge>().HasMany(x => x.Users)
				.WithMany(x => x.Badges)
				.Map(x =>
				{
					x.ToTable("UserBadge");
					x.MapLeftKey("BadgeId");
					x.MapRightKey("UserId");
				});


			modelBuilder.Entity<FermentableAffiliateProduct>().HasRequired(x => x.Ingredient).WithMany().Map(x => x.MapKey("FermentableId"));


			// Special Column Mappings
			modelBuilder.Entity<User>().Property(x => x.CalculatedUsername).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

			base.OnModelCreating(modelBuilder);
		}
	}
}