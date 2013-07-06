Imports System
Imports System.Data
Imports NUnit.Framework
Imports NCI.EasyObjects

Namespace EasyObjects.Tests.SQL

    <TestFixture()> _
    Public Class SqlTinyIntKeyFixture
        Private obj As CivilStatus = New CivilStatus
        Private primaryKey As Byte = 0

        <TestFixtureSetUp()> _
        Public Sub Init()
            obj.DatabaseInstanceName = "SQLUnitTests"
            obj.ResetTable()
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
            obj.s_CivilStatusEN = "Hello"
            obj.s_CivilStatusES = "Hola"
            obj.s_CivilStatusFR = "Bonjour"
            obj.Save()
            Assert.IsFalse(obj.IsColumnNull(CivilStatusSchema.CivilStatusID.FieldName))
            primaryKey = obj.CivilStatusID
            obj.GetChanges()
            Assert.AreEqual(0, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub Proc3Update()
            obj.DefaultCommandType = CommandType.StoredProcedure
            Assert.IsTrue(obj.LoadAll)
            Assert.AreEqual(1, obj.RowCount)
            obj.s_CivilStatusES = "Buenos dias"
            obj.Save()
            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            Assert.AreEqual(1, obj.RowCount)
            Assert.AreEqual("Buenos dias", obj.s_CivilStatusES)
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
            obj.s_CivilStatusEN = "Hello"
            obj.s_CivilStatusES = "Hola"
            obj.s_CivilStatusFR = "Bonjour"
            obj.Save()
            Assert.IsFalse(obj.IsColumnNull(CivilStatusSchema.CivilStatusID.FieldName))
            primaryKey = obj.CivilStatusID
            obj.GetChanges()
            Assert.AreEqual(0, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub Dynamic3Update()
            obj.DefaultCommandType = CommandType.Text
            Assert.IsTrue(obj.LoadAll)
            Assert.AreEqual(1, obj.RowCount)
            obj.s_CivilStatusES = "Buenos dias"
            obj.Save()
            Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey))
            Assert.AreEqual(1, obj.RowCount)
            Assert.AreEqual("Buenos dias", obj.s_CivilStatusES)
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