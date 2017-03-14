
CREATE VIEW [dbo].[RecipeSummary] AS

SELECT
	rcp.RecipeId
 ,	rcp.RecipeTypeId
 ,	rcp.OriginalRecipeId
 ,	[OriginalRecipeName] = orig.RecipeName
 ,	rcp.CreatedBy
 ,	CreatedByUserName = usr.CalculatedUsername
 ,	CreatedByUserEmail = usr.EmailAddress
 ,	rcp.UnitTypeId
 ,	rcp.IbuFormulaId
 ,	rcp.BjcpStyleSubCategoryId
 ,	[BJCPStyleName] = ISNULL(sty.SubCategoryName, 'Unknown Style')
 ,	rcp.RecipeName
 ,	rcp.ImageUrlRoot
 ,	rcp.Description
 ,	rcp.BatchSize
 ,	rcp.BoilSize
 ,	rcp.BoilTime
 ,	rcp.Efficiency
 ,	rcp.IsActive
 ,	rcp.IsPublic
 ,	rcp.DateCreated
 ,	rcp.DateModified
 ,	rcp.Og
 ,	rcp.Fg
 ,	rcp.Srm
 ,	rcp.Ibu
 ,	rcp.BgGu
 ,	rcp.Abv
 ,	rcp.Calories
 ,	[UserIsAdmin] = CAST(CASE WHEN adm.UserId IS NOT NULL THEN 1 ELSE 0 END AS BIT)
 ,	[BrewSessionCount] = ISNULL(brwcount.BrewSessionCount, 0)
FROM
	Recipe rcp with(nolock)
JOIN
	[User] usr with(nolock)
	 ON rcp.CreatedBy = usr.UserId
LEFT JOIN
	[Recipe] orig with(nolock)
	 ON rcp.OriginalRecipeId = orig.RecipeId
LEFT JOIN
	[BJCPStyle] sty with(nolock)
	 ON rcp.BjcpStyleSubCategoryId = sty.SubCategoryID
LEFT JOIN
	[UserAdmin] adm with(nolock)
	 ON rcp.CreatedBy = adm.UserId
LEFT JOIN
	(SELECT 
		RecipeId
	  ,	BrewSessionCount = COUNT(BrewSessionId) 
	 FROM 
		[BrewSession] brw (nolock)
	 WHERE
		IsActive = 1
		AND IsPublic = 1
	 GROUP BY
		RecipeId
	) brwcount
	 ON rcp.RecipeId = brwcount.RecipeId






