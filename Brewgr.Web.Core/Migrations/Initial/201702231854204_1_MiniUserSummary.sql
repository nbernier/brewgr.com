
CREATE VIEW [dbo].[MiniUserSummary]
AS

SELECT
	UserId
 ,	Username = CalculatedUsername
 ,	EmailAddress
FROM
	[User] with(nolock)


