-- Create new Employee
CREATE PROCEDURE dbo.CreateNewEmployee
(
	@EmpNum varchar(6),
	@EmpFirstName varchar(40),
	@EmpSurname varchar(40)
)
as
begin
	insert into Employee values (@EmpNum,@EmpFirstName,@EmpSurname)
end	

-- Update Employee
CREATE PROCEDURE UpdateEmployee
(
	@EmpNum varchar(6),
	@EmpFirstName varchar(40),
	@EmpSurname varchar(40)
)
as
begin
	Update Employee
	set EmpFirstName = @EmpFirstName
	where EmpNo = @EmpNum

	update Employee
	set EmpSurname = @EmpSurname
	where EmpNo = @EmpNum
end

-- View single employee info
CREATE PROCEDURE dbo.ViewEmployeeInfo
(
	@EmpNum varchar(6)
)
as
begin
	select * from Employee
	where EmpNo = @EmpNum
end

-- Delete employee
create procedure dbo.DeleteEmployee
(
	@EmpNo varchar(6)
)
as 
begin
	delete from JobCard_Employee
	where EmpNo = @EmpNo;

	delete from Employee 
	where EmpNo = @EmpNo;
end

--View all employees
CREATE PROCEDURE GetAllEmployees
as 
begin
	select * from Employee
end