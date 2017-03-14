CREATE VIEW [dbo].[vw_UserActivityReport] 
AS

SELECT
	usr.UserId
 ,	usr.FirstName
 ,	usr.LastName
 ,	usr.Username
 ,	lgn.LoginCount
 ,	RecipeCount = ISNULL(rcp.RecipeCount, 0)
 ,	BrewCount = ISNULL(sess.BrewCount, 0)
 ,	CommentCount = ISNULL(cmt.CommentCount, 0)
 ,	HasOAuth = CASE WHEN oath.UserId IS NOT NULL THEN 1 ELSE 0 END
 ,	CustomIngredients = ISNULL(ing.Total, 0)
 ,	EmailOptIn = CASE WHEN news.EmailAddress IS NOT NULL THEN 1 ELSE 0 END
 ,	Suggestions = ISNULL(sugg.SuggestionTotal, 0)
FROM
	[User] usr with(nolock)
JOIN
	(
		SELECT UserId, LoginCount = COUNT(*)
		FROM UserLogin with(nolock) GROUP BY UserId
	) lgn
	 ON usr.UserId = lgn.UserId
LEFT JOIN
(
	SELECT
		CreatedBy
	 ,	RecipeCount = COUNT(*)
	 FROM
	 	Recipe with(nolock)
	GROUP BY
		CreatedBy
) rcp
 ON usr.UserId = rcp.CreatedBy
LEFT JOIN
(
	SELECT
		BrewedBy = UserId
	 ,	BrewCount = COUNT(BrewSessionId)
	FROM
		BrewSession with(nolock)
	GROUP BY
		UserId
) sess
 ON usr.UserId = sess.BrewedBy
LEFT JOIN
(
	SELECT
		UserId
	 ,	CommentCount = COUNT(*)
	FROM
		RecipeComment with(nolock)
	GROUP BY
		UserId
) cmt
 ON usr.UserId = cmt.UserId
LEFT JOIN
	dbo.UserOAuthUserId oath with(nolock)
	 ON usr.UserId = oath.UserId
LEFT JOIN
(
	SELECT 
		UserId = CreatedByUserId
	 ,	Total = SUM(Total)
	FROM
	(
		SELECT CreatedByUserId, Total = COUNT(*) FROM Fermentable with(nolock) WHERE CreatedByUserId IS NOT NULL GROUP BY CreatedByUserId UNION
		SELECT CreatedByUserId, Total = COUNT(*) FROM Hop with(nolock) WHERE CreatedByUserId IS NOT NULL GROUP BY CreatedByUserId UNION
		SELECT CreatedByUserId, Total = COUNT(*) FROM Yeast with(nolock) WHERE CreatedByUserId IS NOT NULL GROUP BY CreatedByUserId UNION
		SELECT CreatedByUserId, Total = COUNT(*) FROM Adjunct with(nolock) WHERE CreatedByUserId IS NOT NULL GROUP BY CreatedByUserId
	) T
	GROUP BY
		CreatedByUserId
) ing
 ON usr.UserId = ing.UserId
LEFT JOIN
	dbo.NewsletterSignup news with(nolock)
	 ON usr.EmailAddress = news.EmailAddress
LEFT JOIN
(
	SELECT
		UserId
	 ,	SuggestionTotal = COUNT(*)
	FROM
		dbo.UserSuggestion sugg with(nolock)
	GROUP BY
		UserId
) sugg
 ON usr.UserId = sugg.UserId
GROUP BY
	usr.UserId
 ,	usr.FirstName
 ,	usr.LastName
 ,	usr.Username
 ,	lgn.LoginCount
 ,	rcp.RecipeCount
 ,	sess.BrewCount
 ,	cmt.CommentCount
 ,	oath.UserId
 ,	ing.Total
 ,	news.EmailAddress
 ,	sugg.SuggestionTotal
