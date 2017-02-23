using System;
using System.Linq;
using Brewgr.Web.Core.Configuration;
using ctorx.Core.Formatting;

namespace Brewgr.Web.Core.Model
{
	public class BrewgrUrlBuilder
	{
		readonly IWebSettings WebSettings;

		/// <summary>
		/// ctor the Mighty
		/// </summary>
		public BrewgrUrlBuilder(IWebSettings webSettings)
		{
			this.WebSettings = webSettings;
		}

		/// <summary>
		/// Builds a Recipe Detail Url
		/// </summary>
		public string BuildDetailUrl(RecipeSummary recipeSummary)
		{
			// NOTE: This Code Duplicated in Brewgr.Web UrlHelperExtensions
			var recipeNameForUrl = StringCleaner.CleanForUrl(recipeSummary.RecipeName);
			if (!string.IsNullOrWhiteSpace(recipeSummary.BJCPStyleName) && recipeSummary.BJCPStyleName.ToLower() != "unknown style")
			{
				recipeNameForUrl += "-" + StringCleaner.CleanForUrl(recipeSummary.BJCPStyleName);
			}

			if (!recipeNameForUrl.ToLower().Trim().EndsWith("recipe"))
			{
				recipeNameForUrl += "-recipe";
			}

			return string.Format("/recipe/{0}/{1}", recipeSummary.RecipeId, recipeNameForUrl);
		}

        /// <summary>
        /// Builds a Recipe Detail Url
        /// </summary>
        public string BuildBrewSessionDetailUrl(BrewSession brewSession)
        {
            var brewSessionNameForUrl = StringCleaner.CleanForUrl(brewSession.RecipeSummary.RecipeName);
            brewSessionNameForUrl += "-brew-session";
            return string.Format("/brew/{0}/{1}", brewSession.BrewSessionId, brewSessionNameForUrl);
        }

		/// <summary>
		/// Builds a user profile url
		/// </summary>
		public string BuildUserProfileUrl(string username)
		{
			// NOTE: This Code Duplicated in Brewgr.Web UrlHelperExtensions
			return string.Format("/!/{1}",  StringCleaner.CleanForUrl(username));
		}
	}
}