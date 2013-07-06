Imports System
Imports System.Data
Imports NUnit.Framework
Imports NCI.EasyObjects

Namespace EasyObjects.Tests.SQL

    <TestFixture()> _
    Public Class SqlComputedColumnFixture
        Dim obj As ComputedTest = New ComputedTest
        Dim primaryKey As Integer = -1

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
            obj.Integer1 = 1
            obj.Integer2 = 2
            obj.s_String1 = "One"
            obj.s_String2 = "Two"
            obj.Save()

            Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.RecordID.FieldName))
            primaryKey = obj.RecordID

            ' Computed columns
            Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.Computed1.FieldName))
            Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.Computed2.FieldName))
            Assert.AreEqual("3", obj.s_Computed1)
            Assert.AreEqual("One Two", obj.s_Computed2)

            obj.GetChanges()
            Assert.AreEqual(0, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub Proc3Update()
            obj.DefaultCommandType = CommandType.StoredProcedure

            Assert.IsTrue(obj.LoadAll())
            Assert.AreEqual(1, obj.RowCount)

            obj.Integer1 = 5
            obj.Integer2 = 10
            obj.String1 = "Five"
            obj.String2 = "Ten"
            obj.Save()

            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            Assert.AreEqual(1, obj.RowCount)
            Assert.AreEqual(15, obj.Computed1)
            Assert.AreEqual("Five Ten", obj.Computed2)
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
            obj.Integer1 = 1
            obj.Integer2 = 2
            obj.s_String1 = "One"
            obj.s_String2 = "Two"
            obj.Save()

            Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.RecordID.FieldName))
            primaryKey = obj.RecordID

            ' Computed columns
            Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.Computed1.FieldName))
            Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.Computed2.FieldName))
            Assert.AreEqual("3", obj.s_Computed1)
            Assert.AreEqual("One Two", obj.s_Computed2)

            obj.GetChanges()
            Assert.AreEqual(0, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub Dynamic3Update()
            obj.DefaultCommandType = CommandType.Text

            Assert.IsTrue(obj.LoadAll())
            Assert.AreEqual(1, obj.RowCount)

            obj.Integer1 = 5
            obj.Integer2 = 10
            obj.String1 = "Five"
            obj.String2 = "Ten"
            obj.Save()

            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            Assert.AreEqual(1, obj.RowCount)
            Assert.AreEqual(15, obj.Computed1)
            Assert.AreEqual("Five Ten", obj.Computed2)
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
