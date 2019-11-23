﻿using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Brewgr.Web.Core.Model;
using Brewgr.Web.Core.Service;
using Brewgr.Web.Models;
using ctorx.Core.Messaging;

namespace Brewgr.Web.Controllers
{
	[RoutePrefix("")]
	public class RecipeSearchController : BrewgrController
	{
		readonly IBeerStyleService BeerStyleService;
		readonly IRecipeSearchService RecipeSearchService;

		/// <summary>
		/// ctor the Mighty
		/// </summary>
		public RecipeSearchController(IBeerStyleService beerStyleService, IRecipeSearchService recipeSearchService)
		{
			this.BeerStyleService = beerStyleService;
			this.RecipeSearchService = recipeSearchService;
		}

		[Route("homebrew-recipe-finder")]
		public ActionResult RecipeFinder()
		{
			ViewBag.Styles = this.BeerStyleService.GetStyleSummaries();
			return this.View();
		}

		[HttpPost]
		[Route("homebrew-recipe-finder")]
		public ActionResult RecipeFinder(RecipeSearchOptions recipeSearchOptions)
		{
			var recipes = this.RecipeSearchService.SearchRecipes(recipeSearchOptions);

			if(!recipes.Any())
			{		
				this.AppendMessage(new InfoMessage { Text = "We couldn't find any recipes that match your search options" });
				return this.View(recipeSearchOptions);
			}

            // Map it here
            var recipeSearchResults = new RecipeSearchResults { RecipeSearchOptions = recipeSearchOptions, Recipes = recipes };
            var recipesViewModel =  Mapper.Map(recipeSearchResults, new RecipeSearchResultsViewModel());

            return this.View("RecipeFinderResults", recipesViewModel);
		}
	}
}