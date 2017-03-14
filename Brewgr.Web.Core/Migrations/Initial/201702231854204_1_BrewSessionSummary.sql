
--------------------------------------
-- BrewSession Summary View -- 
--------------------------------------
CREATE VIEW [dbo].[BrewSessionSummary]
AS

SELECT
	brw.BrewSessionId
 ,	brw.RecipeId
 ,	rcp.RecipeTypeId
 ,	rcp.RecipeName
 ,	[RecipeBjcpStyleSubCategoryId] = rcp.BjcpStyleSubCategoryId
 ,	[RecipeBjcpStyleName] = ISNULL(sty.SubCategoryName, 'Unknown Style')	
 ,	[BrewedBy] = brw.UserId
 ,	[BrewedByUsername] = usr.CalculatedUsername
 ,	[BrewedByUserEmail] = usr.EmailAddress
 ,	brw.BrewDate
 ,	RecipeImageUrlRoot = rcp.ImageUrlRoot
 ,	Summary = LEFT(RTRIM(LTRIM(brw.Notes)), 1000)
 ,	HasWaterInfusion = CAST (
		CASE WHEN
			[GrainWeight] IS NOT NULL
				OR
			[GrainTemp] IS NOT NULL
				OR
			brw.[BoilTime] IS NOT NULL
				OR
			[BoilVolumeEst] IS NOT NULL
				OR
			[FermentVolumeEst] IS NOT NULL
				OR
			[TargetMashTemp] IS NOT NULL
				OR
			[MashThickness] IS NOT NULL
		THEN 1 ELSE 0 END
	AS BIT)
 ,	HasMashBoil = CAST(
		CASE WHEN
			[MashPH] IS NOT NULL
				OR
			[MashStartTemp] IS NOT NULL
				OR
			[MashEndTemp] IS NOT NULL
				OR
			[MashTime] IS NOT NULL
				OR
			[BoilVolumeActual] IS NOT NULL
				OR
			[PreBoilGravity] IS NOT NULL
				OR
			[BoilTimeActual] IS NOT NULL
				OR
			[PostBoilVolume] IS NOT NULL
		THEN 1 ELSE 0 END
	AS BIT)
 ,	HasFermentation = CAST(
		CASE WHEN
			[FermentVolumeActual] IS NOT NULL
				OR
			[OriginalGravity] IS NOT NULL
				OR
			[FinalGravity] IS NOT NULL
		THEN 1 ELSE 0 END
	AS BIT)
 ,	HasConditioning = CAST(
		CASE WHEN 
			[ConditionDate] IS NOT NULL
				OR
			[ConditionTypeId] IS NOT NULL
				OR
			[PrimingSugarType] IS NOT NULL
				OR
			[PrimingSugarAmount] IS NOT NULL
				OR
			[KegPSI] IS NOT NULL
		THEN 1 ELSE 0 END 
	AS BIT)
 ,	HasTastingNotes = CAST(CASE WHEN tast.[Count] IS NOT NULL THEN 1 ELSE 0 END AS BIT)
 ,	[RecipeSrm] = rcp.Srm
 ,	brw.IsActive
 ,	brw.IsPublic
 ,	brw.DateCreated
 ,	brw.DateModified
FROM
	BrewSession brw with(nolock)
JOIN
	[User] usr with(nolock)
	 ON usr.UserId = brw.UserId
JOIN
	Recipe rcp with(nolock)
	 ON rcp.RecipeId = brw.RecipeId
LEFT JOIN
	[BJCPStyle] sty with(nolock)
	 ON rcp.BjcpStyleSubCategoryId = sty.SubCategoryID
LEFT JOIN
	(
		SELECT 
			BrewSessionId 
		 ,	[Count] = COUNT(*)
		FROM
			TastingNote with(nolock)
		GROUP BY
			BrewSessionId
	) tast
	 ON tast.BrewSessionId = brw.BrewSessionId
WHERE
	rcp.IsActive = 1
	AND rcp.IsPublic = 1




