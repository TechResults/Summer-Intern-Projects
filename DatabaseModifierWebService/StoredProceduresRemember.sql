'   get promo ids
   get game ids & VariantID
   get page wrapper
'

--dbo.GD_GAME_LoadForPromotion
declare @gamingDateStart datetime,
    @gamingDateEnd datetime
select @gamingDateStart = dbo.current_day_start(getdate())
select @gamingDateEnd = dateadd(dd, 1, @gamingDateStart)

--	 insert into GD_LogDBTimes(ProcName, Message, LogDate)
--	 values ('GD_GAME_LoadForPromotion', 'Start Query for ' + cast(@PromotionID as nvarchar) + ', ' + cast(@GameID as nvarchar) + ', ' + @PlayerAccountNum + ', ' + @CMSPlayerID, Getdate())

Declare @PlayerLang int
set @PlayerLang = dbo.GD_SESSION_GetForPlayer (@IPAddress)

If(@GameID < 0)
Begin
  select
    pg.PromotionID, pg.GameID, pg.VariantID,
    Case when gv.NumMatches = 0
      Then gg.NumMatches
    Else gv.NumMatches
    End as NumMatches,
    gg.GameName, gg.GameDescription, gpi.PlayInstructions, gv.GameVariantName,
    gg.GameMoviePath, gv.RootObjectFolder, gv.Wide, gv.UseObjectPng
  from GD_PromotionalGames pg with (nolock)
  inner join GD_PromotionParticipationRules pr with (nolock)
    on pr.PromotionID = pg.PromotionID
    and pr.GameID = pg.GameID
  inner join GD_Games gg with (nolock)
    on gg.GameID = pg.GameID
  inner join GD_GameVariants gv with (nolock)
    on gv.GameID = pg.GameID
    and gv.VariantID = pg.VariantID
  inner join GD_GamePlayInstructions gpi
    on gv.GameID = gpi.GameID
    and gv.VariantID = gpi.VariantID
    and gpi.LanguageID = @PlayerLang    -- added for the localization code
  left outer join GD_ParticipationDaily pd with (nolock)
    on pd.PromotionID = pg.PromotionID
    and pd.GameID = pg.GameID
    and pd.PlayerAccountNum = @playeraccountnum
    and pd.CMSPlayerID = @cmsplayerid
    and floor(cast(pd.ParticipationDate as float)) = floor(cast(getdate() as float))
    and isnull(pd.MaxDailyTries, pr.MaxDailyTries) < isnull(pd.CurrentDailyTries, 0)
  where pg.PromotionID = @PromotionID
  and pd.ParticipationID is null
  End
Else
  Begin
  select  top 1
    pg.PromotionID, pg.GameID, pg.VariantID,
    Case When gv.NumMatches = 0
      Then gg.NumMatches
    Else gv.NumMatches
    End as NumMatches,
    gg.GameName, gg.GameDescription, gpi.PlayInstructions, gv.GameVariantName,
    gg.GameMoviePath, gv.RootObjectFolder, gv.Wide, gv.UseObjectPng
  from GD_PromotionalGames pg with (nolock)
  inner join GD_PromotionParticipationRules pr with (nolock)
    on pr.PromotionID = pg.PromotionID
    and pr.GameID = pg.GameID
  inner join GD_Games gg with (nolock)
    on gg.GameID = pg.GameID
  inner join GD_GameVariants gv with (nolock)
    on gv.GameID = pg.GameID
    and gv.VariantID = pg.VariantID
  inner join GD_GamePlayInstructions gpi with (nolock)
    on gv.GameID = gpi.GameID
    and gv.VariantID = gpi.VariantID
    and gpi.LanguageID = @PlayerLang  -- added for the localization code
  left outer join GD_ParticipationDaily pd with (nolock)
    on pd.PromotionID = pg.PromotionID
    and pd.GameID = pg.GameID
    and pd.CMSPlayerID = @cmsplayerid
    and (pd.MaxPerPromoLifetime = 1 OR pd.ParticipationDate between @gamingDateStart and @gamingDateEnd)
  left outer join players_entered pe with (nolock) -- Added 6/2
    on pe.PromoID = pg.PromotionID
    and pe.CMSPlayerID = @CMSPlayerID
    and GETDATE() BETWEEN pe.JoinedDate AND dbo.current_day_end(pe.JoinedDate) --Added logic to only load games for the gamingdate earned
    and pe.RemovedDate is null
    and pe.invalid = 0
  where pg.PromotionID = @PromotionID
  and pg.GameID = @GameID
  and ((pd.participationID is null and pr.maxdailytries > 0)
    OR (pd.participationID is not null AND isnull(pd.CurrentDailyTries, 0) < isnull(pd.MaxDailyTries, pr.MaxDailyTries))
    OR (pe.playerandpromoId is not null))
  End

insert into GD_LogDBTimes(ProcName, Message, LogDate)
values ('GD_GAME_LoadForPromotion', 'End Query for ' + cast(@PromotionID as nvarchar) + ', ' + cast(@GameID as nvarchar) + ', ' + @PlayerAccountNum + ', ' + @CMSPlayerID, Getdate())
End
Else
Begin
/* Return a empty dataset to have Games Direct state
max plays reached for the day as we have noticed an existing Record in Gd_Sessions
that isn't 6 Minutes Old yet */
select top 1
  pg.PromotionID, pg.GameID, pg.VariantID,
  Case when gv.NumMatches = 0
    Then gg.NumMatches
  Else gv.NumMatches
  End as NumMatches,
  gg.GameName, gg.GameDescription, gpi.PlayInstructions, gv.GameVariantName,
  gg.GameMoviePath, gv.RootObjectFolder, gv.Wide, gv.UseObjectPng
from GD_PromotionalGames pg with (nolock)
inner join GD_PromotionParticipationRules pr with (nolock)
  on pr.PromotionID = pg.PromotionID
  and pr.GameID = pg.GameID
inner join GD_Games gg with (nolock)
  on gg.GameID = pg.GameID
inner join GD_GameVariants gv with (nolock)
  on gv.GameID = pg.GameID
  and gv.VariantID = pg.VariantID
inner join GD_GamePlayInstructions gpi
  on gv.GameID = gpi.GameID
  and gv.VariantID = gpi.VariantID
where pg.PromotionID = 0
end


--pec.PROMOTION_ID_GetByCMSUserID
   @CMSPlayerID varchar(8),

   select PromotionID
   from tblcurrentPromotionsByPlayer tcpp
   inner join tblPromotions tpr on tcpp.PromotionID = tpr.
   where CMSPlayerID = @CMSPlayerID
   and getdate() between tpr.StartDate and tpr.EndDate;

--GetGameAttributes
[dbo].[GD_GAMEATTRIBUTES_GetObjectAttributesByType]
(
   @GameID bigint,
   @PageID int,
   @VariantID int,
   @AttributeTypeID int
)
as
  select min(AttributeID) as AttributeID, ObjectName, AttributeName, Min(AttributeValue) as AttributeValue
  from GD_GameAttributes  with (nolock)
  where GameID = @GameID
  and PageID = @PageID
  and VariantID = @VariantID
  and AttributeTypeID = @AttributeTypeID
  group by ObjectName, AttributeName


-- dbo.GD_PROMOTION_CanCheckIn
@PromotionID as bigint,
@playeraccountnum as varchar(8),
@cmsplayerid as varchar(8)
)

as
declare @mydate datetime
set @mydate = GetDate()

Declare @good bit
set @good = dbo.PromoCanCheckIn(@PromotionID, @playeraccountnum, @cmsplayerid, @mydate)
select @good


-- dbo.PromoCanCheckIn (FUNCTION)
@PromotionID bigint,
@PlayerAccountNum varchar(8),
@CMSPlayerID varchar(8),
@thedate datetime
)
RETURNS bit
as
Begin
declare @cancheckin bit
declare @isenrolled bit
declare @playbypromoid bigint
declare @drawingid BIGINT

if not exists (Select * from tblcurrentPromotionsByPlayer
               where PromotionID = @PromotionID
               and PlayerAccountNum = @PlayerAccountNum
               and CMSPlayerID = @CMSPlayerID)
begin
     set @isenrolled = 0
End
if @isenrolled = 0
BEGIN
    set @cancheckin = 0
   RETURN @cancheckin
END
ELSE
BEGIN

  select @drawingid = draw.DrawingID from dbo.promoNextDrawingInfo(@PromotionID) draw
                           left outer join tblDailyPoints pts
                           on pts.PromotionID = draw.PromotionID
                           and pts.PlayerAccountNum = @PlayerAccountNum
                           and pts.CMSPlayerID = @CMSPlayerID
                           and pts.PointSourceID = 8
                           and (pts.GamingDate >= draw.CheckInStartTime)
                           where  draw.CheckInReqd = 1
                           and @thedate >= draw.CheckInStartTime
                           and pts.DailyPointStatID is null

  if @drawingid is null
     set @cancheckin = 0
  else
     set @cancheckin = 1

END

RETURN @cancheckin
END
