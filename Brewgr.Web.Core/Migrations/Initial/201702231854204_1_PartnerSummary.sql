
CREATE VIEW [dbo].[PartnerSummary]
AS

SELECT
	PartnerId
 ,	Name
FROM
	Partner
WHERE
	IsActive = 1

