
CREATE VIEW [dbo].[TastingNoteSummary]
AS

SELECT
	note.TastingNoteId
 ,	note.BrewSessionId
 ,	RecipeID = ISNULL(rec.RecipeId, recalt.RecipeId)
 ,	RecipeName = ISNULL(rec.RecipeName, recalt.RecipeName)
 ,	[RecipeStyleName] = ISNULL(rec.BJCPStyleName, recalt.BJCPStyleName)
 ,	[RecipeImage] = ISNULL(rec.ImageUrlRoot, recalt.ImageUrlRoot)
 ,	[RecipeSrm] = ISNULL(rec.Srm, recalt.Srm)
 ,	note.UserId
 ,	[TastingUsername] = usr.CalculatedUserName
 ,	[TastingUserEmailAddress] = usr.EmailAddress
 ,	note.TasteDate
 ,	note.Rating
 ,	note.Notes
 ,	note.DateCreated
FROM
	TastingNote note with(nolock)
LEFT JOIN
	BrewSession brew with(nolock)
	 on brew.BrewSessionId = note.BrewSessionID
LEFT JOIN
	RecipeSummary rec with(nolock)
	 ON note.RecipeId = rec.RecipeId
LEFT JOIN
	RecipeSummary recalt with(nolock)
	 ON brew.RecipeId = recalt.RecipeId
JOIN
	[User] usr with(nolock)
	 ON usr.UserId = note.UserId
WHERE
	note.IsActive = 1
	AND note.IsPublic = 1
	AND (rec.RecipeId IS NULL OR (rec.IsActive = 1 AND rec.IsPublic = 1))
	AND (note.BrewSessionID IS NULL OR (brew.IsActive = 1 AND brew.IsPublic = 1))


