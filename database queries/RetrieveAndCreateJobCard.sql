-- Retrieve Job Card
create procedure dbo.GetJobCard 
as 
begin
	select JCV.JobCardNo, JCV.CustFirstName,JCV.CustSurname, JCV.PhysicalAddress, JCV.Province, JCV.Code, JCV.JobType, JCV.NoOfDays, 
	SUBSTRING(( 
				select ', ' + Convert(varchar(3), MO.Quantity) + ' x ' + I.MaterialName 
				 from MaterialOrder MO, Inventory I, Job J, Material_Job MJ, JobCard JC
				 where MO.MaterialOrderID = MJ.MaterialOrderID
				 and J.JobID = MJ.JobID
		  		 and I.MaterialID = MO.MaterialID
				 and J.JobID = JC.JobID
				 and JC.JobCardNo = JCV.JobCardNo
				 FOR XML PATH('') 
		), 2 , 9999) as Materials
	from JobCardView JCV
end

	-- View Jobcard numbers
CREATE PROCEDURE dbo.ViewJobCardNumbers
as
begin 
	select JC.JobCardNo, C.CustFirstName + ' ' + C.CustSurname as 'Customer Name'
	from JobCard JC, Customer C
	where C.CustomerID = JC.CustomerID;
end

-- CREATE JOB CARD
	-- Add user and job information
CREATE PROCEDURE dbo.CreateJobCard
(
	@PhysicalAddress varchar(40),
	@Province varchar(40),
	@Code varchar(4),
	@CustomerName varchar(40),
	@CustomerSurname varchar(40),
	@JobTypeID int,
	@NumOfDays int
)
as
begin
	insert into Customer values (@CustomerName,@CustomerSurname)

	declare @CustID as int
	select @CustID = [CustomerID] from Customer	
	insert into Addresses values ( @CustID,@PhysicalAddress,@Province,@Code)

	insert into Job values (@JobTypeID,@NumOfDays)
	
	declare @JobCardNum as int
		select @JobCardNum = [JobCardNo] from JobCard order by JobCardNo asc
		set @JobCardNum = @JobCardNum + 1
	declare @JobID as int
		select @JobID = [JobID] from Job
	insert into JobCard values (@JobCardNum,@JobID,@CustID)
end 
	
	-- Add materials
Create procedure dbo.MaterialsForJob
(
	@Material int,
	@Quantity int
)
as
begin
	insert into MaterialOrder values (@Material,@Quantity) -- add the material and quantity to material order
	update Inventory set QuantityAvailable = QuantityAvailable - @Quantity 
	where MaterialID = @Material -- reduce the number of material in the inventory
	
	declare @LastJob as int
	select @LastJob = [JobID] from Job -- get the id of the current job that needs material added
	declare @MaterialOrdered as int
	select @MaterialOrdered = [MaterialOrderID] from MaterialOrder -- get the id of the material used in the current job
	insert into Material_Job values (@MaterialOrdered,@LastJob)	-- add the material used to the current job
end

	