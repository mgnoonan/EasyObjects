Imports System
Imports System.Data
Imports NUnit.Framework
Imports NCI.EasyObjects

Namespace EasyObjects.Tests.SQL

    <TestFixture()> _
    Public Class SqlGuidKeyFixture
        Dim obj As Employees = New Employees
        Dim primaryKey As Guid = Guid.Empty

        <TestFixtureSetUp()> _
        Public Sub Init()
            obj.DatabaseInstanceName = "SQLUnitTests"
        End Sub

        <SetUp()> _
        Public Sub Init2()
            obj.FlushData()
        End Sub

        <Test()> _
        Public Sub Proc2LoadByPrimaryKey()
            obj.DefaultCommandType = CommandType.StoredProcedure

            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            Assert.AreEqual(1, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub Proc1Insert()
            obj.DefaultCommandType = CommandType.StoredProcedure

            obj.AddNew()
            obj.s_BirthDate = "01/01/1980"
            obj.s_City = "Springboro"
            obj.s_CounTry = "USA"
            obj.s_FirstName = "Matthew"
            obj.HireDate = DateTime.Now
            obj.s_LastName = "Noonan"
            obj.s_PostalCode = "45066"
            obj.s_Region = "OH"
            obj.s_Title = "President"
            obj.s_TitleOfCourtesy = "Mr."
            obj.Save()

            Assert.IsFalse(obj.IsColumnNull(EmployeesSchema.EmployeeID.FieldName))
            primaryKey = obj.EmployeeID

            obj.GetChanges()
            Assert.AreEqual(0, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub Proc3Update()
            obj.DefaultCommandType = CommandType.StoredProcedure

            Assert.IsTrue(obj.LoadAll())
            Assert.AreEqual(1, obj.RowCount)

            obj.s_PostalCode = "45387"
            obj.s_FirstName = "Michael"
            obj.Save()

            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            Assert.AreEqual(1, obj.RowCount)
            Assert.AreEqual("45387", obj.s_PostalCode)
        End Sub

        <Test()> _
        Public Sub Proc4Delete()
            obj.DefaultCommandType = CommandType.StoredProcedure

            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            obj.DeleteAll()
            obj.Save()

            Assert.IsFalse(obj.LoadByPrimaryKey(primaryKey))
        End Sub

        <Test()> _
        Public Sub Dynamic2LoadByPrimaryKey()
            obj.DefaultCommandType = CommandType.Text

            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            Assert.AreEqual(1, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub Dynamic1Insert()
            obj.DefaultCommandType = CommandType.Text

            obj.AddNew()
            obj.s_BirthDate = "01/01/1980"
            obj.s_City = "Springboro"
            obj.s_CounTry = "USA"
            obj.s_FirstName = "Matthew"
            obj.HireDate = DateTime.Now
            obj.s_LastName = "Noonan"
            obj.s_PostalCode = "45066"
            obj.s_Region = "OH"
            obj.s_Title = "President"
            obj.s_TitleOfCourtesy = "Mr."
            obj.Save()

            Assert.IsFalse(obj.IsColumnNull(EmployeesSchema.EmployeeID.FieldName))
            primaryKey = obj.EmployeeID

            obj.GetChanges()
            Assert.AreEqual(0, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub Dynamic3Update()
            obj.DefaultCommandType = CommandType.Text

            Assert.IsTrue(obj.LoadAll())
            Assert.AreEqual(1, obj.RowCount)

            obj.s_PostalCode = "45387"
            obj.s_FirstName = "Michael"
            obj.Save()

            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            Assert.AreEqual(1, obj.RowCount)
            Assert.AreEqual("45387", obj.s_PostalCode)
        End Sub

        <Test()> _
        Public Sub Dynamic4Delete()
            obj.DefaultCommandType = CommandType.Text

            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            obj.DeleteAll()
            obj.Save()

            Assert.IsFalse(obj.LoadByPrimaryKey(primaryKey))
        End Sub
    End Class
End Namespace
