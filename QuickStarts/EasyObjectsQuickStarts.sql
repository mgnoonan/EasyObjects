
USE [Northwind]
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetCustomerCustomerDemo') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetCustomerCustomerDemo];
GO

CREATE PROCEDURE [daab_GetCustomerCustomerDemo]
(
	@CustomerID nchar(5),
	@CustomerTypeID nchar(10)
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CustomerID],
		[CustomerTypeID]
	FROM [CustomerCustomerDemo]
	WHERE
		([CustomerID] = @CustomerID) AND
		([CustomerTypeID] = @CustomerTypeID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetCustomerCustomerDemo Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetCustomerCustomerDemo Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllCustomerCustomerDemo') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllCustomerCustomerDemo];
GO

CREATE PROCEDURE [daab_GetAllCustomerCustomerDemo]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CustomerID],
		[CustomerTypeID]
	FROM [CustomerCustomerDemo]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllCustomerCustomerDemo Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllCustomerCustomerDemo Error on Creation'
GO

-------------------------------------------
-- NO UPDATE Stored Procedure Generated    
-- All Columns are part of the Primary key 
-------------------------------------------


IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddCustomerCustomerDemo') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddCustomerCustomerDemo];
GO

CREATE PROCEDURE [daab_AddCustomerCustomerDemo]
(
	@CustomerID nchar(5),
	@CustomerTypeID nchar(10)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [CustomerCustomerDemo]
	(
		[CustomerID],
		[CustomerTypeID]
	)
	VALUES
	(
		@CustomerID,
		@CustomerTypeID
	)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddCustomerCustomerDemo Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddCustomerCustomerDemo Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteCustomerCustomerDemo') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteCustomerCustomerDemo];
GO

CREATE PROCEDURE [daab_DeleteCustomerCustomerDemo]
(
	@CustomerID nchar(5),
	@CustomerTypeID nchar(10)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [CustomerCustomerDemo]
	WHERE
		[CustomerID] = @CustomerID AND
		[CustomerTypeID] = @CustomerTypeID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteCustomerCustomerDemo Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteCustomerCustomerDemo Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetCustomerDemographics') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetCustomerDemographics];
GO

CREATE PROCEDURE [daab_GetCustomerDemographics]
(
	@CustomerTypeID nchar(10)
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CustomerTypeID],
		[CustomerDesc]
	FROM [CustomerDemographics]
	WHERE
		([CustomerTypeID] = @CustomerTypeID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetCustomerDemographics Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetCustomerDemographics Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllCustomerDemographics') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllCustomerDemographics];
GO

CREATE PROCEDURE [daab_GetAllCustomerDemographics]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CustomerTypeID],
		[CustomerDesc]
	FROM [CustomerDemographics]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllCustomerDemographics Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllCustomerDemographics Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateCustomerDemographics') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateCustomerDemographics];
GO

CREATE PROCEDURE [daab_UpdateCustomerDemographics]
(
	@CustomerTypeID nchar(10),
	@CustomerDesc ntext = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [CustomerDemographics]
	SET
		[CustomerDesc] = @CustomerDesc
	WHERE
		[CustomerTypeID] = @CustomerTypeID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateCustomerDemographics Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateCustomerDemographics Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddCustomerDemographics') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddCustomerDemographics];
GO

CREATE PROCEDURE [daab_AddCustomerDemographics]
(
	@CustomerTypeID nchar(10),
	@CustomerDesc ntext = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [CustomerDemographics]
	(
		[CustomerTypeID],
		[CustomerDesc]
	)
	VALUES
	(
		@CustomerTypeID,
		@CustomerDesc
	)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddCustomerDemographics Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddCustomerDemographics Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteCustomerDemographics') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteCustomerDemographics];
GO

CREATE PROCEDURE [daab_DeleteCustomerDemographics]
(
	@CustomerTypeID nchar(10)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [CustomerDemographics]
	WHERE
		[CustomerTypeID] = @CustomerTypeID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteCustomerDemographics Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteCustomerDemographics Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetCustomers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetCustomers];
GO

CREATE PROCEDURE [daab_GetCustomers]
(
	@CustomerID nchar(5)
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CustomerID],
		[CompanyName],
		[ContactName],
		[ContactTitle],
		[Address],
		[City],
		[Region],
		[PostalCode],
		[Country],
		[Phone],
		[Fax]
	FROM [Customers]
	WHERE
		([CustomerID] = @CustomerID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetCustomers Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetCustomers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllCustomers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllCustomers];
GO

CREATE PROCEDURE [daab_GetAllCustomers]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CustomerID],
		[CompanyName],
		[ContactName],
		[ContactTitle],
		[Address],
		[City],
		[Region],
		[PostalCode],
		[Country],
		[Phone],
		[Fax]
	FROM [Customers]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllCustomers Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllCustomers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateCustomers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateCustomers];
GO

CREATE PROCEDURE [daab_UpdateCustomers]
(
	@CustomerID nchar(5),
	@CompanyName nvarchar(40),
	@ContactName nvarchar(30) = NULL,
	@ContactTitle nvarchar(30) = NULL,
	@Address nvarchar(60) = NULL,
	@City nvarchar(15) = NULL,
	@Region nvarchar(15) = NULL,
	@PostalCode nvarchar(10) = NULL,
	@Country nvarchar(15) = NULL,
	@Phone nvarchar(24) = NULL,
	@Fax nvarchar(24) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Customers]
	SET
		[CompanyName] = @CompanyName,
		[ContactName] = @ContactName,
		[ContactTitle] = @ContactTitle,
		[Address] = @Address,
		[City] = @City,
		[Region] = @Region,
		[PostalCode] = @PostalCode,
		[Country] = @Country,
		[Phone] = @Phone,
		[Fax] = @Fax
	WHERE
		[CustomerID] = @CustomerID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateCustomers Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateCustomers Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddCustomers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddCustomers];
GO

CREATE PROCEDURE [daab_AddCustomers]
(
	@CustomerID nchar(5),
	@CompanyName nvarchar(40),
	@ContactName nvarchar(30) = NULL,
	@ContactTitle nvarchar(30) = NULL,
	@Address nvarchar(60) = NULL,
	@City nvarchar(15) = NULL,
	@Region nvarchar(15) = NULL,
	@PostalCode nvarchar(10) = NULL,
	@Country nvarchar(15) = NULL,
	@Phone nvarchar(24) = NULL,
	@Fax nvarchar(24) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Customers]
	(
		[CustomerID],
		[CompanyName],
		[ContactName],
		[ContactTitle],
		[Address],
		[City],
		[Region],
		[PostalCode],
		[Country],
		[Phone],
		[Fax]
	)
	VALUES
	(
		@CustomerID,
		@CompanyName,
		@ContactName,
		@ContactTitle,
		@Address,
		@City,
		@Region,
		@PostalCode,
		@Country,
		@Phone,
		@Fax
	)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddCustomers Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddCustomers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteCustomers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteCustomers];
GO

CREATE PROCEDURE [daab_DeleteCustomers]
(
	@CustomerID nchar(5)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Customers]
	WHERE
		[CustomerID] = @CustomerID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteCustomers Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteCustomers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetDefaultValues') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetDefaultValues];
GO

CREATE PROCEDURE [daab_GetDefaultValues]
(
	@RefNumber numeric(18,0)
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[RefNumber],
		[FirstName],
		[Age],
		[Salary],
		[DOB],
		[Val]
	FROM [DefaultValues]
	WHERE
		([RefNumber] = @RefNumber)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetDefaultValues Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetDefaultValues Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllDefaultValues') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllDefaultValues];
GO

CREATE PROCEDURE [daab_GetAllDefaultValues]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[RefNumber],
		[FirstName],
		[Age],
		[Salary],
		[DOB],
		[Val]
	FROM [DefaultValues]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllDefaultValues Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllDefaultValues Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateDefaultValues') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateDefaultValues];
GO

CREATE PROCEDURE [daab_UpdateDefaultValues]
(
	@RefNumber numeric(18,0),
	@FirstName varchar(100),
	@Age int,
	@Salary numeric(18,4),
	@DOB datetime,
	@Val char(1) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [DefaultValues]
	SET
		[FirstName] = @FirstName,
		[Age] = @Age,
		[Salary] = @Salary,
		[DOB] = @DOB,
		[Val] = @Val
	WHERE
		[RefNumber] = @RefNumber


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateDefaultValues Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateDefaultValues Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddDefaultValues') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddDefaultValues];
GO

CREATE PROCEDURE [daab_AddDefaultValues]
(
	@RefNumber numeric(18,0) = NULL OUTPUT,
	@FirstName varchar(100),
	@Age int,
	@Salary numeric(18,4),
	@DOB datetime,
	@Val char(1) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [DefaultValues]
	(
		[FirstName],
		[Age],
		[Salary],
		[DOB],
		[Val]
	)
	VALUES
	(
		@FirstName,
		@Age,
		@Salary,
		@DOB,
		@Val
	)

	SET @Err = @@Error
	SELECT @RefNumber = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddDefaultValues Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddDefaultValues Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteDefaultValues') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteDefaultValues];
GO

CREATE PROCEDURE [daab_DeleteDefaultValues]
(
	@RefNumber numeric(18,0)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [DefaultValues]
	WHERE
		[RefNumber] = @RefNumber
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteDefaultValues Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteDefaultValues Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetEmployees') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetEmployees];
GO

CREATE PROCEDURE [daab_GetEmployees]
(
	@EmployeeID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[EmployeeID],
		[LastName],
		[FirstName],
		[Title],
		[TitleOfCourtesy],
		[BirthDate],
		[HireDate],
		[Address],
		[City],
		[Region],
		[PostalCode],
		[Country],
		[HomePhone],
		[Extension],
		[Photo],
		[Notes],
		[ReportsTo],
		[PhotoPath]
	FROM [Employees]
	WHERE
		([EmployeeID] = @EmployeeID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetEmployees Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetEmployees Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllEmployees') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllEmployees];
GO

CREATE PROCEDURE [daab_GetAllEmployees]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[EmployeeID],
		[LastName],
		[FirstName],
		[Title],
		[TitleOfCourtesy],
		[BirthDate],
		[HireDate],
		[Address],
		[City],
		[Region],
		[PostalCode],
		[Country],
		[HomePhone],
		[Extension],
		[Photo],
		[Notes],
		[ReportsTo],
		[PhotoPath]
	FROM [Employees]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllEmployees Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllEmployees Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateEmployees') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateEmployees];
GO

CREATE PROCEDURE [daab_UpdateEmployees]
(
	@EmployeeID int,
	@LastName nvarchar(20),
	@FirstName nvarchar(10),
	@Title nvarchar(30) = NULL,
	@TitleOfCourtesy nvarchar(25) = NULL,
	@BirthDate datetime = NULL,
	@HireDate datetime = NULL,
	@Address nvarchar(60) = NULL,
	@City nvarchar(15) = NULL,
	@Region nvarchar(15) = NULL,
	@PostalCode nvarchar(10) = NULL,
	@Country nvarchar(15) = NULL,
	@HomePhone nvarchar(24) = NULL,
	@Extension nvarchar(4) = NULL,
	@Photo image = NULL,
	@Notes ntext = NULL,
	@ReportsTo int = NULL,
	@PhotoPath nvarchar(255) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Employees]
	SET
		[LastName] = @LastName,
		[FirstName] = @FirstName,
		[Title] = @Title,
		[TitleOfCourtesy] = @TitleOfCourtesy,
		[BirthDate] = @BirthDate,
		[HireDate] = @HireDate,
		[Address] = @Address,
		[City] = @City,
		[Region] = @Region,
		[PostalCode] = @PostalCode,
		[Country] = @Country,
		[HomePhone] = @HomePhone,
		[Extension] = @Extension,
		[Photo] = @Photo,
		[Notes] = @Notes,
		[ReportsTo] = @ReportsTo,
		[PhotoPath] = @PhotoPath
	WHERE
		[EmployeeID] = @EmployeeID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateEmployees Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateEmployees Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddEmployees') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddEmployees];
GO

CREATE PROCEDURE [daab_AddEmployees]
(
	@EmployeeID int = NULL OUTPUT,
	@LastName nvarchar(20),
	@FirstName nvarchar(10),
	@Title nvarchar(30) = NULL,
	@TitleOfCourtesy nvarchar(25) = NULL,
	@BirthDate datetime = NULL,
	@HireDate datetime = NULL,
	@Address nvarchar(60) = NULL,
	@City nvarchar(15) = NULL,
	@Region nvarchar(15) = NULL,
	@PostalCode nvarchar(10) = NULL,
	@Country nvarchar(15) = NULL,
	@HomePhone nvarchar(24) = NULL,
	@Extension nvarchar(4) = NULL,
	@Photo image = NULL,
	@Notes ntext = NULL,
	@ReportsTo int = NULL,
	@PhotoPath nvarchar(255) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Employees]
	(
		[LastName],
		[FirstName],
		[Title],
		[TitleOfCourtesy],
		[BirthDate],
		[HireDate],
		[Address],
		[City],
		[Region],
		[PostalCode],
		[Country],
		[HomePhone],
		[Extension],
		[Photo],
		[Notes],
		[ReportsTo],
		[PhotoPath]
	)
	VALUES
	(
		@LastName,
		@FirstName,
		@Title,
		@TitleOfCourtesy,
		@BirthDate,
		@HireDate,
		@Address,
		@City,
		@Region,
		@PostalCode,
		@Country,
		@HomePhone,
		@Extension,
		@Photo,
		@Notes,
		@ReportsTo,
		@PhotoPath
	)

	SET @Err = @@Error
	SELECT @EmployeeID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddEmployees Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddEmployees Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteEmployees') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteEmployees];
GO

CREATE PROCEDURE [daab_DeleteEmployees]
(
	@EmployeeID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Employees]
	WHERE
		[EmployeeID] = @EmployeeID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteEmployees Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteEmployees Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetEmployeeTerritories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetEmployeeTerritories];
GO

CREATE PROCEDURE [daab_GetEmployeeTerritories]
(
	@EmployeeID int,
	@TerritoryID nvarchar(20)
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[EmployeeID],
		[TerritoryID]
	FROM [EmployeeTerritories]
	WHERE
		([EmployeeID] = @EmployeeID) AND
		([TerritoryID] = @TerritoryID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetEmployeeTerritories Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetEmployeeTerritories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllEmployeeTerritories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllEmployeeTerritories];
GO

CREATE PROCEDURE [daab_GetAllEmployeeTerritories]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[EmployeeID],
		[TerritoryID]
	FROM [EmployeeTerritories]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllEmployeeTerritories Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllEmployeeTerritories Error on Creation'
GO

-------------------------------------------
-- NO UPDATE Stored Procedure Generated    
-- All Columns are part of the Primary key 
-------------------------------------------


IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddEmployeeTerritories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddEmployeeTerritories];
GO

CREATE PROCEDURE [daab_AddEmployeeTerritories]
(
	@EmployeeID int,
	@TerritoryID nvarchar(20)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [EmployeeTerritories]
	(
		[EmployeeID],
		[TerritoryID]
	)
	VALUES
	(
		@EmployeeID,
		@TerritoryID
	)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddEmployeeTerritories Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddEmployeeTerritories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteEmployeeTerritories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteEmployeeTerritories];
GO

CREATE PROCEDURE [daab_DeleteEmployeeTerritories]
(
	@EmployeeID int,
	@TerritoryID nvarchar(20)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [EmployeeTerritories]
	WHERE
		[EmployeeID] = @EmployeeID AND
		[TerritoryID] = @TerritoryID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteEmployeeTerritories Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteEmployeeTerritories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetOrderDetails') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetOrderDetails];
GO

CREATE PROCEDURE [daab_GetOrderDetails]
(
	@OrderID int,
	@ProductID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[OrderID],
		[ProductID],
		[UnitPrice],
		[Quantity],
		[Discount]
	FROM [Order Details]
	WHERE
		([OrderID] = @OrderID) AND
		([ProductID] = @ProductID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetOrderDetails Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetOrderDetails Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllOrderDetails') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllOrderDetails];
GO

CREATE PROCEDURE [daab_GetAllOrderDetails]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[OrderID],
		[ProductID],
		[UnitPrice],
		[Quantity],
		[Discount]
	FROM [Order Details]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllOrderDetails Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllOrderDetails Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateOrderDetails') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateOrderDetails];
GO

CREATE PROCEDURE [daab_UpdateOrderDetails]
(
	@OrderID int,
	@ProductID int,
	@UnitPrice money,
	@Quantity smallint,
	@Discount real
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Order Details]
	SET
		[UnitPrice] = @UnitPrice,
		[Quantity] = @Quantity,
		[Discount] = @Discount
	WHERE
		[OrderID] = @OrderID
	AND	[ProductID] = @ProductID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateOrderDetails Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateOrderDetails Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddOrderDetails') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddOrderDetails];
GO

CREATE PROCEDURE [daab_AddOrderDetails]
(
	@OrderID int,
	@ProductID int,
	@UnitPrice money,
	@Quantity smallint,
	@Discount real
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Order Details]
	(
		[OrderID],
		[ProductID],
		[UnitPrice],
		[Quantity],
		[Discount]
	)
	VALUES
	(
		@OrderID,
		@ProductID,
		@UnitPrice,
		@Quantity,
		@Discount
	)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddOrderDetails Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddOrderDetails Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteOrderDetails') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteOrderDetails];
GO

CREATE PROCEDURE [daab_DeleteOrderDetails]
(
	@OrderID int,
	@ProductID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Order Details]
	WHERE
		[OrderID] = @OrderID AND
		[ProductID] = @ProductID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteOrderDetails Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteOrderDetails Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetOrders') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetOrders];
GO

CREATE PROCEDURE [daab_GetOrders]
(
	@OrderID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[OrderID],
		[CustomerID],
		[EmployeeID],
		[OrderDate],
		[RequiredDate],
		[ShippedDate],
		[ShipVia],
		[Freight],
		[ShipName],
		[ShipAddress],
		[ShipCity],
		[ShipRegion],
		[ShipPostalCode],
		[ShipCountry]
	FROM [Orders]
	WHERE
		([OrderID] = @OrderID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetOrders Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetOrders Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllOrders') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllOrders];
GO

CREATE PROCEDURE [daab_GetAllOrders]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[OrderID],
		[CustomerID],
		[EmployeeID],
		[OrderDate],
		[RequiredDate],
		[ShippedDate],
		[ShipVia],
		[Freight],
		[ShipName],
		[ShipAddress],
		[ShipCity],
		[ShipRegion],
		[ShipPostalCode],
		[ShipCountry]
	FROM [Orders]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllOrders Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllOrders Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateOrders') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateOrders];
GO

CREATE PROCEDURE [daab_UpdateOrders]
(
	@OrderID int,
	@CustomerID nchar(5) = NULL,
	@EmployeeID int = NULL,
	@OrderDate datetime = NULL,
	@RequiredDate datetime = NULL,
	@ShippedDate datetime = NULL,
	@ShipVia int = NULL,
	@Freight money = NULL,
	@ShipName nvarchar(40) = NULL,
	@ShipAddress nvarchar(60) = NULL,
	@ShipCity nvarchar(15) = NULL,
	@ShipRegion nvarchar(15) = NULL,
	@ShipPostalCode nvarchar(10) = NULL,
	@ShipCountry nvarchar(15) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Orders]
	SET
		[CustomerID] = @CustomerID,
		[EmployeeID] = @EmployeeID,
		[OrderDate] = @OrderDate,
		[RequiredDate] = @RequiredDate,
		[ShippedDate] = @ShippedDate,
		[ShipVia] = @ShipVia,
		[Freight] = @Freight,
		[ShipName] = @ShipName,
		[ShipAddress] = @ShipAddress,
		[ShipCity] = @ShipCity,
		[ShipRegion] = @ShipRegion,
		[ShipPostalCode] = @ShipPostalCode,
		[ShipCountry] = @ShipCountry
	WHERE
		[OrderID] = @OrderID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateOrders Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateOrders Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddOrders') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddOrders];
GO

CREATE PROCEDURE [daab_AddOrders]
(
	@OrderID int = NULL OUTPUT,
	@CustomerID nchar(5) = NULL,
	@EmployeeID int = NULL,
	@OrderDate datetime = NULL,
	@RequiredDate datetime = NULL,
	@ShippedDate datetime = NULL,
	@ShipVia int = NULL,
	@Freight money = NULL,
	@ShipName nvarchar(40) = NULL,
	@ShipAddress nvarchar(60) = NULL,
	@ShipCity nvarchar(15) = NULL,
	@ShipRegion nvarchar(15) = NULL,
	@ShipPostalCode nvarchar(10) = NULL,
	@ShipCountry nvarchar(15) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Orders]
	(
		[CustomerID],
		[EmployeeID],
		[OrderDate],
		[RequiredDate],
		[ShippedDate],
		[ShipVia],
		[Freight],
		[ShipName],
		[ShipAddress],
		[ShipCity],
		[ShipRegion],
		[ShipPostalCode],
		[ShipCountry]
	)
	VALUES
	(
		@CustomerID,
		@EmployeeID,
		@OrderDate,
		@RequiredDate,
		@ShippedDate,
		@ShipVia,
		@Freight,
		@ShipName,
		@ShipAddress,
		@ShipCity,
		@ShipRegion,
		@ShipPostalCode,
		@ShipCountry
	)

	SET @Err = @@Error
	SELECT @OrderID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddOrders Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddOrders Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteOrders') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteOrders];
GO

CREATE PROCEDURE [daab_DeleteOrders]
(
	@OrderID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Orders]
	WHERE
		[OrderID] = @OrderID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteOrders Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteOrders Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetProducts') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetProducts];
GO

CREATE PROCEDURE [daab_GetProducts]
(
	@ProductID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[ProductID],
		[ProductName],
		[SupplierID],
		[CategoryID],
		[QuantityPerUnit],
		[UnitPrice],
		[UnitsInStock],
		[UnitsOnOrder],
		[ReorderLevel],
		[Discontinued]
	FROM [Products]
	WHERE
		([ProductID] = @ProductID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetProducts Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetProducts Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllProducts') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllProducts];
GO

CREATE PROCEDURE [daab_GetAllProducts]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[ProductID],
		[ProductName],
		[SupplierID],
		[CategoryID],
		[QuantityPerUnit],
		[UnitPrice],
		[UnitsInStock],
		[UnitsOnOrder],
		[ReorderLevel],
		[Discontinued]
	FROM [Products]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllProducts Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllProducts Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateProducts') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateProducts];
GO

CREATE PROCEDURE [daab_UpdateProducts]
(
	@ProductID int,
	@ProductName nvarchar(40),
	@SupplierID int = NULL,
	@CategoryID int = NULL,
	@QuantityPerUnit nvarchar(20) = NULL,
	@UnitPrice money = NULL,
	@UnitsInStock smallint = NULL,
	@UnitsOnOrder smallint = NULL,
	@ReorderLevel smallint = NULL,
	@Discontinued bit
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Products]
	SET
		[ProductName] = @ProductName,
		[SupplierID] = @SupplierID,
		[CategoryID] = @CategoryID,
		[QuantityPerUnit] = @QuantityPerUnit,
		[UnitPrice] = @UnitPrice,
		[UnitsInStock] = @UnitsInStock,
		[UnitsOnOrder] = @UnitsOnOrder,
		[ReorderLevel] = @ReorderLevel,
		[Discontinued] = @Discontinued
	WHERE
		[ProductID] = @ProductID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateProducts Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateProducts Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddProducts') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddProducts];
GO

CREATE PROCEDURE [daab_AddProducts]
(
	@ProductID int = NULL OUTPUT,
	@ProductName nvarchar(40),
	@SupplierID int = NULL,
	@CategoryID int = NULL,
	@QuantityPerUnit nvarchar(20) = NULL,
	@UnitPrice money = NULL,
	@UnitsInStock smallint = NULL,
	@UnitsOnOrder smallint = NULL,
	@ReorderLevel smallint = NULL,
	@Discontinued bit
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Products]
	(
		[ProductName],
		[SupplierID],
		[CategoryID],
		[QuantityPerUnit],
		[UnitPrice],
		[UnitsInStock],
		[UnitsOnOrder],
		[ReorderLevel],
		[Discontinued]
	)
	VALUES
	(
		@ProductName,
		@SupplierID,
		@CategoryID,
		@QuantityPerUnit,
		@UnitPrice,
		@UnitsInStock,
		@UnitsOnOrder,
		@ReorderLevel,
		@Discontinued
	)

	SET @Err = @@Error
	SELECT @ProductID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddProducts Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddProducts Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteProducts') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteProducts];
GO

CREATE PROCEDURE [daab_DeleteProducts]
(
	@ProductID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Products]
	WHERE
		[ProductID] = @ProductID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteProducts Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteProducts Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetRegion') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetRegion];
GO

CREATE PROCEDURE [daab_GetRegion]
(
	@RegionID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[RegionID],
		[RegionDescription]
	FROM [Region]
	WHERE
		([RegionID] = @RegionID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetRegion Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetRegion Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllRegion') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllRegion];
GO

CREATE PROCEDURE [daab_GetAllRegion]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[RegionID],
		[RegionDescription]
	FROM [Region]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllRegion Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllRegion Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateRegion') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateRegion];
GO

CREATE PROCEDURE [daab_UpdateRegion]
(
	@RegionID int,
	@RegionDescription nchar(50)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Region]
	SET
		[RegionDescription] = @RegionDescription
	WHERE
		[RegionID] = @RegionID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateRegion Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateRegion Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddRegion') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddRegion];
GO

CREATE PROCEDURE [daab_AddRegion]
(
	@RegionID int,
	@RegionDescription nchar(50)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Region]
	(
		[RegionID],
		[RegionDescription]
	)
	VALUES
	(
		@RegionID,
		@RegionDescription
	)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddRegion Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddRegion Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteRegion') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteRegion];
GO

CREATE PROCEDURE [daab_DeleteRegion]
(
	@RegionID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Region]
	WHERE
		[RegionID] = @RegionID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteRegion Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteRegion Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetShippers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetShippers];
GO

CREATE PROCEDURE [daab_GetShippers]
(
	@ShipperID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[ShipperID],
		[CompanyName],
		[Phone]
	FROM [Shippers]
	WHERE
		([ShipperID] = @ShipperID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetShippers Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetShippers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllShippers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllShippers];
GO

CREATE PROCEDURE [daab_GetAllShippers]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[ShipperID],
		[CompanyName],
		[Phone]
	FROM [Shippers]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllShippers Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllShippers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateShippers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateShippers];
GO

CREATE PROCEDURE [daab_UpdateShippers]
(
	@ShipperID int,
	@CompanyName nvarchar(40),
	@Phone nvarchar(24) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Shippers]
	SET
		[CompanyName] = @CompanyName,
		[Phone] = @Phone
	WHERE
		[ShipperID] = @ShipperID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateShippers Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateShippers Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddShippers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddShippers];
GO

CREATE PROCEDURE [daab_AddShippers]
(
	@ShipperID int = NULL OUTPUT,
	@CompanyName nvarchar(40),
	@Phone nvarchar(24) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Shippers]
	(
		[CompanyName],
		[Phone]
	)
	VALUES
	(
		@CompanyName,
		@Phone
	)

	SET @Err = @@Error
	SELECT @ShipperID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddShippers Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddShippers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteShippers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteShippers];
GO

CREATE PROCEDURE [daab_DeleteShippers]
(
	@ShipperID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Shippers]
	WHERE
		[ShipperID] = @ShipperID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteShippers Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteShippers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetSuppliers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetSuppliers];
GO

CREATE PROCEDURE [daab_GetSuppliers]
(
	@SupplierID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[SupplierID],
		[CompanyName],
		[ContactName],
		[ContactTitle],
		[Address],
		[City],
		[Region],
		[PostalCode],
		[Country],
		[Phone],
		[Fax],
		[HomePage]
	FROM [Suppliers]
	WHERE
		([SupplierID] = @SupplierID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetSuppliers Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetSuppliers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllSuppliers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllSuppliers];
GO

CREATE PROCEDURE [daab_GetAllSuppliers]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[SupplierID],
		[CompanyName],
		[ContactName],
		[ContactTitle],
		[Address],
		[City],
		[Region],
		[PostalCode],
		[Country],
		[Phone],
		[Fax],
		[HomePage]
	FROM [Suppliers]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllSuppliers Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllSuppliers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateSuppliers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateSuppliers];
GO

CREATE PROCEDURE [daab_UpdateSuppliers]
(
	@SupplierID int,
	@CompanyName nvarchar(40),
	@ContactName nvarchar(30) = NULL,
	@ContactTitle nvarchar(30) = NULL,
	@Address nvarchar(60) = NULL,
	@City nvarchar(15) = NULL,
	@Region nvarchar(15) = NULL,
	@PostalCode nvarchar(10) = NULL,
	@Country nvarchar(15) = NULL,
	@Phone nvarchar(24) = NULL,
	@Fax nvarchar(24) = NULL,
	@HomePage ntext = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Suppliers]
	SET
		[CompanyName] = @CompanyName,
		[ContactName] = @ContactName,
		[ContactTitle] = @ContactTitle,
		[Address] = @Address,
		[City] = @City,
		[Region] = @Region,
		[PostalCode] = @PostalCode,
		[Country] = @Country,
		[Phone] = @Phone,
		[Fax] = @Fax,
		[HomePage] = @HomePage
	WHERE
		[SupplierID] = @SupplierID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateSuppliers Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateSuppliers Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddSuppliers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddSuppliers];
GO

CREATE PROCEDURE [daab_AddSuppliers]
(
	@SupplierID int = NULL OUTPUT,
	@CompanyName nvarchar(40),
	@ContactName nvarchar(30) = NULL,
	@ContactTitle nvarchar(30) = NULL,
	@Address nvarchar(60) = NULL,
	@City nvarchar(15) = NULL,
	@Region nvarchar(15) = NULL,
	@PostalCode nvarchar(10) = NULL,
	@Country nvarchar(15) = NULL,
	@Phone nvarchar(24) = NULL,
	@Fax nvarchar(24) = NULL,
	@HomePage ntext = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Suppliers]
	(
		[CompanyName],
		[ContactName],
		[ContactTitle],
		[Address],
		[City],
		[Region],
		[PostalCode],
		[Country],
		[Phone],
		[Fax],
		[HomePage]
	)
	VALUES
	(
		@CompanyName,
		@ContactName,
		@ContactTitle,
		@Address,
		@City,
		@Region,
		@PostalCode,
		@Country,
		@Phone,
		@Fax,
		@HomePage
	)

	SET @Err = @@Error
	SELECT @SupplierID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddSuppliers Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddSuppliers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteSuppliers') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteSuppliers];
GO

CREATE PROCEDURE [daab_DeleteSuppliers]
(
	@SupplierID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Suppliers]
	WHERE
		[SupplierID] = @SupplierID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteSuppliers Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteSuppliers Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetTerritories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetTerritories];
GO

CREATE PROCEDURE [daab_GetTerritories]
(
	@TerritoryID nvarchar(20)
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[TerritoryID],
		[TerritoryDescription],
		[RegionID]
	FROM [Territories]
	WHERE
		([TerritoryID] = @TerritoryID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetTerritories Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetTerritories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllTerritories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllTerritories];
GO

CREATE PROCEDURE [daab_GetAllTerritories]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[TerritoryID],
		[TerritoryDescription],
		[RegionID]
	FROM [Territories]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllTerritories Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllTerritories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateTerritories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateTerritories];
GO

CREATE PROCEDURE [daab_UpdateTerritories]
(
	@TerritoryID nvarchar(20),
	@TerritoryDescription nchar(50),
	@RegionID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Territories]
	SET
		[TerritoryDescription] = @TerritoryDescription,
		[RegionID] = @RegionID
	WHERE
		[TerritoryID] = @TerritoryID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateTerritories Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateTerritories Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddTerritories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddTerritories];
GO

CREATE PROCEDURE [daab_AddTerritories]
(
	@TerritoryID nvarchar(20),
	@TerritoryDescription nchar(50),
	@RegionID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Territories]
	(
		[TerritoryID],
		[TerritoryDescription],
		[RegionID]
	)
	VALUES
	(
		@TerritoryID,
		@TerritoryDescription,
		@RegionID
	)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddTerritories Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddTerritories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteTerritories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteTerritories];
GO

CREATE PROCEDURE [daab_DeleteTerritories]
(
	@TerritoryID nvarchar(20)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Territories]
	WHERE
		[TerritoryID] = @TerritoryID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteTerritories Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteTerritories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetCategories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetCategories];
GO

CREATE PROCEDURE [daab_GetCategories]
(
	@CategoryID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CategoryID],
		[CategoryName],
		[Description],
		[Picture]
	FROM [Categories]
	WHERE
		([CategoryID] = @CategoryID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetCategories Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetCategories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_GetAllCategories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_GetAllCategories];
GO

CREATE PROCEDURE [daab_GetAllCategories]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CategoryID],
		[CategoryName],
		[Description],
		[Picture]
	FROM [Categories]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_GetAllCategories Succeeded'
ELSE PRINT 'Procedure Creation: daab_GetAllCategories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_UpdateCategories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_UpdateCategories];
GO

CREATE PROCEDURE [daab_UpdateCategories]
(
	@CategoryID int,
	@CategoryName nvarchar(15),
	@Description ntext = NULL,
	@Picture image = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Categories]
	SET
		[CategoryName] = @CategoryName,
		[Description] = @Description,
		[Picture] = @Picture
	WHERE
		[CategoryID] = @CategoryID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_UpdateCategories Succeeded'
ELSE PRINT 'Procedure Creation: daab_UpdateCategories Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AddCategories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AddCategories];
GO

CREATE PROCEDURE [daab_AddCategories]
(
	@CategoryID int,
	@CategoryName nvarchar(15),
	@Description ntext = NULL,
	@Picture image = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Categories]
	(
		[CategoryID],
		[CategoryName],
		[Description],
		[Picture]
	)
	VALUES
	(
		@CategoryID,
		@CategoryName,
		@Description,
		@Picture
	)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AddCategories Succeeded'
ELSE PRINT 'Procedure Creation: daab_AddCategories Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_DeleteCategories') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_DeleteCategories];
GO

CREATE PROCEDURE [daab_DeleteCategories]
(
	@CategoryID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Categories]
	WHERE
		[CategoryID] = @CategoryID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_DeleteCategories Succeeded'
ELSE PRINT 'Procedure Creation: daab_DeleteCategories Error on Creation'
GO


