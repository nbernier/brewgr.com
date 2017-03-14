

CREATE VIEW [dbo].[RecipeMetaData]
AS

SELECT
	rec.RecipeId
 ,	AverageRating = ISNULL(AVG(nte.Rating), CAST(0 AS FLOAT))
 ,	TastingNoteCount = COUNT(nte.TastingNoteId)
 ,	BrewSessionCount = COUNT(brw.BrewSessionId)
 ,	CommentCount = COUNT(rcom.RecipeCommentId)
 ,	CloneCount = COUNT(clon.RecipeId)
FROM
	Recipe rec with(nolock)
LEFT JOIN
	BrewSession brw with(nolock)
	 ON rec.RecipeId = brw.RecipeId
	  AND brw.IsPublic = 1
	  AND brw.IsActive = 1
LEFT JOIN
	TastingNoteSummary nte with(nolock)
	 ON rec.RecipeId = nte.RecipeId
LEFT JOIN
	RecipeComment rcom with(nolock)
	 ON rec.RecipeId = rcom.RecipeId
	  AND rcom.IsActive = 1
LEFT JOIN
	Recipe clon with(nolock)
	 ON clon.OriginalRecipeId = rec.RecipeId
WHERE
	rec.IsActive = 1
GROUP BY
	rec.RecipeId


