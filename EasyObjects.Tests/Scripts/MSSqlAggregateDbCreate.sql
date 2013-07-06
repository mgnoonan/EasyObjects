CREATE DATABASE [AggregateDb]  ON (NAME = N'AggregateDb_Data', FILENAME = N'C:\PROGRAM FILES\MICROSOFT SQL SERVER\MSSQL\DATA\AggregateDb.mdf' , SIZE = 2, FILEGROWTH = 10%) LOG ON (NAME = N'AggregateDb_Log', FILENAME = N'C:\PROGRAM FILES\MICROSOFT SQL SERVER\MSSQL\DATA\AggregateDb_log.ldf' , SIZE = 1, FILEGROWTH = 10%)
GO

exec sp_dboption N'AggregateDb', N'autoclose', N'true'
GO

exec sp_dboption N'AggregateDb', N'bulkcopy', N'false'
GO

exec sp_dboption N'AggregateDb', N'trunc. log', N'true'
GO

exec sp_dboption N'AggregateDb', N'torn page detection', N'true'
GO

exec sp_dboption N'AggregateDb', N'read only', N'false'
GO

exec sp_dboption N'AggregateDb', N'dbo use', N'false'
GO

exec sp_dboption N'AggregateDb', N'single', N'false'
GO

exec sp_dboption N'AggregateDb', N'autoshrink', N'true'
GO

exec sp_dboption N'AggregateDb', N'ANSI null default', N'false'
GO

exec sp_dboption N'AggregateDb', N'recursive triggers', N'false'
GO

exec sp_dboption N'AggregateDb', N'ANSI nulls', N'false'
GO

exec sp_dboption N'AggregateDb', N'concat null yields null', N'false'
GO

exec sp_dboption N'AggregateDb', N'cursor close on commit', N'false'
GO

exec sp_dboption N'AggregateDb', N'default to local cursor', N'false'
GO

exec sp_dboption N'AggregateDb', N'quoted identifier', N'false'
GO

exec sp_dboption N'AggregateDb', N'ANSI warnings', N'false'
GO

exec sp_dboption N'AggregateDb', N'auto create statistics', N'true'
GO

exec sp_dboption N'AggregateDb', N'auto update statistics', N'true'
GO

if( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) )
	exec sp_dboption N'AggregateDb', N'db chaining', N'false'
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AggregateTest]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AggregateTest]
GO

CREATE TABLE [dbo].[AggregateTest] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[DepartmentID] [int] NULL ,
	[FirstName] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[LastName] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Age] [int] NULL ,
	[HireDate] [datetime] NULL ,
	[Salary] [numeric](8, 4) NULL ,
	[IsActive] [bit] NULL 
) ON [PRIMARY]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FullNameView]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[FullNameView]
GO

CREATE VIEW [dbo].[FullNameView] AS 
SELECT (AggregateTest.LastName + ', ' + AggregateTest.FirstName) AS 'FullName',
        AggregateTest.DepartmentID,
        AggregateTest.HireDate,
        AggregateTest.Salary,
        AggregateTest.IsActive
FROM AggregateTest
WHERE (((AggregateTest.IsActive)=1))
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




ALTER TABLE [dbo].[AggregateTest] ADD 
	CONSTRAINT [PK_AggregateTest] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO







