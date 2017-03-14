
CREATE VIEW [dbo].[BjcpStyleSummary]
AS

SELECT
	sty.SubCategoryId
 ,	SubCategoryName
 ,	CategoryId
 ,	CategoryName
 ,	frnm.UrlFriendlyName
 ,	[RecipeCount] = COUNT(rcp.RecipeId)
FROM
	BjcpStyle sty with(nolock)
JOIN
	BjcpStyleUrlFriendlyName frnm with(nolock)
	 ON sty.SubCategoryId = frnm.SubCategoryId
LEFT JOIN
	Recipe rcp with(nolock)
	 ON sty.SubCategoryId = rcp.BjcpStyleSubCategoryId
	 AND rcp.IsActive = 1
	 AND rcp.IsPublic = 1
GROUP BY
	sty.SubCategoryId
 ,	SubCategoryName
 ,	CategoryId
 ,	CategoryName
 ,	frnm.UrlFriendlyName




