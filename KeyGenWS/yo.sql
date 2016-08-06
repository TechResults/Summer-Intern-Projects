CREATE PROCEDURE dbo.GD_AddPrintJob
	-- Add the parameters for the stored procedure here
	@IPAddress nvarchar(15),
  @GameID bigint


AS
  Declare @PrintJob bigint
  Declare @CurrentTime datetime
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
  @CurrentTime = getDate()

  insert into GD_PrintJobs (KioskPlayed, GameAtPlayID, JobTimeStamp)
  values (@IPAddress, @GameID, @CurrentTime)

END
GO

5 rows for each printjobid
