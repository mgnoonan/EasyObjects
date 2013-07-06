Imports System
Imports NUnit.Framework
Imports NCI.EasyObjects

NameSpace EasyObjects.Tests.SQL

	Public Class UnitTestBase

        Public Shared Sub RefreshDatabase()

            RefreshAggregateTest()

        End Sub

        Public Shared Sub RefreshAggregateTest()

            Dim obj As New AggregateTest
            obj.DatabaseInstanceName = "SQLUnitTests"
            obj.DefaultCommandType = Data.CommandType.Text

            obj.LoadAll()
            obj.DeleteAll()
            obj.Save()

            '// Turn IDENTITY insert on
            obj.IdentityInsert = True
            Dim i As Integer = 1

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "3"
            obj.s_FirstName = "David"
            obj.s_LastName = "Doe"
            obj.s_Age = "16"
            obj.s_HireDate = "2000-02-16 00:00:00"
            obj.s_Salary = "34.71"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "1"
            obj.s_FirstName = "Sarah"
            obj.s_LastName = "McDonald"
            obj.s_Age = "28"
            obj.s_HireDate = "1999-03-25 00:00:00"
            obj.s_Salary = "11.06"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "3"
            obj.s_FirstName = "David"
            obj.s_LastName = "Vincent"
            obj.s_Age = "43"
            obj.s_HireDate = "2000-10-17 00:00:00"
            obj.s_Salary = "10.27"
            obj.s_IsActive = "false"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "2"
            obj.s_FirstName = "Fred"
            obj.s_LastName = "Smith"
            obj.s_Age = "15"
            obj.s_HireDate = "1999-03-15 00:00:00"
            obj.s_Salary = "15.15"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "3"
            obj.s_FirstName = "Sally"
            obj.s_LastName = "Johnson"
            obj.s_Age = "30"
            obj.s_HireDate = "2000-10-07 00:00:00"
            obj.s_Salary = "14.36"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "5"
            obj.s_FirstName = "Jane"
            obj.s_LastName = "Rapaport"
            obj.s_Age = "44"
            obj.s_HireDate = "2002-05-02 00:00:00"
            obj.s_Salary = "13.56"
            obj.s_IsActive = "false"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "4"
            obj.s_FirstName = "Paul"
            obj.s_LastName = "Gellar"
            obj.s_Age = "16"
            obj.s_HireDate = "2000-09-27 00:00:00"
            obj.s_Salary = "18.44"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "2"
            obj.s_FirstName = "John"
            obj.s_LastName = "Jones"
            obj.s_Age = "31"
            obj.s_HireDate = "2002-04-22 00:00:00"
            obj.s_Salary = "17.65"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "3"
            obj.s_FirstName = "Michelle"
            obj.s_LastName = "Johnson"
            obj.s_Age = "45"
            obj.s_HireDate = "2003-11-14 00:00:00"
            obj.s_Salary = "16.86"
            obj.s_IsActive = "false"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "2"
            obj.s_FirstName = "David"
            obj.s_LastName = "Costner"
            obj.s_Age = "17"
            obj.s_HireDate = "2002-04-11 00:00:00"
            obj.s_Salary = "21.74"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "4"
            obj.s_FirstName = "William"
            obj.s_LastName = "Gellar"
            obj.s_Age = "32"
            obj.s_HireDate = "2003-11-04 00:00:00"
            obj.s_Salary = "20.94"
            obj.s_IsActive = "false"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "3"
            obj.s_FirstName = "Sally"
            obj.s_LastName = "Rapaport"
            obj.s_Age = "39"
            obj.s_HireDate = "2002-04-01 00:00:00"
            obj.s_Salary = "25.82"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "5"
            obj.s_FirstName = "Jane"
            obj.s_LastName = "Vincent"
            obj.s_Age = "18"
            obj.s_HireDate = "2003-10-25 00:00:00"
            obj.s_Salary = "25.03"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "2"
            obj.s_FirstName = "Fred"
            obj.s_LastName = "Costner"
            obj.s_Age = "33"
            obj.s_HireDate = "1998-05-20 00:00:00"
            obj.s_Salary = "24.24"
            obj.s_IsActive = "false"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "1"
            obj.s_FirstName = "John"
            obj.s_LastName = "Johnson"
            obj.s_Age = "40"
            obj.s_HireDate = "2003-10-15 00:00:00"
            obj.s_Salary = "29.12"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "3"
            obj.s_FirstName = "Michelle"
            obj.s_LastName = "Rapaport"
            obj.s_Age = "19"
            obj.s_HireDate = "1998-05-10 00:00:00"
            obj.s_Salary = "28.32"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "4"
            obj.s_FirstName = "Sarah"
            obj.s_LastName = "Doe"
            obj.s_Age = "34"
            obj.s_HireDate = "1999-12-03 00:00:00"
            obj.s_Salary = "27.53"
            obj.s_IsActive = "false"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "4"
            obj.s_FirstName = "William"
            obj.s_LastName = "Jones"
            obj.s_Age = "41"
            obj.s_HireDate = "1998-04-30 00:00:00"
            obj.s_Salary = "32.41"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "1"
            obj.s_FirstName = "Sarah"
            obj.s_LastName = "McDonald"
            obj.s_Age = "21"
            obj.s_HireDate = "1999-11-23 00:00:00"
            obj.s_Salary = "31.62"
            obj.s_IsActive = "false"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "4"
            obj.s_FirstName = "Jane"
            obj.s_LastName = "Costner"
            obj.s_Age = "28"
            obj.s_HireDate = "1998-04-20 00:00:00"
            obj.s_Salary = "36.50"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "2"
            obj.s_FirstName = "Fred"
            obj.s_LastName = "Douglas"
            obj.s_Age = "42"
            obj.s_HireDate = "1999-11-13 00:00:00"
            obj.s_Salary = "35.71"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "3"
            obj.s_FirstName = "Paul"
            obj.s_LastName = "Jones"
            obj.s_Age = "22"
            obj.s_HireDate = "2001-06-07 00:00:00"
            obj.s_Salary = "34.91"
            obj.s_IsActive = "false"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "3"
            obj.s_FirstName = "Michelle"
            obj.s_LastName = "Doe"
            obj.s_Age = "29"
            obj.s_HireDate = "1999-11-03 00:00:00"
            obj.s_Salary = "39.79"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "4"
            obj.s_FirstName = "Paul"
            obj.s_LastName = "Costner"
            obj.s_Age = "43"
            obj.s_HireDate = "2001-05-28 00:00:00"
            obj.s_Salary = "39.00"
            obj.s_IsActive = "true"

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1

            obj.AddNew()
            obj.s_ID = i.ToString() : i += 1
            obj.s_DepartmentID = "0"
            obj.s_FirstName = ""
            obj.s_LastName = ""
            obj.s_Age = "0"
            obj.s_Salary = "0"
            obj.Save()

        End Sub

    End Class

End Namespace
