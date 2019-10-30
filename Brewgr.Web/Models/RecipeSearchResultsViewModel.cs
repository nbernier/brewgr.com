using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brewgr.Web.Core.Model;

namespace Brewgr.Web.Models
{
    public class RecipeSearchResultsViewModel
    {
        /// <summary>
        /// Gets or sets the RecipeSearchOptions
        /// </summary>
        public RecipeSearchOptions RecipeSearchOptions { get; set; }

        /// <summary>
        /// Gets or sets the Recipes
        /// </summary>
        public IList<RecipeSummaryViewModel> Recipes { get; set; }
    }
}