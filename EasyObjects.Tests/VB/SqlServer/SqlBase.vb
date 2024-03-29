Imports System
Imports NUnit.Framework
Imports NCI.EasyObjects

Namespace NCI.EasyObjects.Tests.SQL

	Public Class UnitTestBase

		Public Shared Sub RefreshDatabase()

			Dim testData As New AggregateTest
			testData.DatabaseInstanceName = "SQLUnitTests"

			testData.LoadAll()
			testData.DeleteAll()
			testData.Save()

			testData.AddNew()
			testData.s_DepartmentID = "3"
			testData.s_FirstName = "David"
			testData.s_LastName = "Doe"
			testData.s_Age = "16"
			testData.s_HireDate = "2000-02-16 00:00:00"
			testData.s_Salary = "34.71"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "1"
			testData.s_FirstName = "Sarah"
			testData.s_LastName = "McDonald"
			testData.s_Age = "28"
			testData.s_HireDate = "1999-03-25 00:00:00"
			testData.s_Salary = "11.06"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "3"
			testData.s_FirstName = "David"
			testData.s_LastName = "Vincent"
			testData.s_Age = "43"
			testData.s_HireDate = "2000-10-17 00:00:00"
			testData.s_Salary = "10.27"
			testData.s_IsActive = "false"
			testData.AddNew()
			testData.s_DepartmentID = "2"
			testData.s_FirstName = "Fred"
			testData.s_LastName = "Smith"
			testData.s_Age = "15"
			testData.s_HireDate = "1999-03-15 00:00:00"
			testData.s_Salary = "15.15"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "3"
			testData.s_FirstName = "Sally"
			testData.s_LastName = "Johnson"
			testData.s_Age = "30"
			testData.s_HireDate = "2000-10-07 00:00:00"
			testData.s_Salary = "14.36"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "5"
			testData.s_FirstName = "Jane"
			testData.s_LastName = "Rapaport"
			testData.s_Age = "44"
			testData.s_HireDate = "2002-05-02 00:00:00"
			testData.s_Salary = "13.56"
			testData.s_IsActive = "false"
			testData.AddNew()
			testData.s_DepartmentID = "4"
			testData.s_FirstName = "Paul"
			testData.s_LastName = "Gellar"
			testData.s_Age = "16"
			testData.s_HireDate = "2000-09-27 00:00:00"
			testData.s_Salary = "18.44"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "2"
			testData.s_FirstName = "John"
			testData.s_LastName = "Jones"
			testData.s_Age = "31"
			testData.s_HireDate = "2002-04-22 00:00:00"
			testData.s_Salary = "17.65"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "3"
			testData.s_FirstName = "Michelle"
			testData.s_LastName = "Johnson"
			testData.s_Age = "45"
			testData.s_HireDate = "2003-11-14 00:00:00"
			testData.s_Salary = "16.86"
			testData.s_IsActive = "false"
			testData.AddNew()
			testData.s_DepartmentID = "2"
			testData.s_FirstName = "David"
			testData.s_LastName = "Costner"
			testData.s_Age = "17"
			testData.s_HireDate = "2002-04-11 00:00:00"
			testData.s_Salary = "21.74"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "4"
			testData.s_FirstName = "William"
			testData.s_LastName = "Gellar"
			testData.s_Age = "32"
			testData.s_HireDate = "2003-11-04 00:00:00"
			testData.s_Salary = "20.94"
			testData.s_IsActive = "false"
			testData.AddNew()
			testData.s_DepartmentID = "3"
			testData.s_FirstName = "Sally"
			testData.s_LastName = "Rapaport"
			testData.s_Age = "39"
			testData.s_HireDate = "2002-04-01 00:00:00"
			testData.s_Salary = "25.82"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "5"
			testData.s_FirstName = "Jane"
			testData.s_LastName = "Vincent"
			testData.s_Age = "18"
			testData.s_HireDate = "2003-10-25 00:00:00"
			testData.s_Salary = "25.03"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "2"
			testData.s_FirstName = "Fred"
			testData.s_LastName = "Costner"
			testData.s_Age = "33"
			testData.s_HireDate = "1998-05-20 00:00:00"
			testData.s_Salary = "24.24"
			testData.s_IsActive = "false"
			testData.AddNew()
			testData.s_DepartmentID = "1"
			testData.s_FirstName = "John"
			testData.s_LastName = "Johnson"
			testData.s_Age = "40"
			testData.s_HireDate = "2003-10-15 00:00:00"
			testData.s_Salary = "29.12"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "3"
			testData.s_FirstName = "Michelle"
			testData.s_LastName = "Rapaport"
			testData.s_Age = "19"
			testData.s_HireDate = "1998-05-10 00:00:00"
			testData.s_Salary = "28.32"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "4"
			testData.s_FirstName = "Sarah"
			testData.s_LastName = "Doe"
			testData.s_Age = "34"
			testData.s_HireDate = "1999-12-03 00:00:00"
			testData.s_Salary = "27.53"
			testData.s_IsActive = "false"
			testData.AddNew()
			testData.s_DepartmentID = "4"
			testData.s_FirstName = "William"
			testData.s_LastName = "Jones"
			testData.s_Age = "41"
			testData.s_HireDate = "1998-04-30 00:00:00"
			testData.s_Salary = "32.41"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "1"
			testData.s_FirstName = "Sarah"
			testData.s_LastName = "McDonald"
			testData.s_Age = "21"
			testData.s_HireDate = "1999-11-23 00:00:00"
			testData.s_Salary = "31.62"
			testData.s_IsActive = "false"
			testData.AddNew()
			testData.s_DepartmentID = "4"
			testData.s_FirstName = "Jane"
			testData.s_LastName = "Costner"
			testData.s_Age = "28"
			testData.s_HireDate = "1998-04-20 00:00:00"
			testData.s_Salary = "36.50"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "2"
			testData.s_FirstName = "Fred"
			testData.s_LastName = "Douglas"
			testData.s_Age = "42"
			testData.s_HireDate = "1999-11-13 00:00:00"
			testData.s_Salary = "35.71"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "3"
			testData.s_FirstName = "Paul"
			testData.s_LastName = "Jones"
			testData.s_Age = "22"
			testData.s_HireDate = "2001-06-07 00:00:00"
			testData.s_Salary = "34.91"
			testData.s_IsActive = "false"
			testData.AddNew()
			testData.s_DepartmentID = "3"
			testData.s_FirstName = "Michelle"
			testData.s_LastName = "Doe"
			testData.s_Age = "29"
			testData.s_HireDate = "1999-11-03 00:00:00"
			testData.s_Salary = "39.79"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.s_DepartmentID = "4"
			testData.s_FirstName = "Paul"
			testData.s_LastName = "Costner"
			testData.s_Age = "43"
			testData.s_HireDate = "2001-05-28 00:00:00"
			testData.s_Salary = "39.00"
			testData.s_IsActive = "true"
			testData.AddNew()
			testData.AddNew()
			testData.AddNew()
			testData.AddNew()
			testData.AddNew()
			testData.AddNew()
			testData.s_DepartmentID = "0"
			testData.s_FirstName = ""
			testData.s_LastName = ""
			testData.s_Age = "0"
			testData.s_Salary = "0"

			testData.Save()

		End Sub

	End Class

End Namespace
