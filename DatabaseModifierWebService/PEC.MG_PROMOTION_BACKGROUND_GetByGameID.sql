-- PEC.MG_PROMOTION_BACKGROUND_GetByGameID
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters
-- command (Ctrl-Shift-M) to fill in the parameter
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ellis Johnson
-- Create date: 6/16/2016
-- Description:	Get Promotion Background and Intervals
-- =============================================
CREATE PROCEDURE PEC.MG_PROMOTION_BACKGROUND_GetByGameID
  @CMSPlayerID nvarchar(25),
	@GameID bigint,
  @VariantID int,
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
BEGIN
  DECLARE @PlayerLang int
  SET @PlayerLang = dbo.GD_SESSION_GetForPlayer (@IPAddress)

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT gg.GameName, ggp.PageName, ggat.TypeName, gga.ObjectName,
         gga.AttributeName, gga.AttributeValue
  FROM GD_GameAttributes gga
  inner join GD_Games gg
    on gga.GameID = gg.GameID
  inner join GD_GamePages ggp
    on gga.PageID = ggp.PageID
  inner join GD_GameAttributeTypes ggat
    on gga.AttributeTypeID = ggat.AttributeTypeID
  WHERE gga.GameID = @GameID
    AND gga.VariantID = @VariantID
    AND gga.LanguageID = @PlayerLang
END
GO
