
USE [AggregateDb]
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AggregateTestLoadByPrimaryKey') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AggregateTestLoadByPrimaryKey];
GO

CREATE PROCEDURE [daab_AggregateTestLoadByPrimaryKey]
(
	@ID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[ID],
		[DepartmentID],
		[FirstName],
		[LastName],
		[Age],
		[HireDate],
		[Salary],
		[IsActive]
	FROM [AggregateTest]
	WHERE
		([ID] = @ID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AggregateTestLoadByPrimaryKey Succeeded'
ELSE PRINT 'Procedure Creation: daab_AggregateTestLoadByPrimaryKey Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AggregateTestLoadAll') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AggregateTestLoadAll];
GO

CREATE PROCEDURE [daab_AggregateTestLoadAll]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[ID],
		[DepartmentID],
		[FirstName],
		[LastName],
		[Age],
		[HireDate],
		[Salary],
		[IsActive]
	FROM [AggregateTest]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AggregateTestLoadAll Succeeded'
ELSE PRINT 'Procedure Creation: daab_AggregateTestLoadAll Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AggregateTestUpdate') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AggregateTestUpdate];
GO

CREATE PROCEDURE [daab_AggregateTestUpdate]
(
	@ID int,
	@DepartmentID int = NULL,
	@FirstName varchar(25) = NULL,
	@LastName varchar(15) = NULL,
	@Age int = NULL,
	@HireDate datetime = NULL,
	@Salary numeric(8,4) = NULL,
	@IsActive bit = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [AggregateTest]
	SET
		[DepartmentID] = @DepartmentID,
		[FirstName] = @FirstName,
		[LastName] = @LastName,
		[Age] = @Age,
		[HireDate] = @HireDate,
		[Salary] = @Salary,
		[IsActive] = @IsActive
	WHERE
		[ID] = @ID


	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AggregateTestUpdate Succeeded'
ELSE PRINT 'Procedure Creation: daab_AggregateTestUpdate Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AggregateTestInsert') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AggregateTestInsert];
GO

CREATE PROCEDURE [daab_AggregateTestInsert]
(
	@ID int = NULL OUTPUT,
	@DepartmentID int = NULL,
	@FirstName varchar(25) = NULL,
	@LastName varchar(15) = NULL,
	@Age int = NULL,
	@HireDate datetime = NULL,
	@Salary numeric(8,4) = NULL,
	@IsActive bit = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [AggregateTest]
	(
		[DepartmentID],
		[FirstName],
		[LastName],
		[Age],
		[HireDate],
		[Salary],
		[IsActive]
	)
	VALUES
	(
		@DepartmentID,
		@FirstName,
		@LastName,
		@Age,
		@HireDate,
		@Salary,
		@IsActive
	)

	SET @Err = @@Error
	SELECT @ID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AggregateTestInsert Succeeded'
ELSE PRINT 'Procedure Creation: daab_AggregateTestInsert Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('daab_AggregateTestDelete') AND sysstat & 0xf = 4)
    DROP PROCEDURE [daab_AggregateTestDelete];
GO

CREATE PROCEDURE [daab_AggregateTestDelete]
(
	@ID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [AggregateTest]
	WHERE
		[ID] = @ID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: daab_AggregateTestDelete Succeeded'
ELSE PRINT 'Procedure Creation: daab_AggregateTestDelete Error on Creation'
GO
