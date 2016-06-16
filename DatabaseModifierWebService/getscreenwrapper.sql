ALTER PROCEDURE [PEC].[MG_PROMOTION_WRAPPER_GetByPromotionID]
	@CMSPlayerID nvarchar(25),
	@PromotionID bigint,
	@IPAddress nvarchar(24) = '255.255.255.255'
AS

If NOT Exists (
	select * from GD_Sessions ses with (nolock)
	inner join GD_SessionsAttributes sesa with (nolock)
		on ses.SessionID = sesa.SessionID
	WHERE IPAddress != @IPAddress
	and ISNULL(CMSPlayerID, 0) = @CMSPlayerID
	and sesa.AttributeValue is not null
	and sesa.AttributeName = 'PlayerLang'
	and DATEDIFF(ss, ses.StartDate, GetDate()) < 10
)
Begin

	DECLARE @GameID bigint,
			@VariantID int,
			@PlayerLang int,
			@GameName nvarchar(40),
			@GameDescription nvarchar(256),
			@ButtonDesc nvarchar(512),
			@GameIcon nvarchar(512),
			@isButtonEnabled bit
	Set @PlayerLang = dbo.GD_SESSION_GetForPlayer (@IPAddress)

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT GameID = @GameID, VariantID = @VariantID
	FROM GD_PromotionalGames
	WHERE PromotionID = @PromotionID

	SELECT GameName = @GameName, GameDescription = @GameDescription
	FROM GD_Games
	WHERE GameID = @GameID

	SELECT AttributeValue = @ButtonDesc
	FROM GD_GameAttributes
	WHERE ObjectName LIKE 'Play_BTN' AND AttributeName LIKE 'Text'
						AND LanguageID = @PlayerLang

	SELECT AttributeValue = @GameIcon
	FROM GD_GameAttributes
	WHERE ObjectName LIKE 'GameIcon' AND AttributeName LIKE 'Image'
						AND LanguageID = @PlayerLang

	SELECT Activated = @isButtonEnabled
	FROM GD_Games
	WHERE GameID = @GameID

	SELECT @GameID as GameID, @VariantID as VariantID, @PlayerLang as PlayerLang,
			@GameName as GameName, @GameDescription as GameDescription,
			@ButtonDesc as ButtonDesc, @GameIcon as GameIcon,
			@isButtonEnabled as isButtonEnabled

END
