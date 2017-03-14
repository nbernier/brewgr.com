CREATE VIEW [dbo].[RecipeCommentSummary]
AS

SELECT
	cmt.RecipeCommentId
 ,	cmt.UserId
 ,	cmt.RecipeId
 ,	UserName = usr.CalculatedUsername
 ,	usr.EmailAddress
 ,	cmt.CommentText
 ,	cmt.DateCreated
 ,	cmt.IsActive
FROM
	RecipeComment [cmt] with(nolock)
JOIN
	[User] usr with(nolock)
	 ON cmt.UserId = usr.UserId
	 

