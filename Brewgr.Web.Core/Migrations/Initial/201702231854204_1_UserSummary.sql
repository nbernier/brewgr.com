CREATE VIEW [dbo].[UserSummary]
AS

SELECT
	usr.UserId
 ,	Username = usr.CalculatedUsername
 ,	EmailAddress
 ,	FirstName
 ,	LastName
 ,	usr.DateCreated
 ,	usr.Bio
 ,	usr.IsActive
 ,	[RecipeCount] = ISNULL(rcpcount.RecipeCount, 0)
 ,	[BrewSessionCount] = ISNULL(brewcount.BrewSessionCount, 0)
 ,	[CommentCount] = ISNULL(commcount.[CommentCount], 0) 
 ,	[IsAdmin] = CAST(CASE WHEN adm.UserId IS NOT NULL THEN 1 ELSE 0 END AS BIT)
 ,	[IsPartner] = padm.IsPartner
 ,	[HasCustomUsername] = usr.HasCustomUsername
FROM
	[User] usr with(nolock)
LEFT JOIN
	[UserAdmin] adm with(nolock)
	 ON usr.UserId = adm.UserId
	  AND adm.IsActive = 1
LEFT JOIN
	(
		SELECT
			usr.UserId
		 ,	IsPartner = CAST(CASE WHEN COUNT(padm.UserId) > 0 THEN 1 ELSE 0 END AS BIT)
		FROM
			[User] usr with(nolock)
		LEFT JOIN
			UserPartnerAdmin padm with(nolock)
			 ON usr.UserId = padm.UserId
			  AND padm.IsActive = 1
		GROUP BY
			usr.UserId
	) padm
	 ON usr.UserId = padm.UserId
LEFT JOIN
(
	SELECT CreatedBy, [RecipeCount] = COUNT(*)
	FROM Recipe with(nolock)
	WHERE IsActive = 1
	AND IsPublic = 1
	GROUP BY CreatedBy
) rcpcount
	ON usr.UserId = rcpcount.CreatedBy
LEFT JOIN
(
	SELECT BrewedBy = UserId, [BrewSessionCount] = COUNT(*)
	FROM BrewSession with(nolock)
	WHERE IsActive = 1
	AND IsPublic = 1
	GROUP BY UserId
) brewcount
	ON usr.UserId = brewcount.BrewedBy
LEFT JOIN
(
	SELECT UserId, [CommentCount] = COUNT(*)
	FROM RecipeComment with(nolock)
	WHERE IsActive = 1
	GROUP BY UserId
) commcount
	ON usr.UserId = commcount.UserId
