CREATE PROCEDURE [dbo].[GetObjectIdsForDashboard]
(
	@UserId INT
 ,	@Amount INT = 10
 ,	@OlderThanDate DATETIME = NULL
)
AS

SELECT
	[Type]
 ,	[Id]
 ,	[Date]
FROM
(
	SELECT
		objs.UserId
	 ,	objs.[Type]
	 ,	objs.Id
	 ,	objs.[Date]
	 ,	RowNumber = ROW_NUMBER() OVER (ORDER BY objs.[Date] DESC)
	FROM
		UserConnection conn with(nolock)
	JOIN
	(
		SELECT
			[UserId] = CreatedBy
		 ,	[Type] = 'Recipe'
		 ,	[Id] = RecipeId
		 ,	[Date] = DateCreated
		FROM
			Recipe with(nolock)
		WHERE
			DateCreated < ISNULL(@OlderThanDate, GETDATE())
			AND IsActive = 1
			AND IsPublic = 1
		UNION
	
		SELECT
			[UserId] = UserId
		 ,	[Type] = 'BrewSession'
		 ,	[Id] = BrewSessionId
		 ,	[Date] = BrewDate	
		FROM
			BrewSession with(nolock)
		WHERE
			BrewDate < ISNULL(@OlderThanDate, GETDATE())
			AND IsActive = 1
			AND IsPublic = 1
		UNION

		SELECT
			[UserId] = UserId
		 ,	[Type] = 'TastingNote'
		 ,	[Id] = TastingNoteId
		 ,	[Date] = TasteDate
		FROM
			TastingNoteSummary with(nolock)
		WHERE
			DateCreated < ISNULL(@OlderThanDate, GETDATE()) 

	) objs
	 ON 
		conn.UserId = objs.UserId
	 WHERE
		conn.FollowedById = @UserId
		AND
		conn.IsActive = 1
) T
WHERE
	RowNumber <= @Amount

