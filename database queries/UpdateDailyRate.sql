CREATE PROCEDURE dbo.UpdateDailyRate 
(
	@Amount money,
	@JobType int
)
as 
begin
	update JobType 
	set DailyRate = @Amount
	where JobTypeID = @JobType;
end