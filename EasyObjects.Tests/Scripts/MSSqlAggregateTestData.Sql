/* SQL Insert Script Provided by DbaMgr2k  0.15.0                                                                         */
/*                                                                                                                        */
/* Generated # 12/7/2005 5:05:16 PM #                                                                                     */
/* Original database  # AggregateDb #                                                                                     */
/* Original data table # [dbo].[AggregateTest] #                                                                          */
/*                                                                                                                        */
/* Settings                                                                                                               */
/* Property - Value                                                                                                       */
/* -) SET NOCOUNT ON = True                                                                                               */
/* -) Batch size = 1000                                                                                                   */
/* -) SET DATE FORMAT = ymd                                                                                               */
/* -) Date-Time format = {121} ODBC [yyyy-mm-dd hh:mi:ss.mmm(24h)]                                                        */
/*                                                                                                                        */
/*                                                                                                                        */
/* You are advised to check the script manually before running it.                                                        */
/* Also, BACK UP YOUR DATABASE before running this script                                                                 */
/* columns of IMAGE and VARIANT data type are not supported for obvious reasons                                           */
/*                                                                                                                        */
SET DATEFORMAT ymd

/* Loading data                                                                                                           */

SET NOCOUNT ON


INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 3, 'David', 'Doe', 16, '2000-02-16 00:00:00.000', 34.7100, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 1, 'Sarah', 'McDonald', 28, '1999-03-25 00:00:00.000', 11.0600, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 3, 'David', 'Vincent', 43, '2000-10-17 00:00:00.000', 10.2700, 0 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 2, 'Fred', 'Smith', 15, '1999-03-15 00:00:00.000', 15.1500, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 3, 'Sally', 'Johnson', 30, '2000-10-07 00:00:00.000', 14.3600, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 5, 'Jane', 'Rapaport', 44, '2002-05-02 00:00:00.000', 13.5600, 0 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 4, 'Paul', 'Gellar', 16, '2000-09-27 00:00:00.000', 18.4400, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 2, 'John', 'Jones', 31, '2002-04-22 00:00:00.000', 17.6500, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 3, 'Michelle', 'Johnson', 45, '2003-11-14 00:00:00.000', 16.8600, 0 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 2, 'David', 'Costner', 17, '2002-04-11 00:00:00.000', 21.7400, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 4, 'William', 'Gellar', 32, '2003-11-04 00:00:00.000', 20.9400, 0 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 3, 'Sally', 'Rapaport', 39, '2002-04-01 00:00:00.000', 25.8200, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 5, 'Jane', 'Vincent', 18, '2003-10-25 00:00:00.000', 25.0300, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 2, 'Fred', 'Costner', 33, '1998-05-20 00:00:00.000', 24.2400, 0 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 1, 'John', 'Johnson', 40, '2003-10-15 00:00:00.000', 29.1200, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 3, 'Michelle', 'Rapaport', 19, '1998-05-10 00:00:00.000', 28.3200, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 4, 'Sarah', 'Doe', 34, '1999-12-03 00:00:00.000', 27.5300, 0 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 4, 'William', 'Jones', 41, '1998-04-30 00:00:00.000', 32.4100, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 1, 'Sarah', 'McDonald', 21, '1999-11-23 00:00:00.000', 31.6200, 0 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 4, 'Jane', 'Costner', 28, '1998-04-20 00:00:00.000', 36.5000, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 2, 'Fred', 'Douglas', 42, '1999-11-13 00:00:00.000', 35.7100, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 3, 'Paul', 'Jones', 22, '2001-06-07 00:00:00.000', 34.9100, 0 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 3, 'Michelle', 'Doe', 29, '1999-11-03 00:00:00.000', 39.7900, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 4, 'Paul', 'Costner', 43, '2001-05-28 00:00:00.000', 39.0000, 1 )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( NULL, NULL, NULL, NULL, NULL, NULL, NULL )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( NULL, NULL, NULL, NULL, NULL, NULL, NULL )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( NULL, NULL, NULL, NULL, NULL, NULL, NULL )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( NULL, NULL, NULL, NULL, NULL, NULL, NULL )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( NULL, NULL, NULL, NULL, NULL, NULL, NULL )

INSERT INTO [dbo].[AggregateTest] ([DepartmentID], [FirstName], [LastName], [Age], [HireDate], [Salary], [IsActive] )
 VALUES ( 0, NULL, NULL, 0, NULL, 0.0000, NULL )

GO
-- Batch termination


SET NOCOUNT OFF

/* Data load has completed                                                                                                */
/* Sql Insert Script Provided by DbaMgr2k  0.15.0                                                                         */

